using AdoVsEf.Dto;
using AdoVsEf.Models;

namespace AdoVsEf.EfDal.Interfaces
{
    public interface IStoreEfRepository
    {
        Product? GetProductByIdNotTracked(int id);
		Product? GetProductByIdTracked(int id);
		Product? GetProductBySqlRawQuery(int id);
        OrderWithDetailsDto? GetOrderWithDetailsById(int id);
        OrderWithDetailsDto? GetOrderWithDetailsById_1(int id);
        OrderWithDetailsDto? GetOrderWithDetailsById_2(int id);
		OrderWithDetailsDto? GetOrderDetailsByIdSplitQuery(int id);

		IEnumerable<Order>? GetTopOrderWithDetailsFullNotTracked(int count);
		IEnumerable<Order>? GetTopOrderWithDetailsFullWithIdentityResolution(int count);
		IEnumerable<Order>? GetTopOrderWithDetailsFull(int count);
	}
}
