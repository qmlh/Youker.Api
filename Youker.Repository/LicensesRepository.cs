using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Youker.Entity;
using Youker.Entity.Context;

namespace Youker.Repository
{
    public class LicensesRepository : BaseRepository
    {
        public LicensesRepository(YoukerContext dbProvider) : base(dbProvider)
        {

        }

        public List<Licenses> GetLicenses(int user_id)
        {
            string execSp = "cp_API_Licenses_List";
            return _connection.Query<Licenses>(execSp, new { user_id }, null, true, null, commandType: CommandType.StoredProcedure).ToList();
        }

        //分配授权码
        public void AssignLicenses()
        { 
            
        }
    }
}
