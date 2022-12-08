namespace TO_BDD.Models
{
    public class Order
    {
        public int Id { get; set; }
        //public int CartId { get; set; } = new();
        public int UserId { get; set; }
        public string Books { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; }
    }
}
