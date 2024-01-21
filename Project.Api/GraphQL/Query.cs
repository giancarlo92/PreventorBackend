using Project.Data;
using Project.Entities.Model;
using Project.Repository.Interfaces;

namespace Project.Api.GraphQL
{
    public class Query
    {
        [UseFiltering]
        [UseSorting]
        //[UseProjection]
        public IQueryable<Student> GetStudentContext([Service] PreventorDBContext _context)
        {
            return _context.Student;
        }

        [UseFiltering]
        [UseSorting]
        public IQueryable<Course> GetCourseContext([Service] PreventorDBContext _context)
        {
            return _context.Course;
        }

        public async Task<Student> GetStudent([Service] IStudentRepository _repository, int id)
        {
            return await _repository.GetById(id);
        }
    }
}
