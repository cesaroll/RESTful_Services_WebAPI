using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ClientPrototype
{
    class Program
    {
        static void Main(string[] args)
        {
            getBooks();

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        static async void getBooks()
        {
            using (var client = new HttpClient(new AuthenticationHandler("cesar", "supersecretpassword")))
            {
                client.BaseAddress = new Uri("https://localhost:44301/");

                var response = await client.GetAsync("/api/books");

                if (response.IsSuccessStatusCode)
                {
                    var books = await response.Content.ReadAsAsync<JToken>();

                    foreach (var book in books)
                    {
                        Console.WriteLine("{0}\t{1}", book["id"], book["title"]);
                    }
                }
                else
                {
                    Console.WriteLine("Error: {0}", response.ReasonPhrase);
                }
            }
            
        }
    }
}
