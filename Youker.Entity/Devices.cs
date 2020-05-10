using System;
using System.Collections.Generic;
using System.Text;

namespace Youker.Entity
{
    public class Devices
    {
        public int device_id { get; set; }
        public string device_mac { get; set; }
        public string device_key { get; set; }
        public string device_user_password { get; set; }
        public string device_model_no { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        public int device_license_id { get; set; }
        public string device_subdomain { get; set; }
    }

    public class DevicesWithLicense : Devices
    {
        public string device_license_key { get; set; }
        public int device_license_is_active { get; set; }
    }
}
