namespace AdoVsEf.Dto
{
    public class OrderDetailsDto
    {
        public  int OrderId { get; set; }
        public string ProductName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
    }
}