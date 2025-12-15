using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Infrastructure.Exceptions;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class AuthRepository(AppDbContext appDbContext, IMapper mapper) : IAuthRepository
    {
        public async Task<UserDto> AuthorizeUser(UserLoginDto users)
        {
            var user = await appDbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Login == users.Login);

            if (user is null)
                throw new EntityNotFoundException($"Неверный пароль или логин.");

            if (!BCrypt.Net.BCrypt.Verify(users.Password, user.PasswordHash))
                throw new EntityNotFoundException("Неверный пароль или логин.");

            return mapper.Map<UserDto>(user);
        }
    }
}
