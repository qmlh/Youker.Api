using System;
using System.Collections.Generic;
using System.Text;

namespace Youker.Common.Enum
{
    public enum ResponseCodeEnum
    {
        Success = 1001,
        /// <summary>
        /// 请求结果失败
        /// </summary>
        Fail = 1002,
        /// <summary>
        /// 请求异常
        /// </summary>
        Exception = 1003,
        /// <summary>
        /// 请求参数错误
        /// </summary>
        ParamError = 1004
    }
}
