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
            await Connection.ExecuteAsync(query, new
            {
                name = entity.Name,
                gender = entity.Gender,
                age = entity.Age,
                date_of_birth = entity.DateOfBirth,
                mobile_number = entity.MobileNumber,
                address = entity.Address,
                occupation = entity.Occupation,
                blood_group = entity.BloodGroup
            });
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
