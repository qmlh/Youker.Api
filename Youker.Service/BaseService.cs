using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Youker.Entity.Context;

namespace Youker.Service
{
    public class BaseService : IDisposable
    {
        public DBContextBase Context { get; }
        private bool _disposed;

        public BaseService(DBContextBase context)
        {
            Context = context;
            _disposed = false;
        }

        public bool Save()
        {
            if (Context.Transaction != null)
                Context.Transaction.Commit();
            return true;
        }
        public async Task<bool> SaveAsync()
        {
            await Task.Run(() =>
            {
                if (Context.Transaction != null)
                    Context.Transaction.Commit();
            });
            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Dispose(bool isDisposing)
        {
            if (!_disposed)
            {
                if (isDisposing)
                {
                    if (Context.Connection != null) Context.Connection.Dispose();
                    if (Context.Transaction != null) Context.Transaction.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
