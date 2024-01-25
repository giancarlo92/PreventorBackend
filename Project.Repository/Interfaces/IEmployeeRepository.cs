using Project.Entities.Dto;
using Project.Entities.Model;

namespace Project.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<int> Add(Employee entity);
        Task Delete(int id);
        Task Update(EmployeeDto entity, int id);
        Task<List<Employee>> Get();
        Task<bool> ExistField(int id);
    }
}
