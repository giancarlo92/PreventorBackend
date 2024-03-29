﻿using Microsoft.AspNetCore.Mvc;
using Project.Entities.Dto;
using Project.Entities.Model;
using Project.Repository.Interfaces;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _repository;

        public StudentsController(IStudentRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _repository.Get();
            return Ok(result);
        }

        [HttpGet("Courses")]
        public async Task<ActionResult> GetWithRelations()
        {
            var result = await _repository.GetWithRelations();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var existe = await _repository.ExistField(id);
            if (!existe) return NotFound();
            var result = await _repository.GetById(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Student entity)
        {
            var result = await _repository.Add(entity);
            return Ok(result);
        }

        [HttpPut("{id}")]
        //[SwaggerRequestExample(typeof(Student), typeof(StudentExample))]
        public async Task<ActionResult> Put(StudentDto entity, int id)
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
