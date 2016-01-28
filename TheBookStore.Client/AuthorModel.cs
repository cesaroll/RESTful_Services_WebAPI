using System.Collections.Generic;

namespace TheBookStore.Client
{
    public class AuthorModel
    {
        public int id { get; set; }
        public string fullname { get; set; }
        public IEnumerable<BookItem> Books { get; set; }
    }
}