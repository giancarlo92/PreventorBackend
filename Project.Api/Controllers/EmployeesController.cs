using Microsoft.AspNetCore.Mvc;
using Project.Entities.Dto;
using Project.Entities.Model;
using Project.Repository.Interfaces;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;

        public EmployeesController(IEmployeeRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _repository.Get();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Employee entity)
        {
            var result = await _repository.Add(entity);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(EmployeeDto entity, int id)
        {
            var existe = await _repository.ExistField(id);
            if (!existe) return NotFound();
            await _repository.Update(entity, id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _repository.ExistField(id);
            if (!existe) return NotFound();
            await _repository.Delete(id);
            return Ok();
        }
    }
}
