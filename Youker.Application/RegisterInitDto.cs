using System;
using System.Collections.Generic;
using System.Text;

namespace Youker.Application
{
    public class RegisterInitDto
    {
        public List<CustomerDto> customerList { get; set; }
        public List<CountryDto> coutryList { get; set; }
    }
}
