using FougeraClub.Core.Models;
using FougeraClub.Services.DTOs.UserDtos;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.Mapping
{
    public class UserMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<User , UserDto>(); 
        }
    }
}
