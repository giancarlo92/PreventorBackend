using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Entities.Dto;
using Project.Entities.Model;
using Project.Repository.Interfaces;

namespace Project.Repository.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly PreventorDBContext _context;

        public EmployeeRepository(PreventorDBContext context)
        {
            this._context = context;
        }
        public async Task<int> Add(Employee entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(int id)
        {
            _context.Remove(new Employee { Id = id });
            await _context.SaveChangesAsync();
        }

        public async Task<List<Employee>> Get()
        {
            return await _context.Employee.AsNoTracking().ToListAsync();
        }

        public async Task Update(EmployeeDto entity, int id)
        {
            var Employee = await _context.Employee.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
            _context.Entry(Employee).CurrentValues.SetValues(entity);
            _context.Entry(Employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistField(int id)
        {
            return await _context.Employee.AsNoTracking().AnyAsync(x => x.Id.Equals(id));
        }
    }
}
