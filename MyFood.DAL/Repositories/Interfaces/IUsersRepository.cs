using System;
using System.Collections.Generic;
using MyFood.DAL.Models;

namespace MyFood.DAL.Repositories
{
    public interface IUsersRepository
    {
        User GetUserByLogin(string login);
        User RegisterUser(User newUser);
        List<User> GetAllUsers();
    }
}