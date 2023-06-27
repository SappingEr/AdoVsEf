using AdoVsEf.Dto;
using AdoVsEf.Models;

namespace AdoVsEf.AdoDal.Interfaces
{
    public interface IStoreAdoRepository
    {
        Product? GetProductById(int id);
        OrderWithDetailsDto GetOrderWithDetailsById(int id);
        Product? GetProductByIdBySqlRawQuery(int id);
    }
}
