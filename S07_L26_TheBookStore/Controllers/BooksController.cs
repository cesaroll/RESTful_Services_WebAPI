using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using TheBookStore;
using TheBookStore.Contracts;
using TheBookStore.DataStores;
using TheBookStore.DataTransferObjects;
using TheBookStore.Infrastructure;

namespace TheBookStore.Controllers
{
    /// <summary>
    /// Use this API to get all the books, search for specific criteria or get a single book
    /// </summary>
    public class BooksController : ApiController
    {
        private IUnitOfWork unit;

        public BooksController(IUnitOfWork unit)
        {
            this.unit = unit;
        }

        /// <summary>
        /// Retrieves all books registered on the system
        /// </summary>
        /// <returns>Collection of books entities</returns>
        [EnableQuery]
        [ResponseType(typeof(IEnumerable<BookDto>))]
        public IHttpActionResult Get()
        {
            var books = unit.Books.All;

            var response = books.To<BookDto>();

            return Ok(response);

        }

        /// <summary>
        /// Searches book for a specific query
        /// </summary>
        /// <param name="query">Query text to search</param>
        /// <returns>Filtered list based on search criteria</returns>
        [EnableQuery]
        [ResponseType(typeof(IEnumerable<BookDto>))]
        public IHttpActionResult Get(string query)
        {
            var results = unit.Books.Search(query);

            if (!results.Any())
                return NotFound();

            var response = results.To<BookDto>();

            return Ok(response);

        }

        /// <summary>
        /// Get a single book
        /// </summary>
        /// <param name="id">Unique identifier for book</param>
        /// <returns>Single book entitys</returns>
        [CheckNulls]
        public IHttpActionResult Get(int id)
        {
            var result = unit.Books.GetOne(id);

            if(result == null)
                return NotFound();

            var response = result.To<BookDto>();

            return Ok(response);

        }



    }
}
