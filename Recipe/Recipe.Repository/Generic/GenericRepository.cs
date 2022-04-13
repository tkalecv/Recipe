using Dapper;
using Recipe.Repository.Common.Generic;
using Recipe.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Recipe.Repository.Generic
{
    public class GenericRepository<T> : IGenericRepository<T>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenericRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(T entity)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var columns = GetColumns();
                var stringOfColumns = string.Join(", ", columns);
                var stringOfParameters = string.Join(", ", columns.Select(e => "@" + e));
                var query = $"INSERT INTO {typeof(T).Name} ({stringOfColumns}) values ({stringOfParameters})";

                await _unitOfWork.ExecuteQueryAsync(query, entity);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        public async Task CreateAsync(IEnumerable<T> entityList)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                foreach (T entity in entityList)
                {
                    //TODO: this wont get values for every entity
                    var columns = GetColumns();
                    var stringOfColumns = string.Join(", ", columns);
                    var stringOfParameters = string.Join(", ", columns.Select(e => "@" + e));
                    var query = $"INSERT INTO {typeof(T).Name} ({stringOfColumns}) values ({stringOfParameters})";

                    await _unitOfWork.ExecuteQueryAsync(query, entity);
                }

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var query = $"DELETE FROM {typeof(T).Name} WHERE {typeof(T).Name}ID = @{typeof(T).Name}ID";

                await _unitOfWork.ExecuteQueryAsync(query, entity);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(string where = null)
        {
            try
            {
                IEnumerable<T> entityList;

                _unitOfWork.BeginTransaction();

                var query = $"SELECT * FROM {typeof(T).Name} ";

                if (!string.IsNullOrWhiteSpace(where))
                    query += where;

                entityList = await _unitOfWork.ExecuteQueryAsync<T, dynamic>(query, new { });

                _unitOfWork.Commit();

                return entityList;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var columns = GetColumns();
                var stringOfColumns = string.Join(", ", columns.Select(e => $"{e} = @{e}"));
                var query = $"UPDATE {typeof(T).Name} SET {stringOfColumns} WHERE @{typeof(T).Name}ID = @{typeof(T).Name}ID";

                await _unitOfWork.ExecuteQueryAsync(query, entity);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        public async Task<IEnumerable<K>> LoadData<K, U>(string storedProcedure, U parameters)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                return await _unitOfWork.Connection.QueryAsync<K>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        public async Task SaveData<U>(string storedProcedure, U parameters)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                await _unitOfWork.Connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw ex;
            }
        }

        private IEnumerable<string> GetColumns()
        {
            return typeof(T)
                    .GetProperties()
                    .Where(e => e.Name.ToLower().Contains("id") && !e.PropertyType.GetTypeInfo().IsGenericType)
                    .Select(e => e.Name);
        }
    }
}
