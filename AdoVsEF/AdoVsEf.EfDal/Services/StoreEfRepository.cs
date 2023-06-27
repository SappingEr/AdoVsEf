using AdoVsEf.Dto;
using AdoVsEf.EfDal.Context;
using AdoVsEf.EfDal.Interfaces;
using AdoVsEf.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AdoVsEf.EfDal.Services
{
	public class StoreEfRepository : IStoreEfRepository
	{
		private readonly StoreDbContext _dbContext;

		private const string GetProductByIdQuery = """
        			SELECT [p].[ProductID]
        			      ,[p].[ProductName] 
        			      ,[p].[SupplierID] 
        			      ,[p].[CategoryID] 
        			      ,[p].[QuantityPerUnit] 
        			      ,[p].[UnitPrice] 
        			      ,[p].[UnitsInStock] 
        			      ,[p].[UnitsOnOrder] 
        			      ,[p].ReorderLevel 
        			      ,[p].[Discontinued] 
        			FROM [dbo].[Products] AS [p]
        			WHERE [p].[ProductID] = @ProductId
        """;

		public StoreEfRepository(StoreDbContext storeDbContext)
		{
			_dbContext = storeDbContext ?? throw new ArgumentNullException(nameof(storeDbContext));
		}

		public Product? GetProductByIdNotTracked(int id)
		{
			return _dbContext.Products.AsNoTracking().FirstOrDefault(p => p.ProductId == id);
		}

		public Product? GetProductByIdTracked(int id)
		{
			return _dbContext.Products.FirstOrDefault(p => p.ProductId == id);
		}

		public async Task<Product?> GetProductByIdAsync(int id)
		{
			return await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == id);
		}

		public OrderWithDetailsDto? GetOrderWithDetailsById(int id)
		{
			var orderWithDetails = _dbContext.Orders.AsNoTracking()
				.Where(o => o.OrderId == id)
				.Include(o => o.OrderDetails)!
				.ThenInclude(d => d.Product)
				.ThenInclude(p => p.Category)
				.FirstOrDefault();

			if (orderWithDetails == null)
				return null;

			return new OrderWithDetailsDto
			{
				Order = orderWithDetails,
				Details = orderWithDetails.OrderDetails!.Select(d => new OrderDetailsDto
				{
					ProductName = d.Product.ProductName,
					CategoryName = d.Product.Category!.CategoryName,
					UnitPrice = d.UnitPrice,
					Quantity = d.Quantity,
					Discount = d.Discount
				})
			};
		}

		public OrderWithDetailsDto? GetOrderWithDetailsById_1(int id)
		{
			return _dbContext.Orders.AsNoTracking()
				.Where(o => o.OrderId == id)
				.Select(o => new OrderWithDetailsDto
				{
					Order = o,
					Details = o.OrderDetails!.Join(
						_dbContext.Products,
						orderDetails => orderDetails.ProductId,
						product => product.ProductId,
						(d, p) => new OrderDetailsDto()
						{
							ProductName = p.ProductName,
							CategoryName = p.Category!.CategoryName,
							UnitPrice = d.UnitPrice,
							Quantity = d.Quantity,
							Discount = d.Discount
						})
				}).FirstOrDefault();
		}

		public OrderWithDetailsDto? GetOrderWithDetailsById_2(int id)
		{
			return _dbContext.Orders.AsNoTracking()
				.Where(o => o.OrderId == id)
				.Select(o => new OrderWithDetailsDto
				{
					Order = o,
					Details = o.OrderDetails!.Select(d => new OrderDetailsDto
					{
						ProductName = d.Product.ProductName,
						CategoryName = d.Product.Category!.CategoryName,
						UnitPrice = d.UnitPrice,
						Quantity = d.Quantity,
						Discount = d.Discount
					})
				}).FirstOrDefault();
		}

		public OrderWithDetailsDto? GetOrderDetailsByIdSplitQuery(int id)
		{
			return _dbContext.Orders.AsSplitQuery()
				.Where(o => o.OrderId == id)
				.Select(o => new OrderWithDetailsDto
				{
					Order = o,
					Details = o.OrderDetails!.Select(d => new OrderDetailsDto
					{
						ProductName = d.Product.ProductName,
						CategoryName = d.Product.Category!.CategoryName,
						UnitPrice = d.UnitPrice,
						Quantity = d.Quantity,
						Discount = d.Discount
					})
				}).FirstOrDefault();
		}

		public IEnumerable<Order>? GetTopOrderWithDetailsFullNotTracked(int count)
		{
			return _dbContext.Orders.AsNoTracking()
				.Include(o => o.OrderDetails)!
				.ThenInclude(d => d.Product)
				.Take(count).ToList();
		}

		public IEnumerable<Order>? GetTopOrderWithDetailsFullWithIdentityResolution(int count)
		{
			return _dbContext.Orders.AsNoTrackingWithIdentityResolution()
				.Include(o => o.OrderDetails)!
				.ThenInclude(d => d.Product)
				.Take(count).ToList();
		}

		public IEnumerable<Order>? GetTopOrderWithDetailsFull(int count)
		{
			return _dbContext.Orders.AsSplitQuery()
				.Include(o => o.OrderDetails)!
				.ThenInclude(d => d.Product)
				.Take(count).ToList();
		}

		public Product? GetProductBySqlRawQuery(int id)
		{
			var parameter = new SqlParameter("@ProductId", id);
			return _dbContext.Products.FromSqlRaw(GetProductByIdQuery, parameter).FirstOrDefault();
		}
	}
}