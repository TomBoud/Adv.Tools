﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.Abstractions
{
    public interface IDbDataAccess
    {
        Task<List<T>> LoadData<T, U>(string sqlQuery, U parameters);
        Task SaveData<T>(string sqlQuery, T parameters);
        Task<List<T>> LoadDataSelectAll<T>(string databaseName);

        Task DeleteData<T, U>(string sqlQuery, U parameters);
        Task ExecuteWithTransaction(params Func<Task>[] tasks);
    }
}