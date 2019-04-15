using Clinic.Api.Core.Domain;
using Clinic.Api.Core.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Api.Persistance.Repositories
{
    public class PatientRepository : Repository<Patient>, IPatientRepository
    {
        public PatientRepository(IDbTransaction transaction) : base(transaction)
        {

        }
        public async override Task Add(Patient entity)
        {
            if (entity == null)
            {
                throw new ApplicationException("Can not add null patient.");
            }
            string query = $@"INSERT INTO patient (name, gender, age, date_of_birth, mobile_number, address, occupation, blood_group)
             VALUES ( @name, @gender, @age, @date_of_birth, @mobile_number, @address, @occupation, @blood_group);";
            var patientParameters = new DynamicParameters();
            patientParameters.Add("name", entity.Name, DbType.String, ParameterDirection.Input, 1024);
            patientParameters.Add("gender", entity.Gender, DbType.String, ParameterDirection.Input, 56);
            patientParameters.Add("age", entity.Age, DbType.Int16, ParameterDirection.Input);
            patientParameters.Add("date_of_birth", entity.DateOfBirth, DbType.DateTime, ParameterDirection.Input, 1024);
            patientParameters.Add("mobile_number", entity.MobileNumber, DbType.String, ParameterDirection.Input, 16);
            patientParameters.Add("address", entity.Address, DbType.String, ParameterDirection.Input, 10240);
            patientParameters.Add("occupation", entity.Occupation, DbType.String, ParameterDirection.Input, 1024);
            patientParameters.Add("blood_group", entity.BloodGroup, DbType.String, ParameterDirection.Input, 8);
            await Connection.ExecuteAsync(query, patientParameters);
        }

        public override void Add(IEnumerable<Patient> entities)
        {
            throw new NotImplementedException();
        }

        public async override Task<IEnumerable<Patient>> Get()
        {
            var patient = await Connection.QueryAsync<Patient>("SELECT * FROM patient");
            return patient;
        }

        public override Patient Get(long id)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Patient entity)
        {
            throw new NotImplementedException();
        }

        public override void Remove(IEnumerable<Patient> entities)
        {
            throw new NotImplementedException();
        }

        public override Patient SingleOrDefault(Expression<Func<bool, Patient>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
