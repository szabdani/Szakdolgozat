﻿namespace DataLibrary
{
	public interface IDataAccess
	{
		Task<List<T>> LoadData<T, U>(string sql, U parameters);
		Task SaveData<T>(string sql, T parameters);
	}
}