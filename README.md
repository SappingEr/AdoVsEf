# AdoVsEF

AdoVsEF is a performance comparison project that benchmarks different Data Access Layers (DALs) in .NET. The project includes implementations of ADO.NET, Dapper, and Entity Framework Core. The goal is to assess and compare the performance of different queries in each DAL, or to check the performance of different variants of the same query.

## Project Structure

The project is organized into several sub-projects:

- ADO.NET project
- Dapper project
- Entity Framework Core project
- BenchmarkDotNet project

Each project represents a separate DAL implementation. The BenchmarkDotNet project is used to test and compare the performance of these implementations.

## Getting Started

Open the solution in your IDE and explore the projects.

## Running the Benchmarks

Run the benchmark tests in release mode, which is recommended for accurate performance results.

## Benchmark Results

The benchmarks compare the performance of ADO.NET, Dapper, and Entity Framework Core.

## Contributing

Contributions are welcome. If you find a bug or have a suggestion for improving the project, please open an issue or submit a pull request.

