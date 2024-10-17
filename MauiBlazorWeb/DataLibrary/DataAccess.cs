﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace DataLibrary
{
	public class DataAccess : IDataAccess
	{
		public async Task<List<T>> LoadData<T, U>(string sql, U parameters, string connectionString)
		{
			using (IDbConnection connection = new MySqlConnection(connectionString))
			{
				var rows = await connection.QueryAsync<T>(sql, parameters);

				return rows.ToList();
			}
		}

		public async Task SaveData<T>(string sql, T parameters, string connectionString)
		{
			using (IDbConnection connection = new MySqlConnection(connectionString))
			{
				await connection.ExecuteAsync(sql, parameters);
			}
		}
	}
}