namespace EmptySummer.Core.Models
{
    public class Book : Entity
    {
        public string Title { get; set; }
        public string IBAN { get; set; }
        public string Description { get; set; }

        public int AutorId { get; set; }
        public Author Author { get; set; }
    }
}
