namespace TO_BDD.Models
{
    public class Order
    {
        public int Id { get; set; }
        //public int CartId { get; set; } = new();
        public int UserId { get; set; }
        public List<Book> Books { get; set; } = new ();
        public DateTime TimeStamp { get; set; }
    }
}
