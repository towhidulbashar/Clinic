using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Clinic.Authorize.Core.Repositories;

namespace Clinic.Authorize.Core
{
    public interface IUnitOfWork
    {
        void Complete();
        IUserRepository UserRepository { get; }
    }
}
