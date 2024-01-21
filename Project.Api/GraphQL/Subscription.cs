using Project.Entities.Model;

namespace Project.Api.GraphQL
{
    public class Subscription
    {
        [Subscribe]
        [Topic]
        public Student OnStudentAdded([EventMessage] Student student)
        {
            return student;
        }
    }
}
