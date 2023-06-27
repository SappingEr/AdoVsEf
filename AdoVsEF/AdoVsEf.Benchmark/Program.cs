using AdoVsEf.AdoDal.Interfaces;
using AdoVsEf.AdoDal.Services;
using AdoVsEf.Benchmark.Utils;
using AdoVsEf.Dapper.Interfaces;
using AdoVsEf.Dapper.Services;
using AdoVsEf.Dto;
using AdoVsEf.EfDal.Context;
using AdoVsEf.EfDal.Interfaces;
using AdoVsEf.EfDal.Services;
using AdoVsEf.Models;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace AdoVsEf.Benchmark;

[MemoryDiagnoser()]
[HideColumns(
	BenchmarkDotNet.Columns.Column.Error,
	BenchmarkDotNet.Columns.Column.RatioSD,
	BenchmarkDotNet.Columns.Column.StdDev,
	BenchmarkDotNet.Columns.Column.AllocRatio,
	BenchmarkDotNet.Columns.Column.Gen0,
	BenchmarkDotNet.Columns.Column.Gen1,
	BenchmarkDotNet.Columns.Column.Gen2)]

public class DataAccess
{
	private readonly string _connectionString;
	private readonly IStoreEfRepository _storeEfRepository;
	private readonly IStoreAdoRepository _storeAdoRepository;
	private readonly IStoreDapperRepository _storeDapperRepository;
	private readonly PooledDbContextFactory<StoreDbContext> _contextFactory;

	public DataAccess()
	{
		_connectionString = ConfigurationHelpers.GetConnectionString();
		_storeEfRepository = new StoreEfRepository(ConfigurationHelpers.GetStoreDbContext()) ??
							 throw new ArgumentNullException(nameof(StoreDbContext));
		_storeAdoRepository =
			new StoreAdoRepository(
				new AdoDal.DataAccess.DataAccess(_connectionString)) ??
			throw new ArgumentNullException(nameof(DataAccess));
		_storeDapperRepository = new StoreDapperRepository(_connectionString) ??
								 throw new ArgumentNullException(nameof(StoreDapperRepository));
		_contextFactory =
			new PooledDbContextFactory<StoreDbContext>(ConfigurationHelpers.GetDbContextOptionsBuilder().Options);
	}


	//[Benchmark]
	public Product? GetProductByIdAdoFromProcedure() => _storeAdoRepository.GetProductById(5);

	//[Benchmark]
	public Product? GetProductByIdAdoRawQuery() => _storeAdoRepository.GetProductByIdBySqlRawQuery(5);

	//[Benchmark]
	public Product? GetProductByIdDapperRawQuery() => _storeDapperRepository.GetProductBySqlRawQuery(5);

	//[Benchmark]
	public Product? GetProductByIdEfRawQuery() => _storeEfRepository.GetProductBySqlRawQuery(5);

	//[Benchmark]
	public Product? GetProductByIdEfNotTracked() => _storeEfRepository.GetProductByIdNotTracked(5);

	//[Benchmark]
	public Product? GetProductByIdEfTracked() => _storeEfRepository.GetProductByIdTracked(5);

	//[Benchmark]
	public Product? GetProductByIdAdoDataAccessByRequest()
	{
		var storeRepository = new StoreAdoRepository(new AdoDal.DataAccess.DataAccess(_connectionString));
		return storeRepository.GetProductByIdBySqlRawQuery(5);
	}


	//[Benchmark]
	public Product? GetProductByIdDapperRepositoryByRequest()
	{
		var storeDapperRepository = new StoreDapperRepository(_connectionString);
		return storeDapperRepository.GetProductBySqlRawQuery(5);
	}
	
	//[Benchmark]
	public Product? GetProductByIdEfPooledDbContextFactory()
	{
		using var context = _contextFactory.CreateDbContext();
		return context.Products.FirstOrDefault(p => p.ProductId == 5);
	}


	//[Benchmark]
	public Product? GetProductByIdEfPooledDbContextFactoryNotTracked()
	{
		using var context = _contextFactory.CreateDbContext();
		return context.Products.AsNoTracking().FirstOrDefault(p => p.ProductId == 5);
	}
	
	//[Benchmark]
	public Product? GetProductByIdRawQueryEfOneContextByRequestNotTracked()
	{
		var storeRepository = new StoreEfRepository(ConfigurationHelpers.GetStoreDbContext());
		return storeRepository.GetProductBySqlRawQuery(5);
	}
	
	//[Benchmark]
	public Product? GetProductByIdEfOneContextByRequest()
	{
		var context = ConfigurationHelpers.GetStoreDbContext();
		return context.Products.AsNoTracking().FirstOrDefault(p => p.ProductId == 5);
	}
	
	//[Benchmark]
	public Product? GetProductByIdEfWithCompiledQuery()
	{
		var context = ConfigurationHelpers.GetStoreDbContext();
		return context.GetProductByIdCompiled(5);
	}

	//[Benchmark]
	public Product? GetProductByIdDapper()
	{
		return _storeDapperRepository.GetProductById(5);
	}

	//[Benchmark]
	public OrderWithDetailsDto? GetOrderWithDetailsByIdEf()
	{
		var storeEfRepository = new StoreEfRepository(ConfigurationHelpers.GetStoreDbContext());
		return storeEfRepository.GetOrderWithDetailsById(11077);
	}

	//[Benchmark]
	public OrderWithDetailsDto? GetOrderWithDetailsByIdEf1()
	{
		var storeEfRepository = new StoreEfRepository(ConfigurationHelpers.GetStoreDbContext());
		return storeEfRepository.GetOrderWithDetailsById_1(11077);
	}
	
	//[Benchmark]
	public OrderWithDetailsDto? GetOrderWithDetailsByAdoSp()
	{
		return _storeAdoRepository.GetOrderWithDetailsById(11077);
	}
	
	//[Benchmark]
	public OrderWithDetailsDto? GetOrderWithDetailsByDapperSp()
	{
		return _storeDapperRepository.GetOrderWithDetailsById(11077);
	}

	//[Benchmark]
	public OrderWithDetailsDto? GetOrderWithDetailsByIdEf2()
	{
		var storeEfRepository = new StoreEfRepository(ConfigurationHelpers.GetStoreDbContext());
		return storeEfRepository.GetOrderWithDetailsById_2(11077);
	}
	
	//[Benchmark]
	public OrderWithDetailsDto? GetOrderWithDetailsByIdEfSplitQuery()
	{
		var storeEfRepository = new StoreEfRepository(ConfigurationHelpers.GetStoreDbContext());
		return storeEfRepository.GetOrderDetailsByIdSplitQuery(11077);
	}
}

public class Program
{
	public static void Main(string[] args)
	{
		var _ = BenchmarkRunner.Run<DataAccess>();
	}
}