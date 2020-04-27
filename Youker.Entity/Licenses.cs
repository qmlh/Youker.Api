using System;
using System.Collections.Generic;
using System.Text;

namespace Youker.Entity
{
    public class Licenses
    {
        public int license_id { get; set; }
        public int user_id { get; set; }
        public string license_key { get; set; }
        public int quantity { get; set; }
        public int quantity_used { get; set; }
        public int is_active { get; set; }
        public DateTime license_start_date { get; set; }
        public DateTime license_end_date { get; set; }
        public string order_id { get; set; }
        public DateTime create_time { get; set; }
    }
}
