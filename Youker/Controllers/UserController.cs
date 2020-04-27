using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Youker.Application;
using Youker.Common;
using Youker.Common.Enum;
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
            var user = _userService.UserLogin(loginRequest.Customer_Code, loginRequest.UserName);
            if (user == null) {
                return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.ParamError, ResponseMessage = "客户代码或用户代码错误" });
            }
            if (user.password != loginRequest.Password) {
                return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.ParamError, ResponseMessage = "密码错误" });
            }
            string token;
            if (_tokenAuthenticationService.IsAuthenticated(loginRequest, out token)) {
                return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success,
                    ResponseMessage = "请求成功",
                    ResponseData = new { 
                        token 
                    } 
                });
            }
            return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Fail, ResponseMessage = "请求失败" });
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="registerDto"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public IActionResult Register(RegisterDto registerDto)
        {
            return Ok();
        }
        #endregion

        #region 找回密码
        /// <summary>
        /// 找回密码（发送邮件）
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(string email)
        {
            return Ok();
        }

        /// <summary>
        /// 确认邮箱
        /// </summary>
        /// <param name="email"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost("ConfrimCode")]
        public IActionResult ConfrimCode(string email,string code)
        {
            return Ok();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost("ChangePwd")]
        public IActionResult ChangePwd(string email,string password)
        {
            return Ok();
        }

        #endregion

        #region 编辑个人信息
        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("Info")]
        //[Authorize]
        public IActionResult UserInfo(int userId)
        {
            return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "Info请求成功" });
        }

        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="editUserInfoDto"></param>
        /// <returns></returns>
        [HttpPut("Edit")]
        //[Authorize]
        public IActionResult EditUserInfo(EditUserInfoDto editUserInfoDto)
        {
            return Ok();
        }
        #endregion
    }


}