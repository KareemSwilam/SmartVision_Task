using FougeraClub.Core.IRespository;
using FougeraClub.Services.DTOs.UserDtos;
using FougeraClub.Services.IServices;
using FougeraClub.Services.Result;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unit;
        public UserServices(IUnitOfWork unit)
        {
            _unit = unit;            
        }
        public async Task<CustomResult<UserDto>> GetUSer(int id)
        {
            var userExist = await _unit.User.Get(u =>  u.Id == id);
            if (userExist == null)
                return CustomResult<UserDto>.Failure(CustomError.NotFoundError("User Not Exist"));
           var userDto =  userExist.Adapt<UserDto>();
            return CustomResult<UserDto>.Success(userDto);

        }
    }
}
