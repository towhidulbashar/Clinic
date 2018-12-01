using System;
using System.Data;
using Clinic.Api.Core;
using Clinic.Api.Core.Repositories;
using Npgsql;

namespace Clinic.Api.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Func<IDbTransaction, IPatientRepository> patientRepositoryFactory;
        private IPatientRepository patientRepository = null;
        private readonly IDbTransaction transaction;
        private readonly IDbConnection connection;
        public UnitOfWork(string connectionString, Func<IDbTransaction, IPatientRepository> patientRepositoryFactory)
        {
            try
            {
                Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
                connection = new NpgsqlConnection(connectionString);
                connection.Open();
                transaction = connection.BeginTransaction();
                this.patientRepositoryFactory = patientRepositoryFactory;
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
        
        public IPatientRepository PatientRepository
        {
            get
            {
                if (patientRepository == null)
                    this.patientRepository = this.patientRepositoryFactory(transaction);
                return patientRepository;
            }
        }
    }
}
