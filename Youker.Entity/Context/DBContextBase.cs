using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Youker.Entity.Context
{
    public interface IDbConnectionProvider
    {
        IDbConnection Connection { get; }
    }
    public class DBContextBase : IDbConnectionProvider
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;
        public DBContextBase()
        {
            _connection = new SqlConnection("Data Source=122.51.15.158;Initial Catalog=Youker;user id=qun;Password=09dcsncm01;MultipleActiveResultSets=True;");
        }

        public IDbConnection Connection { get => _connection; }
        public IDbTransaction Transaction { get => _transaction; set => _transaction = value; }
    }

}
