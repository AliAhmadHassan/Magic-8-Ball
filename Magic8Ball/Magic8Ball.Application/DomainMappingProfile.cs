using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic8Ball.Application
{
    public class DomainMappingProfile : AutoMapper.Profile
    {
        public DomainMappingProfile()
        {
            DomainToDto();
        }

        public void DomainToDto()
        {

        }
    }
}
