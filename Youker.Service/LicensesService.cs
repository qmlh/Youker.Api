using System;
using System.Collections.Generic;
using System.Text;
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


    }
}
