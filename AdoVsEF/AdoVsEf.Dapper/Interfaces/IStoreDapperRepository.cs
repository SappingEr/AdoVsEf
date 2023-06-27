using AdoVsEf.Dto;
using AdoVsEf.Models;

namespace AdoVsEf.Dapper.Interfaces
{
	public interface IStoreDapperRepository
	{
		Product? GetProductById(int id);
		Product? GetProductBySqlRawQuery(int id);
		OrderWithDetailsDto? GetOrderWithDetailsById(int id);
	}
}
