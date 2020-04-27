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
    public class DevicesRepository : BaseRepository
    {
        public DevicesRepository(YoukerContext dbProvider) : base(dbProvider)
        { 
            
        }

        public List<Devices> GetDevices()
        {
            string execSp = "cp_API_Devices_List";
            return _connection.Query<Devices>(execSp, new { }, null, true, null, commandType: CommandType.StoredProcedure).ToList();
        }

        public Devices GetDevicesById(int device_id)
        {
            string execSp = "cp_API_Devices_Info";
            return _connection.Query<Devices>(execSp, new { device_id }, null, true, null, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

    }
}
