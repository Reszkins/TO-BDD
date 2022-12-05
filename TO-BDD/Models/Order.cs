namespace TO_BDD.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Cart Cart { get; set; } = new();
        public DateTime TimeStamp { get; set; }
    }
}
