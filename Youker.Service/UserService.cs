using System;
using System.Collections.Generic;
using System.Text;
using Youker.Entity;
using Youker.Repository;
using Youker.Entity.Context;

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

    }
}
