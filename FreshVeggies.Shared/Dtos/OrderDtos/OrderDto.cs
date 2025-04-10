namespace FreshVeggies.Shared.Dtos.OrderDtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Remarks { get; set; }
        public string Status { get; set; }
        public string AddressName { get; set; }
        public string FullAddress { get; set; }
        public int TotalItems { get; set; }
    }
}
