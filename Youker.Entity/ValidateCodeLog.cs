using System;
using System.Collections.Generic;
using System.Text;

namespace Youker.Entity
{
    public class ValidateCodeLog
    {
        /// <summary>
        /// 验证码
        /// </summary>]
        public string ValidateCode { get; set; }
        /// <summary>
        /// 发送状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 记录创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    public class EmailValidateCodeLog : ValidateCodeLog
    {
        // <summary>
        /// 发送邮箱
        /// </summary>
        public string Email { get; set; }
    }

    public class PhoneValidateCodeLog : ValidateCodeLog
    {
        // <summary>
        /// 发送号码
        /// </summary>
        public string Phone { get; set; }
    }
}
