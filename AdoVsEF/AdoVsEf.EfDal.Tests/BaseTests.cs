using AdoVsEf.EfDal.Context;
using AdoVsEf.EfDal.Interfaces;
using AdoVsEf.EfDal.Services;
using NUnit.Framework;

namespace AdoVsEf.EfDal.Tests
{
    public class BaseTests
    {
        protected IStoreEfRepository _storeRepository;
        protected StoreDbContext _context;

        [SetUp]
        public void Setup()
        {
            _context = TestHelpers.GetContext();
            _storeRepository = new StoreEfRepository(_context);
        }

        [TearDown]
        public void CleanUp()
        {
            _context.Dispose();
        }
    }
}