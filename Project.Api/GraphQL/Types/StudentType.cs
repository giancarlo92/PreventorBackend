using Project.Data;
using Project.Entities.Model;

namespace Project.Api.GraphQL.Types
{
    public class StudentType : ObjectType<Student>
    {
        protected override void Configure(IObjectTypeDescriptor<Student> descriptor)
        {
            descriptor.Description("Tabla de estudiantes");

            descriptor
                .Field(x => x.Passport)
                .Ignore();

            descriptor
                .Field(x => x.StudentCourses)
                .ResolveWith<Resolvers>(x => x.GetStudentCourseById(default!, default!))
                .UseDbContext<PreventorDBContext>()
                .Description("Lista de courses por studentid");

        }

        private class Resolvers
        {
            public IQueryable<StudentCourse> GetStudentCourseById([Parent] Student student, [Service] PreventorDBContext _context)
            {
                return _context.StudentCourse.Where(x => x.StudentId == student.StudentId);
            }
        }
    }
}
