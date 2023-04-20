namespace Domain.Entities
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int Order_ID { get; set; }
        public int Product_ID { get; set; }
        public decimal price { get; set; }
        public string? MembershipName { get; set; }
        public int Quantity { get; set; }
        public Order? Order { get; set; }

    }
}
