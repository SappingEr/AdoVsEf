using AdoVsEf.Dapper.Interfaces;
using AdoVsEf.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using AdoVsEf.Dto;

namespace AdoVsEf.Dapper.Services
{
	public class StoreDapperRepository : IStoreDapperRepository
	{
		private const string GetProductByIdProcedure = "[dbo].[GetProductById]";
		private const string GetOrderWithDetailsByIdProcedure = "[dbo].[GetOrderWithDetailsById]";
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
		private readonly string _connectionString;

		public StoreDapperRepository(string connectionString)
		{
			_connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
		}

		private DbConnection OpenConnection()
		{
			var connection = new SqlConnection(_connectionString);
			connection!.ConnectionString = _connectionString;
			connection.Open();
			return connection;
		}

		public Product? GetProductById(int id)
		{
			using var connection = OpenConnection();
			return connection.QueryFirst<Product>(
				GetProductByIdProcedure,
				new { ProductId = id },
				null,
				null,
				CommandType.StoredProcedure);
		}

		public Product? GetProductBySqlRawQuery(int id)
		{
			using var connection = OpenConnection();
			return connection.QueryFirst<Product>(GetProductByIdQuery, new { ProductId = id });
		}

		public OrderWithDetailsDto? GetOrderWithDetailsById(int id)
		{
			using var connection = OpenConnection();
			using var multipleReader = connection.QueryMultiple(
				GetOrderWithDetailsByIdProcedure,
				new { OrderId = id },
				null,
				null,
				CommandType.StoredProcedure);

			var order = multipleReader.Read<Order>().FirstOrDefault();
			var details = multipleReader.Read<OrderDetailsDto>().ToList();
			return new OrderWithDetailsDto { Order = order, Details = details };
		}
	}
}
