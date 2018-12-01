using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Clinic.Api.Core.Repositories;

namespace Clinic.Api.Core
{
    public interface IUnitOfWork
    {
        void Complete();
        IPatientRepository PatientRepository { get; }
    }
}
