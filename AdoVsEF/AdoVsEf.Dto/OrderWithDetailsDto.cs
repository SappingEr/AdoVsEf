using AdoVsEf.Models;

namespace AdoVsEf.Dto
{
    public class OrderWithDetailsDto
    {
        public Order? Order { get; set; }
        public IEnumerable<OrderDetailsDto>? Details { get; set; }
    }
}
