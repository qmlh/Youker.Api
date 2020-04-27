using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Youker.Entity.Context;

namespace Youker.Repository
{
    public class BaseRepository
    {
        protected IDbConnection _connection;
        protected IDbTransaction _transaction;

        public DBContextBase _context { get; }

        public BaseRepository(DBContextBase dbProvider)
        {
            _connection = dbProvider.Connection;
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            _transaction = dbProvider.Transaction;
            _context = dbProvider;
        }

        public IDbConnection Connection { get => _connection; }
        public IDbTransaction Transaction { get => _transaction; set { _transaction = value; _context.Transaction = value; } }

    }
}
