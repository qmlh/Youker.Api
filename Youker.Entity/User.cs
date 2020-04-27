using System;
using System.Collections.Generic;
using System.Text;

namespace Youker.Entity
{
    public class User
    {
        public int user_id { get; set; }
        public int customer_id { get; set; }
        public string user_code { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public int country_id { get; set; }
        public string telephone { get; set; }
        public string mobile { get; set; }
        public int language_id { get; set; }
        public int user_level { get; set; }
        public string user_type { get; set; }
        public int is_active { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        public int create_user_id { get; set; }
        public int update_user_id { get; set; }
        public int function_group_id { get; set; }
        public int device_group_id { get; set; }
    }
}
