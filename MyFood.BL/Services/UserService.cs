using AutoMapper;
using MyFood.BL.Models;
using MyFood.DAL.Repositories;
using System;

namespace MyFood.BL.Services
{
    public class UserService
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
    }
}
