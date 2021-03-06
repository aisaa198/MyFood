﻿using MyFood.DAL.Data;
using MyFood.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace MyFood.DAL.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        public User RegisterUser(User newUser)
        {
            using(var dbContext = new MyFoodDbContext())
            {
                var existingUser = dbContext.Users.SingleOrDefault(x => x.Login == newUser.Login);
                if(existingUser != null)
                {
                    return null;
                }

                var registeredUser = dbContext.Users.Add(newUser);
                dbContext.SaveChanges();
                return registeredUser;
            }
        }

        public User GetUserByLogin(string login)
        {
            using (var dbContext = new MyFoodDbContext())
            {
                var user = dbContext.Users.Include(x => x.Favourites).SingleOrDefault(x => x.Login == login);
                return user;
            }
        }

        public List<User> GetAllUsers()
        {
            using (var dbContext = new MyFoodDbContext())
            {
                return dbContext.Users.ToList();
            }
        }
    }
}
