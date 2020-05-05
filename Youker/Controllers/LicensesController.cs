using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Youker.Application.Licenses;
using Youker.Common;
using Youker.Common.Enum;
using Youker.Service;

namespace Youker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("CorsPolicy-public")]
    public class LicensesController : ControllerBase
    {
        public readonly LicensesService _licensesService;
        public LicensesController(LicensesService licensesService)
        {
            _licensesService = licensesService;
        }

        /// <summary>
        /// 获取用户可用Licenses
        /// </summary>
        /// <returns></returns>
        [HttpGet("List")]
        public IActionResult GetLicenses()
        {
            var result = _licensesService.GetLicenses(UserId);
            return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "查询成功", ResponseData = result });
        }

        /// <summary>
        /// 获取用户所有Licenses
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllList")]
        public IActionResult GetAllLicenses()
        {
            var result = _licensesService.GetAllLicenses(UserId);
            return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "查询成功", ResponseData = result });
        }

        /// <summary>
        /// 分配Licenses
        /// </summary>
        /// <param name="assignLicensesDto"></param>
        /// <returns></returns>
        [HttpPut("Assign")]
        public IActionResult AssignLicenses(AssignLicensesDto assignLicensesDto)
        {
            if (!_licensesService.CheckQuantity(assignLicensesDto.license_id))
            {
                return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Fail, ResponseMessage = "授权码可激活设备数量不足" });
            }
            var result = _licensesService.AssignLicenses(assignLicensesDto.device_id, assignLicensesDto.license_id);
            return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "分配成功", ResponseData = result });
        }

        private int UserId {
            get {
                var claimsIdentity = this.User.Identity as ClaimsIdentity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
                return Convert.ToInt32(userId);
            }
        }
    }
}