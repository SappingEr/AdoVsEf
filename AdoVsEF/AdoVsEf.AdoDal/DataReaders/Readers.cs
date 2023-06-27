using AdoVsEf.Dto;
using AdoVsEf.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AdoVsEf.AdoDal.DataReaders
{
	internal static class Readers
	{
		public static Product ReadProduct(this SqlDataReader reader)
		{
			return new Product
			{
				ProductId = reader.GetInt32(nameof(Product.ProductId)),
				CategoryId = reader.GetInt32(nameof(Product.CategoryId)),
				SupplierId = reader.GetInt32(nameof(Product.SupplierId)),
				Discontinued = reader.GetBoolean(nameof(Product.Discontinued)),
				ProductName = reader.GetString(nameof(Product.ProductName)),
				QuantityPerUnit = reader.GetString(nameof(Product.QuantityPerUnit)),
				ReorderLevel = reader.GetInt16(nameof(Product.ReorderLevel)),
				UnitPrice = reader.GetDecimal(nameof(Product.UnitPrice)),
				UnitsInStock = reader.GetInt16(nameof(Product.UnitsInStock)),
				UnitsOnOrder = reader.GetInt16(nameof(Product.UnitsOnOrder))
			};
		}

		public static Order ReadOrder(this SqlDataReader reader)
		{
			return new Order
			{
				OrderId = reader.GetInt32(nameof(Order.OrderId)),
				OrderDate = reader.GetDateTime(nameof(Order.OrderDate)),
				RequiredDate = reader.GetDateTime(nameof(Order.RequiredDate)),
				Freight = reader.GetDecimal(nameof(Order.Freight)),
				ShipName = reader.GetString(nameof(Order.ShipName)),
				ShipAddress = reader.GetString(nameof(Order.ShipAddress)),
				ShipCity = reader.GetString(nameof(Order.ShipCity)),
				ShipRegion = reader.GetString(nameof(Order.ShipRegion)),
				ShipPostalCode = reader.GetString(nameof(Order.ShipPostalCode)),
				ShipCountry = reader.GetString(nameof(Order.ShipCountry))
			};
		}

		public static OrderDetailsDto ReadOrderDetailsDto(this SqlDataReader reader)
		{
			return new OrderDetailsDto
			{
				OrderId = reader.GetInt32(nameof(OrderDetailsDto.OrderId)),
				ProductName = reader.GetString(nameof(OrderDetailsDto.ProductName)),
				CategoryName = reader.GetString(nameof(OrderDetailsDto.CategoryName)),
				UnitPrice = reader.GetDecimal(nameof(OrderDetailsDto.UnitPrice)),
				Quantity = reader.GetInt16(nameof(OrderDetailsDto.Quantity)),
				Discount = reader.GetFloat(nameof(OrderDetailsDto.Discount))
			};
		}
	}
}
