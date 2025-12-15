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
using TestingPlatform.Models;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class StudentRepository(AppDbContext _appDbContext, IMapper mapper) : IStudentRepository
    {
        public async Task<int> CreateAsync(StudentDto studentDto)
        {
             var student = mapper.Map<Student>(studentDto);

            var studentId = await _appDbContext.Students.AddAsync(student);

            await _appDbContext.SaveChangesAsync();

            return studentId.Entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _appDbContext.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
                throw new Exception("Студент не найден");

            _appDbContext.Users.Remove(student.User);

            await _appDbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<StudentDto>> GetAllAsync()
        {
            var students = await _appDbContext.Students
               .Include(s => s.User )
              .AsNoTracking()
               .ToListAsync();

            return mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<StudentDto> GetByIdAsync(int id)
        {
            var students = await _appDbContext.Students
               .Include(s => s.User)
               .Include(s => s.Tests)

               
              .AsNoTracking()
              .FirstOrDefaultAsync(s => s.Id == id);

            if(students == null)
                throw new EntityNotFoundException("Студент не найден");

            return mapper.Map<StudentDto>(students);
        }

        public async Task UpdateAsync(StudentDto studentDto, int id)
        {
           var student = await _appDbContext.Students.FirstOrDefaultAsync(s => s.Id == id);
            if(student == null)
                throw new EntityNotFoundException("Студент не найден");
            student.Phone = studentDto.Phone;
            student.VKProfileLink = studentDto.VKProfileLink;
           
           
            await _appDbContext.SaveChangesAsync();
        }

        Task<List<StudentDto>> IStudentRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }


        public async Task GetStudentByIdAsync(int id)
        {
            var student = await _appDbContext.Students
                .Include(s => s.User)
                .Include(s => s.Tests)
                .AsNoTracking()
                .FirstOrDefaultAsync(student => student.UserId == id);

            if (student == null)
            {
                throw new EntityNotFoundException("студент не найден");

            }

            return mapper.Map<StudentDto>(student);





        }
    }
}
