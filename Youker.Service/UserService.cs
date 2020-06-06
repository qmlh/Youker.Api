using System;
using System.Collections.Generic;
using System.Text;
using Youker.Entity;
using Youker.Repository;
using Youker.Entity.Context;
using Youker.Application;

namespace Youker.Service
{
    public class UserService : BaseService
    {
        protected UserRepository _userRepository;
        public UserService(YoukerContext context, UserRepository userRepository) :base(context)
        {
            if (_userRepository == null)
                _userRepository = userRepository;
        }

        public User UserLogin(string customer_Code, string userName)
        {
            return _userRepository.UserLogin(customer_Code, userName);
        }

        public bool Register(RegisterDto registerDto)
        {
            return _userRepository.Register(registerDto);
        }

        public bool CheckEmail(string email)
        {
            return _userRepository.CheckEmail(email);
        }

        public bool CreateEmailLog(EmailValidateCodeLog emailValidateCodeLog)
        {
            return _userRepository.CreateEmailLog(emailValidateCodeLog);
        }

        public EmailValidateCodeLog GetEmailLog(string email)
        {
            return _userRepository.GetEmailLog(email);
        }

        public bool ChangePwdByEmail(string email,string password)
        {
            return _userRepository.ChangePwdByEmail(email,password);
        }
        public User GetUserInfoByUserId(int user_id)
        {
            return _userRepository.GetUserInfoByUserId(user_id);
        }

        public bool EditUserInfo(int user_id,EditUserInfoDto editUserInfoDto)
        {
            return _userRepository.EditUserInfo(user_id,editUserInfoDto);
        }

        public bool EditUserInfo(EditUserInfoManageDto editUserInfoManageDto)
        {
            return _userRepository.EditUserInfo(editUserInfoManageDto);
        }

        public List<CustomerDto> GetCustomer()
        {
            return _userRepository.GetCustomer();
        }

        public List<User> GetUsers(int customer_id)
        {
            return _userRepository.GetUsers(customer_id);
        }

        public List<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public List<CountryDto> GetCountry()
        {
            return _userRepository.GetCountry();
        }
    }
}
