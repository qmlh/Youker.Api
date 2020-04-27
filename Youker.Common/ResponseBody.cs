using System;
using System.Collections.Generic;
using System.Text;
using Youker.Common.Enum;

namespace Youker.Common
{
    public class ResponseBody
    {
        public ResponseCodeEnum ResponseCode { get; set; }
        public string ResponseMessage { get; set; }

        public dynamic ResponseData { get; set; }
    }
}
