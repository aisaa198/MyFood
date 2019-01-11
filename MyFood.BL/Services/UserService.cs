using AutoMapper;
using MyFood.BL.Models;
using MyFood.DAL.Models;
using MyFood.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFood.BL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUsersRepository _usersRepository;

        public UserService(IMapper mapper, IUsersRepository usersRepository)
        {
            _mapper = mapper;
            _usersRepository = usersRepository;
        }

        public UserDto GetUserById (Guid userId)
        {
            var user = _usersRepository.GetUserById(userId);
            return _mapper.Map<UserDto>(user);
        }

        public UserDto GetUserByLogin(string userLogin)
        {
            var user = _usersRepository.GetUserByLogin(userLogin);
            return _mapper.Map<UserDto>(user);
        }

        public List<UserDto> GetAllUsers()
        {
            return _usersRepository.GetAllUsers().Select(x => _mapper.Map<UserDto>(x)).ToList();
        }

        public UserDto RegisterUser(UserDto newUser)
        {
            var registeredUser = _usersRepository.RegisterUser(_mapper.Map<User>(newUser));
            return (registeredUser == null)? null : _mapper.Map<UserDto>(registeredUser);
        }

        public UserDto LogIn(string login, string password)
        {
            var foundUser = _usersRepository.GetUserByLogin(login);

            return (foundUser == null || foundUser.Password != password) ? null : _mapper.Map<UserDto>(foundUser);
        }
    }
}
