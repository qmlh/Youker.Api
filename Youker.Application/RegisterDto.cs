﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Youker.Application
{
    public class RegisterDto
    {
        public string customer_id { get; set; }
        public string user_code { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string telephone { get; set; }
        public string website { get; set; }
        public int country_id { get; set; }
    }
}
