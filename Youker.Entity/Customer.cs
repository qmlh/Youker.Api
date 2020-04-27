using System;
using System.Collections.Generic;
using System.Text;

namespace Youker.Entity
{
    public class Customer
    {
        public int customer_id { get; set; }
        public string customer_code { get; set; }
        public string customer_name { get; set; }
        public int country_id { get; set; }
        public string website { get; set; }
        public string telephone { get; set; }
        public string uniform_number { get; set; }
        public int is_active { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        public int create_user_id { get; set; }
        public int update_user_id { get; set; }
    }
}
