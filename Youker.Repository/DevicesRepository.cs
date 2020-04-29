using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Youker.Application.Devices;
using Youker.Entity;
using Youker.Entity.Context;

namespace Youker.Repository
{
    public class DevicesRepository : BaseRepository
    {
        public DevicesRepository(YoukerContext dbProvider) : base(dbProvider)
        { 
            
        }

        public List<Devices> GetDevices(string mac, string key, string password, string is_active, string subdomain,int pageIndex, int pageSize, out int pageCount)
        {
            string execSp = "cp_API_Devices_List";
            var parems = new DynamicParameters();//建立一个parem对象
            parems.Add("@device_mac", mac);
            parems.Add("@device_key", key);
            parems.Add("@device_user_password", password);
            parems.Add("@is_active", is_active);
            parems.Add("@device_subdomain", subdomain);
            parems.Add("@page_size", pageSize);
            parems.Add("@page_index", pageIndex);
            parems.Add("@page_count", 0, DbType.Int32, ParameterDirection.Output);//输出返回值
            var list = _connection.Query<Devices>(execSp, parems, null, true, null, commandType: CommandType.StoredProcedure).ToList();
            pageCount = parems.Get<int>("@page_count");
            return list;
        }

        public List<Devices> GetUnassignedDevices()
        {
            string execSp = "cp_API_Devices_List_User_Unassigned";
            return _connection.Query<Devices>(execSp, new { }, null, true, null, commandType: CommandType.StoredProcedure).ToList();
        }
        public Devices GetDevicesById(int device_id)
        {
            string execSp = "cp_API_Devices_Info";
            return _connection.Query<Devices>(execSp, new { device_id }, null, true, null, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public bool DeleteDevice(int device_id)
        {
            string execSp = "cp_API_Devices_Delete";
            return _connection.Execute(execSp, new { device_id }, null, null, commandType: CommandType.StoredProcedure) > 0 ? true : false;
        }

        public bool EditDevicePwd(EditDevicePwdDto editDevicePwdDto)
        {
            string execSp = "cp_API_Devices_EditDevicePwd";
            return _connection.Execute(execSp, new { editDevicePwdDto.device_id, editDevicePwdDto.device_user_password }, null, null, commandType: CommandType.StoredProcedure) > 0 ? true : false;
        }

        public bool Activate(int license_id)
        {
            string execSp = "cp_API_Devices_Activate";
            return _connection.Execute(execSp, new { license_id }, null, null, commandType: CommandType.StoredProcedure) > 0 ? true : false;
        }
    }
}
