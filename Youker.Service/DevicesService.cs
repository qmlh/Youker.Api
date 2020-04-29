using System;
using System.Collections.Generic;
using System.Text;
using Youker.Application.Devices;
using Youker.Entity;
using Youker.Entity.Context;
using Youker.Repository;

namespace Youker.Service
{
    public class DevicesService : BaseService
    {
        protected DevicesRepository _devicesRepository;
        public DevicesService(YoukerContext context, DevicesRepository devicesRepository) : base(context)
        {
            if (_devicesRepository == null)
                _devicesRepository = devicesRepository;
        }

        public List<Devices> GetDevices(string mac, string key, string password, string is_active, string subdomain,int pageIndex, int pageSize, out int pageCount)
        {
            return _devicesRepository.GetDevices(mac, key, password, is_active, subdomain, pageIndex, pageSize, out pageCount);
        }

        /// <summary>
        /// 获取未分配的设备
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public List<Devices> GetUnassignedDevices()
        {
            return _devicesRepository.GetUnassignedDevices();
        }

        public Devices GetDevicesById(int device_id)
        {
            return _devicesRepository.GetDevicesById(device_id);
        }

        public bool DeleteDevice(int device_id)
        {
            return _devicesRepository.DeleteDevice(device_id);
        }
        public bool EditDevicePwd(EditDevicePwdDto editDevicePwdDto )
        {
            return _devicesRepository.EditDevicePwd(editDevicePwdDto);
        }

        public bool Activate(int license_id)
        {
            return _devicesRepository.Activate(license_id);
        }

    }
}
