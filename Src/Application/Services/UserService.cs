using Application.Dtos.Users;
using Application.Interfaces;
using Application.Mappers;
using Domain;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task AddUser(UserCreateDto dto)
        {
            await _repository.Insert(dto.MapTo());
        }
    }
}
