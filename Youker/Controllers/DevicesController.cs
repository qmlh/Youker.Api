using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Youker.Common;
using Youker.Common.Enum;
using Youker.Service;

namespace Youker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DevicesController : ControllerBase
    {
        public readonly DevicesService _devicesService;
        public DevicesController(DevicesService devicesService)
        {
            _devicesService = devicesService;
        }

        /// <summary>
        /// 获取设备列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("list")]
        public IActionResult GetDevices()
        {
            var result = _devicesService.GetDevices();
            return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "查询成功", ResponseData = result });
        }

    }
}