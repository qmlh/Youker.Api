﻿using System;
using System.Collections.Generic;
using System.Text;
using Youker.Entity;
using Youker.Entity.Context;
using Youker.Repository;

namespace Youker.Service
{
    public class LicensesService : BaseService
    {
        protected LicensesRepository _licensesRepository;
        public LicensesService(YoukerContext context, LicensesRepository licensesRepository) : base(context)
        {
            if (_licensesRepository == null)
                _licensesRepository = licensesRepository;
        }

        public List<Licenses> GetLicenses(int user_id)
        {
            return _licensesRepository.GetLicenses(user_id);
        }

        public List<Licenses> GetAllLicenses(int user_id)
        {
            return _licensesRepository.GetAllLicenses(user_id);
        }

        public bool CheckQuantity(int license_id)
        {
            return _licensesRepository.CheckQuantity(license_id);
        }

        public bool AssignLicenses(int device_id, int license_id)
        {
            return _licensesRepository.AssignLicenses(device_id, license_id);
        }


    }
}
