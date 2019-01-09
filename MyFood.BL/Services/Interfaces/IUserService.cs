using System;
using System.Collections.Generic;
using MyFood.BL.Models;

namespace MyFood.BL.Services
{
    public interface IUserService
    {
        List<UserDto> GetAllUsers();
        UserDto GetUserById(Guid userId);
        UserDto GetUserByLogin(string userLogin);
        UserDto RegisterUser(UserDto newUser);
    }
}