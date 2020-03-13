using System;
using System.Collections.Generic;
using System.Text;

namespace WhoLives.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {

        void Save();
    }
}
