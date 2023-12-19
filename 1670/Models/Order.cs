namespace _1670.Models
{
    public class Order
    {
        public int Id { get; set; }
        //public double Price { get; set; }
        public DateTime OrderTime { get; set; }
        public string UserId { get; set; }
        //public int ProductId { get; set; }

        public virtual Product? Product { get; set; }
    }
}
