using System;
using System.Collections.Generic;
using System.Text;

namespace Youker.Application.Licenses
{
    public class AssignLicensesBatchDto
    {
        public List<int> device_ids { get; set; } 
        public List<license> license_ids { get; set; }
        public int is_active { get; set; }
    }

    public class license { 
        public int license_id { get; set; }
        public int count { get; set; }
    }
}
