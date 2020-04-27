using System;
using System.Collections.Generic;
using System.Text;

namespace Youker.Entity
{
    public class Device_Licenses
    {
        public int device_license_id { get; set; }
        public string device_license_key { get; set; }
        public int device_license_is_active { get; set; }
        public DateTime device_license_start_date { get; set; }
        public DateTime device_license_end_date { get; set; }
        public int license_id { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
    }
}
