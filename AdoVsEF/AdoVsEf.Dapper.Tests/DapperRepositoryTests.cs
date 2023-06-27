using AdoVsEf.Dapper.Interfaces;
using AdoVsEf.Dapper.Services;

namespace AdoVsEf.Dapper.Tests
{
	public class Tests
	{
		private IStoreDapperRepository _repository = null!;

		[SetUp]
		public void Setup()
		{
			var connectionString = TestHelpers.GetConnectionString();
			_repository = new StoreDapperRepository(connectionString);
		}

		[Test]
		public void GetProductById_Id_EntityNotNull()
		{
			var product = _repository!.GetProductById(5);

			Assert.That(product, Is.Not.Null);
		}

		[Test]
		public void GetProductBySqlRawQuery_Id_EntityNotNull()
		{
			var product = _repository!.GetProductBySqlRawQuery(5);

			Assert.That(product, Is.Not.Null);
		}
		
		[Test]
		public void GetOrderWithDetailsById_Id_EntityNotNull()
		{
			var orderWithDetails = _repository!.GetOrderWithDetailsById(11077);

			Assert.That(orderWithDetails, Is.Not.Null);
		}
	}
}