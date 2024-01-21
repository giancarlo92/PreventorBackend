using Project.Entities.Model;

namespace Project.Api.GraphQL.Models
{
    public record StudentRequest(string Name, string Surname, int? DocumentType, string Passport, string Email, string Phone);

    public record StudentResponse(Student student);

}
