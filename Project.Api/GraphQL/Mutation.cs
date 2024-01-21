using Azure;
using HotChocolate.Subscriptions;
using Project.Api.GraphQL.Models;
using Project.Data;
using Project.Entities.Model;

namespace Project.Api.GraphQL
{
    public class Mutation
    {
        public async Task<Student> AddStudentAsync(
            [Service] PreventorDBContext _context,
            StudentRequest request,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken
        )
        {
            var student = new Student
            {
                Name = request.Name,
                Surname = request.Surname,
                DocumentType = request.DocumentType,
                Passport = request.Passport,
                Email = request.Email,
                Phone = request.Phone
            };

            await _context.Student.AddAsync(student);
            await _context.SaveChangesAsync(cancellationToken);
            await eventSender.SendAsync(nameof(Subscription.OnStudentAdded), student, cancellationToken);

            return student;
        }
    }
}
