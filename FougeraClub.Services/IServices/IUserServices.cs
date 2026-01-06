using FougeraClub.Services.DTOs.UserDtos;
using FougeraClub.Services.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.IServices
{
    public interface IUserServices
    {
        public Task<CustomResult<UserDto>> GetUSer(int  id);
    }
}
