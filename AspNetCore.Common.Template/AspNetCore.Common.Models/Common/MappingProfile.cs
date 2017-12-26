using AspNetCore.Common.Models.Identity;
using AspNetCore.Common.Models.Identity.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCore.Common.Models.Common
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateCommonMap();
        }

        private void CreateCommonMap()
        {
            CreateMap<Menu, MenuViewModel>();
        }
    }
}
