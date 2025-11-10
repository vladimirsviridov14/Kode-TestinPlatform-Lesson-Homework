using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Models;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext appDbContext, IMapper mapper) : IUserRepository

    {
        public async Task<int> CreateAsync(UserDto userDto)
        {

            var user = mapper.Map<User>(userDto);
            
            await appDbContext.Users.AddAsync(user);
            await  appDbContext.SaveChangesAsync();

              return user.Id;
        }

      

        public async Task  DeleteAsync(int id)
        {
            var user = await appDbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
                throw new Exception("User not found");

            appDbContext.Users.Remove(user);
            await appDbContext.SaveChangesAsync();
        }

      

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = appDbContext.Users.AsNoTracking().ToListAsync();
            return mapper.Map<IEnumerable<UserDto>>(users);
        }

     

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await appDbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            if(user is null)
                throw new Exception("User not found");

            return mapper.Map<UserDto>(user);
        }

     
        public async Task UpdateAsync(UserDto userDto, int id)
        {
            var user = appDbContext.Users.FirstOrDefault(u => u.Id == id);
            
            if(user is null)
                throw new Exception("User not found");

            if(await appDbContext.Users.AnyAsync(u => u.Login == userDto.Login))
                throw new Exception("Login is already in use");

            user.FirtsName = userDto.FirtsName;
            user.MiddleName = userDto.MiddleName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.Login = userDto.Login;
            user.Role = userDto.Role;


            await appDbContext.SaveChangesAsync();
          
        }

        public Task UpdateAsync(UserDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
