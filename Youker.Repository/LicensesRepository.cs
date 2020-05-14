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

        public List<Licenses> GetAllLicenses(int user_id)
        {
            string execSp = "cp_API_Licenses_List_All";
            return _connection.Query<Licenses>(execSp, new { user_id }, null, true, null, commandType: CommandType.StoredProcedure).ToList();
        }

        /// <summary>
        /// 确认授权码的剩余数量
        /// </summary>
        /// <param name="license_id"></param>
        /// <returns></returns>
        public bool CheckQuantity(int license_id)
        {
            string execSp = "cp_API_Licenses_CheckQuantity";
            return _connection.Query<Licenses>(execSp, new { license_id }, null, true, null, commandType: CommandType.StoredProcedure).ToList().Count() > 0 ? true : false;
        }

        /// <summary>
        /// 确认授权码的剩余数量
        /// </summary>
        /// <param name="license_id"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool CheckQuantity(int license_id, int count)
        {
            string execSp = "cp_API_Licenses_CheckQuantity";
            var result = _connection.ExecuteScalar<int>(execSp, new { license_id }, null, null, commandType: CommandType.StoredProcedure);
            return result >= count ? true : false;
        }
        
        /// <summary>
        /// 分配授权码
        /// </summary>
        /// <param name="device_id"></param>
        /// <param name="license_id"></param>
        /// <returns></returns>
        public bool AssignLicenses(int device_id, int license_id)
        {
            string execSp = "cp_API_Licenses_Assign";
            return _connection.Execute(execSp, new { device_id, license_id }, null, null, commandType: CommandType.StoredProcedure) > 0 ? true : false;
        }

        /// <summary>
        /// 批量分配授权码
        /// </summary>
        /// <param name="device_ids"></param>
        /// <param name="license_id"></param>
        /// <returns></returns>
        public bool AssignLicensesBatch(List<int> device_ids, List<int> license_ids, int is_active)
        {
            string device_id = string.Join(",", device_ids);
            string license_id = string.Join(",", license_ids);
            string execSp = "cp_API_Licenses_Assign_Batch";
            return _connection.Execute(execSp, new { device_ids=device_id, license_ids=license_id, is_active }, null, null, commandType: CommandType.StoredProcedure) > 0 ? true : false;
        }


    }
}
