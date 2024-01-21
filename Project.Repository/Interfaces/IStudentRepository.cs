using Project.Entities.Dto;
using Project.Entities.Model;

namespace Project.Repository.Interfaces
{
    public interface IStudentRepository
    {
        Task<int> Add(Student entity);
        Task Delete(int id);
        Task Update(StudentDto entity, int id);
        Task<List<Student>> Get();
        Task<List<Student>> GetWithRelations();
        Task<Student> GetById(int id);
        Task<bool> ExistField(int id);
    }
}
