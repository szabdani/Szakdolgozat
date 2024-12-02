using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using LifeTrackerDatabaseAPI.Intefaces;
using MySql.Data.MySqlClient;

namespace LifeTrackerDatabaseAPI.Services
{
    public class DataAccess : IDataAccess
	{
		private readonly string _connectionString;

		public DataAccess(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DefaultConnection")!;
		}

		private MySqlConnection GetConnection() => new(_connectionString);

		public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
		{
			using (IDbConnection connection = GetConnection())
			{
				try
				{
					var rows = await connection.QueryAsync<T>(sql, parameters);
					return rows.ToList();
				}
				catch (Exception e)
				{
                    throw new Exception($"Error loading data from the database: {e.Message}");
				}
				
			}
		}

		public async Task<int> SaveData<T>(string sql, T parameters)
		{
			using (IDbConnection connection = GetConnection())
			{
				try
				{
					return await connection.ExecuteAsync(sql, parameters);
				}
				catch (Exception e)
				{
					throw new Exception($"Error saving data to the database: {e.Message}");
                }
				
			}
        }
	}
}