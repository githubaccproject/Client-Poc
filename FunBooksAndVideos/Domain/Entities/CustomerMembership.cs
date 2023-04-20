namespace Domain.Entities
{
    public class CustomerMembership
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int CustomerId { get; set; }
    }
}
