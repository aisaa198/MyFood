using System;
using MyFood.DAL.Models;

namespace MyFood.DAL.Repositories
{
    public interface IUsersRepository
    {
        User GetUserById(Guid userId);
        User GetUserByLogin(string login);
        User RegisterUser(User newUser);
    }
}