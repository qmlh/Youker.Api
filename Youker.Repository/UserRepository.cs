using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Youker.Entity;
using System.Linq;
using Dapper;
using Youker.Entity.Context;
using Youker.Application;

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

        public bool Register(RegisterDto registerDto)
        {
            string execSp = "cp_API_User_Register";
            return _connection.Execute(execSp, new {
                registerDto.customer_id,
                registerDto.user_code,
                registerDto.user_name,
                registerDto.password,
                registerDto.email,
                registerDto.mobile,
                registerDto.telephone,
                registerDto.website,
                registerDto.country_id
            }, null, null, commandType: CommandType.StoredProcedure) > 0 ? true : false;
        }

        public bool CheckEmail(string email)
        {
            string execSp = "cp_API_User_CheckEmail";
            var result = _connection.Query<User>(execSp, new { email }, null, true, null, commandType: CommandType.StoredProcedure).FirstOrDefault();
            if (result !=null) {
                return true;
            }
            return false;
        }

        public bool CreateEmailLog(EmailValidateCodeLog log)
        {
            string execSp = "cp_API_User_CreateEmailLog";
            return _connection.Execute(execSp, new { log.Email, log.Status, log.ValidateCode }, null, null, commandType: CommandType.StoredProcedure) > 0 ? true : false;
        }
        
        public EmailValidateCodeLog GetEmailLog(string email)
        {
            string execSp = "cp_API_User_GetEmailLog";
            return _connection.Query<EmailValidateCodeLog>(execSp, new { email, time = DateTime.Now.ToString("yyyyMMdd") }, null, true, null, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public bool ChangePwdByEmail(string email,string password)
        {
            string execSp = "cp_API_User_ChangePwdByEmail";
            return _connection.Execute(execSp, new { email, password }, null, null, commandType: CommandType.StoredProcedure) > 0 ? true : false;
        }

        public User GetUserInfoByUserId(int user_id)
        {
            string execSp = "cp_API_User_GetUserInfoByUserId";
            return _connection.Query<User>(execSp, new { user_id }, null, true, null, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public bool EditUserInfo(int user_id,EditUserInfoDto editUserInfoDto)
        {
            string execSp = "cp_API_User_EditUserInfo";
            return _connection.Execute(execSp, new
            {
                user_id,
                editUserInfoDto.user_code,
                editUserInfoDto.user_name,
                editUserInfoDto.email,
                editUserInfoDto.mobile,
                editUserInfoDto.telephone,
                editUserInfoDto.website,
                editUserInfoDto.country_id
            }, null, null, commandType: CommandType.StoredProcedure) > 0 ? true : false;
        }

        public List<CustomerDto> GetCustomer()
        {
            string execSp = "cp_API_User_GetCustomer";
            return _connection.Query<CustomerDto>(execSp, new { }, null, true, null, commandType: CommandType.StoredProcedure).ToList();
        }

        public List<CountryDto> GetCountry()
        {
            string execSp = "cp_API_User_GetCountry";
            return _connection.Query<CountryDto>(execSp, new { }, null, true, null, commandType: CommandType.StoredProcedure).ToList();
        }


    }
}
