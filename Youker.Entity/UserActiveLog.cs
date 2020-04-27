using System;
using System.Collections.Generic;
using System.Text;

namespace Youker.Entity
{
    public class UserActiveLog
    {
        public int log_id { get; set; }
        public int user_id { get; set; }
        public DateTime login_time { get; set; }
        public string login_ip { get; set; }
        public string remote_host { get; set; }
        public string user_agent { get; set; }
    }
}
