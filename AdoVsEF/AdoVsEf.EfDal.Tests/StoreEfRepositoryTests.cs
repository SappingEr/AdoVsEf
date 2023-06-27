using NUnit.Framework;

namespace AdoVsEf.EfDal.Tests
{
	internal class StoreEfRepositoryTests : BaseTests
	{
		[Test]
		public void GetOrderWithDetailsById_Id_EntityNotNull()
		{
			var orderWithDetails = _storeRepository.GetOrderWithDetailsById(11077);

			Assert.That(orderWithDetails, Is.Not.Null);
		}

		[Test]
		public void GetOrderWithDetailsById1_Id_EntityNotNull()
		{
			var orderWithDetails = _storeRepository.GetOrderWithDetailsById_1(11077);

			Assert.That(orderWithDetails, Is.Not.Null);
		}

		[Test]
		public void GetOrderWithDetailsById2_Id_EntityNotNull()
		{
			var orderWithDetails = _storeRepository.GetOrderWithDetailsById_2(11077);

			Assert.That(orderWithDetails, Is.Not.Null);
		}
		
		[Test]
		public void GetOrderDetailsByIdSplitQuery_Id_EntityNotNull()
		{
			var orderWithDetails = _storeRepository.GetOrderDetailsByIdSplitQuery(11077);

			Assert.That(orderWithDetails, Is.Not.Null);
		}

		[Test]
		public void GetProductBySqlRawQuery_Id_EntityNotNull()
		{
			var orderWithDetails = _storeRepository.GetProductBySqlRawQuery(5);

			Assert.That(orderWithDetails, Is.Not.Null);
		}

		[Test]
		public void GetTopOrderWithDetailsFullNotTracked_Id_CollectionIsNotEmpty()
		{
			var orders = _storeRepository.GetTopOrderWithDetailsFullNotTracked(10);
			
			CollectionAssert.IsNotEmpty(orders);
		}
		
		[Test]
		public void GetTopOrderWithDetailsFullWithIdentityResolution_Id_CollectionIsNotEmpty()
		{
			var orders = _storeRepository.GetTopOrderWithDetailsFullWithIdentityResolution(10);
			
			CollectionAssert.IsNotEmpty(orders);
		}
		
		[Test]
		public void GetTopOrderWithDetailsFull_Id_CollectionIsNotEmpty()
		{
			var orders = _storeRepository.GetTopOrderWithDetailsFull(10);
			
			CollectionAssert.IsNotEmpty(orders);
		}
		
		
	}
}