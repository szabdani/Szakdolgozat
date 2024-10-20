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
		private string connectionString = "Server=localhost;Port=3306;Database=diary_db;User=root;Password=;";

		public MySqlConnection GetConnection()
		{
			MySqlConnection connection = new MySqlConnection(connectionString);
			return connection;
		}

		public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
		{
			using (IDbConnection connection = GetConnection())
			{
				var rows = await connection.QueryAsync<T>(sql, parameters);

				return rows.ToList();
			}
		}

		public async Task SaveData<T>(string sql, T parameters)
		{
			using (IDbConnection connection = GetConnection())
			{
				await connection.ExecuteAsync(sql, parameters);
			}
		}
	}
}