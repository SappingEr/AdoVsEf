using AdoVsEf.AdoDal.Interfaces;
using AdoVsEf.AdoDal.Services;

namespace AdoVsEf.AdoDal.Tests
{
    internal class StoreAdoRepositoryTests
    {
        private IStoreAdoRepository? _storeRepository;

        [SetUp]
        public void Setup()
        {
            var connectionString = TestHelpers.GetConnectionString();
            var dataAccess = new DataAccess.DataAccess(connectionString);
            _storeRepository = new StoreAdoRepository(dataAccess);
        }

        [Test]
        public void GetProductById_Id_EntityNotNull()
        {
            var product = _storeRepository!.GetProductById(5);

            Assert.That(product, Is.Not.Null);
        }

        [Test]
        public void GetOrdersWithDetails_Id_EntityNotNull()
        {
            var orderWithDetails = _storeRepository!.GetOrderWithDetailsById(11077);

            Assert.That(orderWithDetails, Is.Not.Null);
        }

        [Test]
        public void GetProductByIdBySqlRawQuery_Id_EntityNotNull()
        {
	        var product = _storeRepository!.GetProductByIdBySqlRawQuery(45);

	        Assert.That(product, Is.Not.Null);
        }
	}
}
