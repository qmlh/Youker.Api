using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Youker.Application
{
    public class LoginRequestDto
    {
        public string CustomerCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
