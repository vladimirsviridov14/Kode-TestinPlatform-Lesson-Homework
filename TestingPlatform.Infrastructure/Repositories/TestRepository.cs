using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Models;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class TestRepository(AppDbContext appDbContext, IMapper mapper) : ITestRepository
    {

        public async Task<int> CreateAsync(TestDto testDto)
        {
            var test = mapper.Map<Test>(testDto);
            
            var testId = await appDbContext.AddAsync(test);
           
            await UpdateMembersTest(test, testDto);


            await appDbContext.SaveChangesAsync();
            
            return testId.Entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var test = await appDbContext.Tests.FirstOrDefaultAsync(x => x.Id == id);

            if (test == null)
                throw new Exception("Тест не найдн");

            appDbContext.Tests.Remove(test);
            await appDbContext.SaveChangesAsync();

        }
        
        
        
        public async Task<IEnumerable<TestDto>> GetAllAsync(bool? isPublic, List<int> groupsIds, List<int> studentsIds)
        {
            var tests = appDbContext.Tests
                 .OrderByDescending(x => x.PublishedAt)
                 .ThenBy(t => t.Title)
                 .AsNoTracking()
                 .AsQueryable();

            if (isPublic is not null)
                tests = tests.Where(t => t.IsPublic == isPublic);

            if (studentsIds.Any())
                tests = tests.Where(t => t.Students.Any(s => studentsIds.Contains(s.Id)));

            if (studentsIds.Any())
                tests = tests.Where(t => t.Groups.Any(g => groupsIds.Contains(g.Id)));

            var result = await tests.ToListAsync();

            return mapper.Map<IEnumerable<TestDto>>(result);

        }

        public async Task<IEnumerable<TestDto>> GetAllForStudentsAsync(int studentId)
        {
            var tests = await appDbContext.Tests
                 .Where(t => t.IsPublic)
                 .Where(t =>
                     t.Students.Any(s => s.Id == studentId)
                     || t.Courses.Any(c => c.Groups.Any(g => g.Students.Any(s => s.Id == studentId)))
                     || t.Projects.Any(c => c.Groups.Any(g => g.Students.Any(s => s.Id == studentId)))
                     || t.Directions.Any(c => c.Groups.Any(g => g.Students.Any(s => s.Id == studentId)))
                 )
                     .ToListAsync();
            
            return mapper.Map<IEnumerable<TestDto>>(tests);
        }

      
        public async Task<IEnumerable<TestDto>> GetByIdAsync(int id)
        {
            var tests = await appDbContext.Tests
                
                .Include(t => t.Directions)
                .Include(t => t.Courses)
                .Include(t => t.Groups)
                .Include(t => t.Projects)
                .Include(x => x.Students)
             .ThenInclude(x => x.User)



                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tests == null)
                throw new Exception("Тест не найден");
            
            return mapper.Map<IEnumerable<TestDto>>(tests);
        }

        public async Task<IEnumerable<TestDto>> GetTopRecentAsync(int count = 5)
        {
            var tests = await appDbContext.Tests.AsNoTracking()
                .OrderByDescending(t => t.PublishedAt)
                .ThenByDescending(t => t.Id)
                .Take(count)
                .ToListAsync();









            return mapper.Map<IEnumerable<TestDto>>(tests);
        }
        public async Task UpdateAsync(TestDto testDto)
        {
            var test = await appDbContext.Tests.FirstOrDefaultAsync(t => t.Id == testDto.Id);

            if (test == null)
                throw new Exception("Тест не найден");

            test.Title = testDto.Title;
            test.Description = testDto.Description;
            test.IsRepeatable = testDto.IsRepeatable;
            test.Type = testDto.Type;
            test.PublishedAt = testDto.PublishedAt;
            test.Deadlinen = testDto.Deadlinen;
            test.DurationMinutes = testDto.DurationMinutes;
            test.IsPublic = testDto.IsPublic;
            test.PassingScore = testDto.PassingScore;
            test.MaxAttempts = testDto.MaxAttempts;

            await appDbContext.SaveChangesAsync();
        }

        public async Task UpdateMembersTest(Test test, TestDto testDto)
        {
            var studentIds = testDto.Students?.Select(x => x.Id)
                .Where(id => id > 0)
                .Distinct()
                .ToArray() ?? Array.Empty<int>();
            
            var groupsIds = testDto.Groups?.Select(x => x.Id)
                .Where(id => id > 0)
                .Distinct()
                .ToArray() ?? Array.Empty<int>();
           
            var courseIds = testDto.Courses?.Select(x => x.Id)
                .Where(id => id > 0)
                .Distinct()
                .ToArray() ?? Array.Empty<int>();
           
            var directionIds = testDto.Directions?.Select(x => x.Id)
                .Where(id => id > 0)
                .Distinct()
                .ToArray() ?? Array.Empty<int>();
            
            var projectIds = testDto.Projects?.Select(x => x.Id)
                .Where(id => id > 0)
                .Distinct()
                .ToArray() ?? Array.Empty<int>();

            if(appDbContext.Entry(test).State == EntityState.Detached)
                appDbContext.Attach(test);
            
            await appDbContext.Entry(test).Collection(t => t.Students).LoadAsync();
            test.Students.Clear();
            if (studentIds.Length > 0) 
            {
                var students = await appDbContext.Students
                    .Where(s => studentIds.Contains(s.Id))
                    .ToListAsync();
                
                foreach (var s in students)
                    test.Students.Add(s);
            }
            
            await appDbContext.Entry(test).Collection(t => t.Groups).LoadAsync();
            test.Groups.Clear();
            if (studentIds.Length > 0) 
            {
                var groups = await appDbContext.Groups
                    .Where(s => groupsIds.Contains(s.Id))
                    .ToListAsync();
                
                foreach (var g in groups)
                    test.Groups.Add(g);
            }
            
            await appDbContext.Entry(test).Collection(t => t.Courses).LoadAsync();
            test.Courses.Clear();
            if (studentIds.Length > 0) 
            {
                var courses = await appDbContext.Courses
                    .Where(s => studentIds.Contains(s.Id))
                    .ToListAsync();
                
                foreach (var c in courses)
                    test.Courses.Add(c);
            }
            
            await appDbContext.Entry(test).Collection(t => t.Directions).LoadAsync();
            test.Directions.Clear();
            if (studentIds.Length > 0) 
            {
                var directions = await appDbContext.Directions
                    .Where(s => studentIds.Contains(s.Id))
                    .ToListAsync();
                
                foreach (var d in directions)
                    test.Directions.Add(d);
            }
            
            await appDbContext.Entry(test).Collection(t => t.Projects).LoadAsync();
            test.Projects.Clear();
            if (studentIds.Length > 0) 
            {
                var projects = await appDbContext.Projects
                    .Where(s => studentIds.Contains(s.Id))
                    .ToListAsync();
                
                foreach (var p in projects)
                    test.Projects.Add(p);
            }
        } 

    }
}