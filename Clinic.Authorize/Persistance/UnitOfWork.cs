using System;
using System.Data;
using Clinic.Authorize.Core;
using Clinic.Authorize.Core.Repositories;
using Npgsql;

namespace Clinic.Authorize.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Func<IDbTransaction, IUserRepository> userRepositoryFactory;
        private IUserRepository userRepository = null;
        private readonly IDbTransaction transaction;
        private readonly IDbConnection connection;
        public UnitOfWork(string connectionString, Func<IDbTransaction, IUserRepository> userRepositoryFactory)
        {
            try
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                connection = new NpgsqlConnection(connectionString);
                connection.Open();
                transaction = connection.BeginTransaction();
                this.userRepositoryFactory = userRepositoryFactory;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public void Complete()
        {
            try
            {
                transaction.Commit();
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                throw exception;
            }
            finally
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
        
        public IUserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                    this.userRepository = this.userRepositoryFactory(transaction);
                return userRepository;
            }
        }
    }
}
