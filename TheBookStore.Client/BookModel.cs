using System.Collections.Generic;

namespace TheBookStore.Client
{
    public class BookModel
    {
        public int id { get; set; }
        public string isbn { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public IEnumerable<AuthorItem> Authors { get; set; }
        public IEnumerable<ReviewItem> Reviews { get; set; }
    }
}