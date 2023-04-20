namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int Customer_ID { get; set; }
        public DateTime Date { get; set; }
        public decimal price { get; set; }
        public List<OrderProduct>? OrderProducts { get; set; }
    }

}
