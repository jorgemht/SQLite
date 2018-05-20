﻿namespace SQLite.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    public class DataStorage : IDataStorage
    {
        private readonly SQLiteAsyncConnection database;

        public DataStorage() => database = DependencyService.Get<ISqlite>().GetAsyncConnection();

        public async Task SaveValueAsync<T>(T value, bool overrideIfExists = true) where T : class, IKeyObject, new()
        {
            if (overrideIfExists) await database.InsertOrReplaceAsync(value);
            else
                try
                {
                    await database.InsertAsync(value);
                }
                catch (SQLiteException e)
                {
                    if (e.Result == SQLite3.Result.Constraint)
                        throw new Exception($"Element { value.Id } already exists");
                }
        }

        public async Task DeleteAsync<T>(T value) where T : class, IKeyObject, new() => await database.DeleteAsync(value);

        public async Task<List<T>> GetAllItemsAsync<T>() where T : class, IKeyObject, new() => await database.Table<T>().ToListAsync();

        public async Task<T> GetItemAsync<T>(string id) where  T : class, IKeyObject, new() =>  await database.FindAsync<T>(id);

        public async Task SaveAllAsync<T>(IEnumerable<T> values, bool overrideIfExists = true) where T : class, IKeyObject, new()
        {
            await database.DropTableAsync<T>();    // Delete table
            await database.CreateTableAsync<T>();  // Create table
            await database.InsertAllAsync(values); // InsertAll
        }
    }
}
