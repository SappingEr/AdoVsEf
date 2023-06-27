using Microsoft.Data.SqlClient;
using System.Data;

namespace AdoVsEf.AdoDal.DataAccess
{
	public class DataAccess
	{
		private readonly string _connectionString;

		public DataAccess(string connectionString)
		{
			_connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
		}

		public T ExecuteCustomQuery<T>(
			string storedProcedureName,
			Func<SqlDataReader, T> executeRead,
			SqlParameter[]? parameters = null)
		{
			using var connection = new SqlConnection(_connectionString);
			connection.Open();

			var sqlCommand = new SqlCommand
			{
				Connection = connection,
				CommandText = storedProcedureName,
				CommandType = CommandType.StoredProcedure
			};

			AddParameters(parameters, sqlCommand);

			using var sqlDataReader = sqlCommand.ExecuteReader();

			return executeRead.Invoke(sqlDataReader);
		}

		public T ExecuteCustomRawQuery<T>(
			string query,
			Func<SqlDataReader, T> executeRead,
			SqlParameter[]? parameters = null)
		{
			using var connection = new SqlConnection(_connectionString);
			connection.Open();

			var sqlCommand = new SqlCommand
			{
				Connection = connection,
				CommandText = query,
				CommandType = CommandType.Text
			};

			AddParameters(parameters, sqlCommand);

			using var sqlDataReader = sqlCommand.ExecuteReader();

			return executeRead.Invoke(sqlDataReader);
		}

		private void AddParameters(SqlParameter[]? parameters, SqlCommand sqlCommand)
		{
			if (parameters != null && parameters.Any())
				sqlCommand.Parameters.AddRange(parameters);
		}
	}
}