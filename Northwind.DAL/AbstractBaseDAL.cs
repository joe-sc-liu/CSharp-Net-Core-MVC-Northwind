using System;
using Microsoft.Extensions.Options;
using Northwind.Util;

namespace Northwind.DAL
{
    public abstract class AbstractBaseDAL : IDisposable
    {
        protected readonly string _dbConnectionString;

        public AbstractBaseDAL(IOptionsSnapshot<Settings> settings)
        {
            _dbConnectionString = settings.Value.ConnectionStrings.NorthwindConnection;
            // Connect to Database...
        }

        public void Dispose()
        {
            // Disconnect from Database...
        }
    }
}
