using Project.Entities.Model;
using Swashbuckle.AspNetCore.Filters;

namespace Project.Api.Swagger
{
    public class StudentExample : IExamplesProvider<Student>
    {
        public Student GetExamples()
        {
            return new Student();
        }
    }
}
