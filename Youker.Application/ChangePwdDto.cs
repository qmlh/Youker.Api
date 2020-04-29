using System;
using System.Collections.Generic;
using System.Text;

namespace Youker.Application
{
    public class ChangePwdDto
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string NewPassword { get; set; }
    }
}
