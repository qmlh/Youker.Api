using System;
using System.Collections.Generic;
using System.Text;
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

        public List<Devices> GetDevices()
        {
            return _devicesRepository.GetDevices();
        }

        public Devices GetDevicesById(int device_id)
        {
            return _devicesRepository.GetDevicesById(device_id);
        }

    }
}
