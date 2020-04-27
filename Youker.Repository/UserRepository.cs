using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Youker.Entity;
using System.Linq;
using Dapper;
using Youker.Entity.Context;

namespace Youker.Repository
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(YoukerContext dbProvider) : base(dbProvider)
        { 
            
        }

        public User UserLogin(string customer_Code, string userName)
        {
            string execSp = "cp_API_User_UserLogin";
            return _connection.Query<User>(execSp, new { customer_Code,userName }, null, true, null, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }
        

    }
}
