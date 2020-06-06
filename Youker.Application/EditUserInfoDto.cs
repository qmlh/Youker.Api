using System;
using System.Collections.Generic;
using System.Text;

namespace Youker.Application
{
    public class EditUserInfoDto
    {
        public string user_name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string telephone { get; set; }
        public string website { get; set; }
        public int country_id { get; set; }
    }

    public class EditUserInfoManageDto : EditUserInfoDto
    { 
        public int user_id { get; set; }
        public string password { get; set; }
        public int is_active { get; set; }
        public int user_level { get; set; }
    }
}
