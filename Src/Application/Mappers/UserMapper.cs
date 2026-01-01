using Application.Dtos.Users;
using Application.Helpers;
using Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public static class UserMapper
    {
        public static User MapTo(this UserCreateDto dto)
        {
            return new User(dto.FullName, dto.Email, PasswordHelper.Hash(dto.Password), dto.Role);
        }
    }
}
