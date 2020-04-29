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
    public class LicensesController : ControllerBase
    {
        public readonly LicensesService _licensesService;
        public LicensesController(LicensesService licensesService)
        {
            _licensesService = licensesService;
        }

        /// <summary>
        /// 获取用户Licenses
        /// </summary>
        /// <returns></returns>
        [HttpGet("List")]
        public IActionResult GetLicenses()
        {
            int user_id = 0;

            return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "查询成功", ResponseData = null });
        }
    }
}