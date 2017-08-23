namespace EmptySummer.Core.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
