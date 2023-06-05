using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project.Data;
using Project.Entities.Dto;
using Project.Entities.Model;
using Project.Repository.Implementation;
using Project.Repository.Interfaces;

namespace Project.Test
{
    [TestClass]
    public class StudentTest
    {
        private IStudentRepository _repository;
        private DbContextOptions<PreventorDBContext> options;

        [TestInitialize]
        public void Initialize()
        {
            options = new DbContextOptionsBuilder<PreventorDBContext>()
                .UseNpgsql("Server=chunee.db.elephantsql.com;Port=5432;Database=rgwnmmab;User Id=rgwnmmab;Password=JY0E2bn08ipPbcvdPShgwCyot8cUrsSO;")
                .Options;
            var context = new PreventorDBContext(options);
            _repository = new StudentRepository(context);
        }

        [TestMethod]
        public async Task Create()
        {
            var student = CreateObject();

            var result = await _repository.Add(student);
            Assert.IsNotNull(result);
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public async Task GetList()
        {
            var result = await _repository.Get();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public async Task GetById()
        {
            var studentId = await AddStudentTest();
            var result = await _repository.GetById(studentId);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Student));
            await RemoveStudentTest(studentId);
        }

        [TestMethod]
        public async Task Update()
        {
            var studentId = await AddStudentTest();
            var student = new StudentDto
            {
                Name = "Jose",
                Surname = "Galvez",
                DocumentType = 2,
                Email = "jose@gmail.com",
                Passport = "T-655265265",
                Phone = "798465321"
            };
            await _repository.Update(student, studentId);
            var result = VerifyStudentTest(studentId);

            Assert.AreEqual(result.Name, student.Name);
            Assert.AreEqual(result.Surname, student.Surname);

            await RemoveStudentTest(studentId);
        }

        [TestMethod]
        public async Task Delete()
        {
            var studentId = await AddStudentTest();
            await _repository.Delete(studentId);
            var result = VerifyStudentTest(studentId);

            Assert.IsNull(result);
        }

        private Student CreateObject()
        {
            return new Student
            {
                Name = "Giancarlo",
                Surname = "Zevallos",
                DocumentType = 1,
                Email = "gian@gmail.com",
                Passport = "T-655265265",
                Phone = "798465321"
            };
        }

        private async Task<int> AddStudentTest()
        {
            using (var context = new PreventorDBContext(options))
            {
                var student = CreateObject();
                await context.Student.AddAsync(student);
                await context.SaveChangesAsync();
                return student.StudentId;
            }
        }

        private async Task RemoveStudentTest(int studentId)
        {
            using (var context = new PreventorDBContext(options))
            {
                context.Remove(new Student { StudentId = studentId });
                await context.SaveChangesAsync();
            }
        }

        private Student VerifyStudentTest(int studentId)
        {
            using (var context = new PreventorDBContext(options))
            {
                return context.Student.FirstOrDefault(x => x.StudentId.Equals(studentId));
            }
        }
    }
}