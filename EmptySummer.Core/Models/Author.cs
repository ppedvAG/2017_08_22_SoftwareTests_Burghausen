using System.Collections.Generic;

namespace EmptySummer.Core.Models
{
    public class Author : Entity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public ICollection<Book> Books { get; } = new HashSet<Book>();
    }
}
