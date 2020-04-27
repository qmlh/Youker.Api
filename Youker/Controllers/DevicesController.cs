using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Youker.Application.Devices;
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
        [HttpGet("List")]
        public IActionResult GetDevices(string mac,string key,string password,string is_active,string subdomain,
            int pageIndex = 1,int pageSize = 10)
        {
            var result = _devicesService.GetDevices();
            return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "查询成功", ResponseData = result });
        }

        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <param name="device_id"></param>
        /// <returns></returns>
        [HttpGet("Info")]
        public IActionResult GetDeviceById(int device_id)
        {
            var result = _devicesService.GetDevicesById(device_id);
            return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "查询成功", ResponseData = result });
        }

        /// <summary>
        /// 删除设备
        /// </summary>
        /// <param name="device_id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public IActionResult DeleteDevice(int device_id)
        {
            if (_devicesService.DeleteDevice(device_id))
            {
                return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "删除成功" });
            }
            return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Fail, ResponseMessage = "删除失败" });
        }

        /// <summary>
        /// 修改设备密码
        /// </summary>
        /// <param name="editDevicePwdDto"></param>
        /// <returns></returns>
        [HttpPut("EditPwd")]
        public IActionResult EditDevicePwd(EditDevicePwdDto editDevicePwdDto)
        {
            if (_devicesService.EditDevicePwd(editDevicePwdDto))
            {
                return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "更新成功" });
            }
            return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Fail, ResponseMessage = "更新失败" });
        }

        /// <summary>
        /// 激活license
        /// </summary>
        /// <param name="license_id"></param>
        /// <returns></returns>
        [HttpPut("Activate")]
        public IActionResult Activate(int license_id)
        {
            if (_devicesService.Activate(license_id))
            {
                return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Success, ResponseMessage = "激活成功" });
            }
            return Ok(new ResponseBody() { ResponseCode = ResponseCodeEnum.Fail, ResponseMessage = "激活成功" });
        }

    }
}