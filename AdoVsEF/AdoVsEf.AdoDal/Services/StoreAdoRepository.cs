using System.Data;
using AdoVsEf.AdoDal.DataReaders;
using AdoVsEf.AdoDal.Interfaces;
using AdoVsEf.Dto;
using AdoVsEf.Models;
using Microsoft.Data.SqlClient;

namespace AdoVsEf.AdoDal.Services
{
	public class StoreAdoRepository : IStoreAdoRepository
	{
		private readonly DataAccess.DataAccess _dataAccess;
		private const string GetProductByIdProcedure = "[dbo].[GetProductById]";
		private const string GetOrderWithDetailsByIdProcedure = "[dbo].[GetOrderWithDetailsById]";

		private const string GetProductByIdQuery = $"""
														SELECT p.ProductID
														 	  ,p.ProductName
													     	  ,p.SupplierID 
													     	  ,p.CategoryID 
													     	  ,p.QuantityPerUnit
													     	  ,p.UnitPrice
													     	  ,p.UnitsInStock
													     	  ,p.UnitsOnOrder 
													     	  ,p.ReorderLevel
													     	  ,p.Discontinued
														FROM [dbo].[Products] AS p 
														WHERE p.ProductID = @ProductId
													""";

		public StoreAdoRepository(DataAccess.DataAccess dataAccess)
		{
			_dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
		}

		public Product? GetProductById(int id)
		{
			var parameters = new[]
			{
				new SqlParameter
				{
					ParameterName = "@ProductId",
					DbType = DbType.Int32,
					Value = id
				}
			};

			return _dataAccess.ExecuteCustomQuery(GetProductByIdProcedure, (reader) =>
			{
				Product product = null!;

				if (!reader.HasRows)
					return product;

				while (reader.Read()) product = reader.ReadProduct();

				return product;
			}, parameters);
		}

		public Product? GetProductByIdBySqlRawQuery(int id)
		{
			var parameters = new[]
				{ new SqlParameter { ParameterName = "@ProductId", DbType = DbType.Int32, Value = id } };

			return _dataAccess.ExecuteCustomRawQuery(GetProductByIdQuery, (reader) =>
			{
				Product product = null!;

				if (!reader.HasRows)
					return product;

				while (reader.Read())
				{
					product = reader.ReadProduct();
				}

				return product;
			}, parameters);
		}

		public OrderWithDetailsDto GetOrderWithDetailsById(int id)
		{
			var parameters = new[]
				{ new SqlParameter { ParameterName = "@OrderId", DbType = DbType.Int32, Value = id } };

			return _dataAccess.ExecuteCustomQuery(GetOrderWithDetailsByIdProcedure, (reader) =>
			{
				Order order = null!;

				if (!reader.HasRows)
					return null!;

				while (reader.Read())
				{
					order = reader.ReadOrder();
				}

				reader.NextResult();

				var details = new List<OrderDetailsDto>();

				while (reader.Read())
				{
					details.Add(reader.ReadOrderDetailsDto());
				}

				return new OrderWithDetailsDto { Order = order, Details = details };
			}, parameters);
		}
	}
}