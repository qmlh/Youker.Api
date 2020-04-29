using System;
using System.Collections.Generic;
using System.Text;

namespace Youker.Common
{
    public class SMSHelper
    {
        /// <summary>
        /// 生成6位随机验证码
        /// </summary>
        /// <returns></returns>
        public static string GetValidateCode()
        {
            Random ran = new Random();
            string CheakNum = ran.Next(0, 9) + "" + ran.Next(0, 9) + "" + ran.Next(0, 9) + "" + ran.Next(0, 9) + "" +
                              ran.Next(0, 9) + "" + ran.Next(0, 9);
            return CheakNum;
        }

        /// <summary>
        /// 发送邮箱验证码
        /// </summary>
        /// <param name="sendEmail"></param>
        public static bool SendMail(string code, string sendEmail)
        {

            string mailBody = "<body>" +
                "<table cellpadding = \"0\" cellspacing = \"0\" border =\"0\">" +
                "<tr><td height = '50'></td></tr>" +
                 "<tr><td style = \"font-size:17px; font-family:\"Microsoft Yahei\"; color:#121C35;\"> 已收到你的邮箱找回密码要求，请输入验证码：" + code + " ，改验证码30分钟内有效。</td></tr>" +
                 "<tr><td height = \"23\"></td></tr>" +
                  "<tr><td style = \"font-size:17px; font-family:'Microsoft Yahei'; color:#121C35;\"> 感谢对Youker的支持，希望你在Youker体验愉快。</td></tr>" +
                         "<tr><td height = \"23\" ></td></tr>" +
                            "<tr><td style = \"font-size:17px; font-family:'Microsoft Yahei'; color:#121C35;\"> --Youker </td></tr>" +
                               "<tr><td height = \"23\"></td></tr>" +
                                  "<tr><td style = \"font-size:17px; font-family:'Microsoft Yahei'; color:#121C35;\"> (这是一封自动产生的email，请勿回复。）</td></tr>" +
                                    "</table> ";
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.163.com");
            //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.exmail.qq.com");

            client.UseDefaultCredentials = true;//设置为发送认证消息
            client.Credentials = new System.Net.NetworkCredential("jikealert@163.com", "jikealert2019");
            System.Net.Mail.MailMessage mess = new System.Net.Mail.MailMessage();
            mess.From = new System.Net.Mail.MailAddress("jikealert@163.com", "youker");
            mess.To.Add(new System.Net.Mail.MailAddress(sendEmail));
            mess.Subject = "Youker-找回密码";
            mess.IsBodyHtml = true;
            mess.Body = mailBody;
            try
            {
                client.Send(mess);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static int CheckTime(DateTime previousTime)
        {
            TimeSpan timeSpan = DateTime.Now - previousTime;
            return (int)timeSpan.TotalSeconds;
        }
    }
}
