using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TheBookStore.Client
{
    public class TheBookStoreClient
    {
        private readonly HttpClient client;
        public HttpResponseMessage response;

        public TheBookStoreClient()
        {
            ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };


            client = new HttpClient(new AuthenticationHandler("cesar", "supersecretpassword"));

            client.BaseAddress = new Uri("https://localhost:44301");

        }

        public BookModel GetBook(int id)
        {
            var response = client.GetAsync("/api/books/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                var bookResult = response.Content.ReadAsAsync<JToken>().Result;
                var book = new BookModel
                {
                    id = bookResult["id"].Value<int>(),
                    title = bookResult["title"].Value<string>(),
                    isbn = bookResult["description"].Value<string>(),
                    description = bookResult["description"].Value<string>(),
                    Authors = from a in bookResult["authors"]
                              select new AuthorItem
                              {
                                  id = a["id"].Value<int>(),
                                  fullname = a["fullName"].Value<string>()
                              }
                };

                var reviewResponse = client.GetAsync("/api/reviews/?bookid=" + id).Result;

                if (reviewResponse.IsSuccessStatusCode)
                {
                    var reviewResult = reviewResponse.Content.ReadAsAsync<JToken>().Result;
                    book.Reviews = from r in reviewResult
                                   select new ReviewItem
                                   {
                                        id = r["id"].Value<int>(),
                                        reviewer = r["name"].Value<string>(),
                                        comment = r["feedback"].Value<string>(),
                                        rating = r["rating"].Value<int>()
                                   };

                }

                return book;
            }

            return null;
        }

        public IEnumerable<BookItem> GetBooks()
        {
            response = client.GetAsync("/api/books").Result;

            if (response.IsSuccessStatusCode)
            {
                var books = response.Content.ReadAsAsync<JToken>().Result;
                var list = from b in books
                    select new BookItem
                    {
                        id = b["id"].Value<int>(),
                        title = b["title"].Value<string>()
                    };

                return list;
            }

            return null;
        }

        public AuthorModel GetAuthor(int id)
        {
            response = client.GetAsync("/api/authors/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<JToken>().Result;

                result = result.FirstOrDefault(r => r["id"].Value<int>() == id);

                var author = new AuthorModel
                {
                    id = result["id"].Value<int>(),
                    fullname = result["fullName"].Value<string>(),
                    Books = from a in result["books"]
                            select new BookItem
                            {
                                id = a["id"].Value<int>(),
                                title = a["title"].Value<string>()
                            }
                };

                return author;
            }

            return null;
        }

        public IEnumerable<AuthorItem> GetAuthors()
        {
            response = client.GetAsync("/api/authors").Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<JToken>().Result;

                var authors = from r in result
                    select new AuthorItem
                    {
                        id = r["id"].Value<int>(),
                        fullname = r["fullName"].Value<string>()
                    };

                return authors;
            }

            return null;
        }

        public void PostReview(ReviewModel review)
        {
            client.PostAsync<ReviewModel>("/api/reviews/?bookid=" + review.bookid, review, new JsonMediaTypeFormatter());
        }

    }
}
