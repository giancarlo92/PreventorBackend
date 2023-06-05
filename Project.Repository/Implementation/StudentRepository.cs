using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Entities.Dto;
using Project.Entities.Model;
using Project.Repository.Interfaces;

namespace Project.Repository.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly PreventorDBContext _context;

        public StudentRepository(PreventorDBContext context)
        {
            this._context = context;
        }
        public async Task<int> Add(Student entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.StudentId;
        }

        public async Task Delete(int id)
        {
            _context.Remove(new Student { StudentId = id });
            await _context.SaveChangesAsync();
        }

        public async Task<List<Student>> Get()
        {
            return await _context.Student.AsNoTracking().ToListAsync();
        }

        public async Task<Student> GetById(int id)
        {
            return await _context.Student.AsNoTracking().FirstOrDefaultAsync(x => x.StudentId.Equals(id));
        }

        public async Task Update(StudentDto entity, int id)
        {
            var student = await _context.Student.AsNoTracking().FirstOrDefaultAsync(x => x.StudentId.Equals(id));
            _context.Entry(student).CurrentValues.SetValues(entity);
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistField(int id)
        {
            return await _context.Student.AsNoTracking().AnyAsync(x => x.StudentId.Equals(id));
        }
    }
}
