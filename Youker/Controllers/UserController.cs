using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Youker.Application;
using Youker.Common;
using Youker.Common.Enum;
using Youker.Entity;
using Youker.Service;

namespace Youker.Api.Controllers
{
    /// <summary>
    /// 用户模块
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy-public")]
    public class UserController : ControllerBase
    {
        public readonly UserService _userService;
        public readonly TokenAuthenticationService _tokenAuthenticationService;
        public UserController(UserService userService, TokenAuthenticationService tokenAuthenticationService )
        {
            _userService = userService;
            _tokenAuthenticationService = tokenAuthenticationService;
        }

        #region 登陆注册
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("Login")]
        public IActionResult Login([FromBody]LoginRequestDto loginRequest)
        {
            var user = _userService.UserLogin(loginRequest.CustomerCode, loginRequest.UserName);
            if (user == null) {
                return BadRequest(new ResponseBody() { ResponseCode = ResponseCodeEnum.ParamError, ResponseMessage = "客户代码或用户代码错误" });
            }
            if (user.password != loginRequest.Password) {
                return BadRequest(new ResponseBody() { ResponseCode = ResponseCodeEnum.ParamError, ResponseMessage = "密码错误" });
            }
            string token;
            if (_tokenAuthenticationService.IsAuthenticated(user.user_id, out token)) {
                //记录

                return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success,
                    ResponseMessage = "请求成功",
                    ResponseData = new { 
                        token 
                    } 
                });
            }
            return BadRequest(new ResponseBody() { ResponseCode = ResponseCodeEnum.Fail, ResponseMessage = "请求失败" });
        }

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        [HttpGet("Session")]
        public IActionResult Session()
        {
            //var result = _tokenAuthenticationService.ReadToken();
            return Ok();
        }

        /// <summary>
        /// 注册页面初始化
        /// </summary>
        /// <returns></returns>
        [HttpGet("RegisterInit")]
        public IActionResult RegisterInit()
        {
            var country = _userService.GetCountry();
            var customer = _userService.GetCustomer();
            return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "请求成功",
                ResponseData = new RegisterInitDto {
                    customerList = customer,
                    coutryList = country
                }
            });

        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public IActionResult Register([FromBody]RegisterDto registerDto)
        {
            //是否已经注册
            
            if (_userService.Register(registerDto)) {
                return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "注册成功" });
            }
            return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Fail, ResponseMessage = "注册失败" });
        }
        #endregion

        #region 找回密码
        /// <summary>
        /// 找回密码（发送验证码）
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost("SendEmailCode")]
        public IActionResult SendCode(string email)
        {
            var reuslt = _userService.CheckEmail(email);
            if (reuslt) {
                //发送验证码
                var emailLog = _userService.GetEmailLog(email);
                if (emailLog != null) {
                    if (SMSHelper.CheckTime(emailLog.CreateTime) < 30)
                    {
                        return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Fail, ResponseMessage = "邮箱已发送" });
                    }
                }
                string validateCode = SMSHelper.GetValidateCode();
                var IsSendSuccess = SMSHelper.SendMail(validateCode, email);
                EmailValidateCodeLog emailValidateCodeLog = new EmailValidateCodeLog() {
                    Email = email,
                    Status = IsSendSuccess ? "SUCCESS" : "FAIL",
                    ValidateCode = validateCode
                };
                _userService.CreateEmailLog(emailValidateCodeLog);
                return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "邮件已发送" });
            }
            return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Fail, ResponseMessage = "邮箱尚未注册" });
        }

        /// <summary>
        /// 确认验证码，并且修改密码
        /// </summary>
        /// <param name="changePwdDto"></param>
        /// <returns></returns>
        [HttpPost("ChangePwd")]
        public IActionResult ChangePwd(ChangePwdDto changePwdDto)
        {
            changePwdDto.Email = changePwdDto.Email.Trim();
            //第一步 是否发送过短消息 查询最后一次发送短消息实体对象，验证是否超时
            EmailValidateCodeLog entity = _userService.GetEmailLog(changePwdDto.Email);
            if (entity != null)
            {
                if (SMSHelper.CheckTime(entity.CreateTime) > 30 * 60)
                {
                    return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Fail, ResponseMessage = "验证码已过期" });
                }
                if (entity.ValidateCode == changePwdDto.Code)
                {
                    //修改密码
                    var reuslt = _userService.ChangePwdByEmail(changePwdDto.Email, changePwdDto.NewPassword);
                    if (reuslt)
                    {
                        return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "修改密码成功" });
                    }
                    return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Fail, ResponseMessage = "修改密码失败" });
                }
                else
                {
                    return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Fail, ResponseMessage = "验证码错误" });
                }
            }
            return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Fail, ResponseMessage = "请求失败，请重新获取验证码" });
        }

        #endregion

        #region 编辑个人信息
        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("Info")]
        [Authorize]
        public IActionResult UserInfo()
        {
            var result = _userService.GetUserInfoByUserId(UserId);
            if (result!=null) {
                return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "请求成功" ,ResponseData = result });
            }
            return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "用户不存在" });
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="editUserInfoDto"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        [Authorize]
        public IActionResult EditUserInfo([FromBody]EditUserInfoDto editUserInfoDto)
        {
            var result = _userService.EditUserInfo(UserId, editUserInfoDto);
            if (result)
            {
                return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "修改成功" });
            }
            return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "修改失败" });
        }
        #endregion

        ///// <summary>
        ///// 用户Session
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("Session")]
        //[Authorize]
        //public IActionResult UserSession()
        //{
        //    return Ok();
        //}

        #region 用户管理
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("Management")]
        [Authorize]
        public IActionResult UserManagement()
        {
            var user = _userService.GetUserInfoByUserId(UserId);
            List<User> users = new List<User>();
            switch (user.user_type)
            {
                
                case "supper":
                case "admin":
                    users = _userService.GetUsers(user.customer_id);
                    return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "获取成功", ResponseData = users });
                case "sysadm":
                case "prdadm":
                case "prdowner":
                    users = _userService.GetUsers();
                    return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "获取成功", ResponseData = users });
                case "user":
                default:
                    return Unauthorized(new ResponseBody() { ResponseCode = ResponseCodeEnum.Fail, ResponseMessage = "没有权限" });
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        [HttpGet("Management_Info")]
        public IActionResult UserInfo(int user_id)
        {
            var user = _userService.GetUserInfoByUserId(user_id);
            return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "获取成功", ResponseData = user });
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="editUserInfoManageDto"></param>
        /// <returns></returns>
        [HttpPost("Management_Edit")]
        public IActionResult EditUserInfo([FromBody]EditUserInfoManageDto editUserInfoManageDto)
        {
            // 权限？
            var result = _userService.EditUserInfo(editUserInfoManageDto);
            if (result)
            {
                return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "修改成功" });
            }
            return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "修改失败" });
        }

        #endregion

        private int UserId
        {
            get
            {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                return Convert.ToInt32(userId);
            }
        }
    }


}