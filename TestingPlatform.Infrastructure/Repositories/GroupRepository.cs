
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Models;



namespace TestingPlatform.Infrastructure.Repositories
{
    public class GroupRepository(AppDbContext _appDbContext, IMapper mapper) : IGroupRepository
    {

        public async Task<List<GroupDto>> GetAllAsync()
        {
            var groups = await _appDbContext.Groups
                .Include(x => x.Project)
                .Include(x => x.Direction)
                .Include(x => x.Course)
                .Include(x => x.Students)
                .AsNoTracking()
                .ToListAsync();

            return mapper.Map<List<GroupDto>>(groups);
        }

        public async Task<GroupDto> GetByIdAsync(int id)
        {
            var group = await _appDbContext.Groups
                .Include(x => x.Project)
                .Include(x => x.Direction)
                .Include(x => x.Course)
                .Include(x => x.Students)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (group is null) throw new Exception("Группа не найдена");

            return mapper.Map<GroupDto>(group);
        }

        public async Task<int> CreateAsync(GroupDto groupDto)
        {
            var group = mapper.Map<Group>(groupDto);

            var direction = await _appDbContext.Directions.FirstOrDefaultAsync(d => d.Id == group.DirectionId);
            var course = await _appDbContext.Courses.FirstOrDefaultAsync(c => c.Id == group.CourseId);
            var project = await _appDbContext.Projects.FirstOrDefaultAsync(p => p.Id == group.ProjectId);

            if (direction is null) throw new Exception("Убедитесь что направление существует");
            group.Direction = direction;
            if (course is null) throw new Exception("Убедитесь что курс существует");
            group.Course = course;
            if (project is null) throw new Exception("Убедитесь что проект существует");
            group.Project = project;

            if (group.Students.Any())
            {
                var ids = group.Students.Select(x => x.Id).ToList();
                var students = _appDbContext.Students.Where(x => ids.Contains(x.Id)).ToList();

                if (students.Count != ids.Count)
                    throw new Exception("Некоторые студенты не найдены");

                group.Students = students;
            }

            var groupId = await _appDbContext.AddAsync(group);
            await _appDbContext.SaveChangesAsync();

            return groupId.Entity.Id;
        }

        public async Task UpdateAsync(GroupDto groupDto, int id)
        {
            var group = await _appDbContext.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (group is null) throw new Exception("Группа не найдена");


            group.Name = groupDto.Name;
            group.DirectionId = groupDto.Direction.Id;
            group.CourseId = groupDto.Course.Id;
            group.ProjectId = groupDto.Project.Id;

            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var group = await _appDbContext.Groups.FirstOrDefaultAsync(g => g.Id == id);

            if (group is null) throw new Exception("Группа не найдена");

             _appDbContext.Groups.Remove(group);
            await _appDbContext.SaveChangesAsync();
        }

       
    }
}
