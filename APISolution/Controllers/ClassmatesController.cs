using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APISolution.Data;
using APISolution.Dtos;
using APISolution.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace APISolution.Controllers
{
    //[Route("api/classmates")]
    [Authorize]
    [Route("api/v{version:apiVersion}/classmates")]
    [ApiController]
    public class ClassmatesController : ControllerBase
    {
        private readonly IClassmateRepo _repository;
        private readonly IMapper _mapper;

        public ClassmatesController(IClassmateRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClassmates()
        {
            var classmateItems = await _repository.GetAllClassmatesAsync();

            return Ok(_mapper.Map<IEnumerable<ClassmateReadDto>>(classmateItems));
        }

        [HttpGet("{id}", Name = "GetClassmateById")]
        public async Task<IActionResult> GetClassmateById(int id)
        {
            var classmateItem = await _repository.GetClassmateByIdAsync(id);

            if (classmateItem != null)
                return Ok(_mapper.Map<ClassmateReadDto>(classmateItem));

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateClassmate(ClassmateCreatDto classmate)
        {
            var classmateModel = _mapper.Map<Classmate>(classmate);
            _repository.CreateClassmate(classmateModel);
            await _repository.SaveChangesAsync();

            var classmateReadDto = _mapper.Map<ClassmateReadDto>(classmateModel);

            return CreatedAtRoute(nameof(GetClassmateById), new { Id = classmateReadDto.Id }, classmateReadDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClassmate(int id, ClassmateUpdateDto classmateUpdateDto)
        {
            var classmateModelFromRepo = _repository.GetClassmateByIdAsync(id).Result;

            if (classmateModelFromRepo == null)
                return NotFound();

            _mapper.Map(classmateUpdateDto, classmateModelFromRepo);

            _repository.UpdateClassmate(classmateModelFromRepo);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialClassmateUpdate(int id, JsonPatchDocument<ClassmateUpdateDto> patchDoc)
        {
            var classmateModelFromRepo = _repository.GetClassmateByIdAsync(id).Result;

            if (classmateModelFromRepo == null)
                return NotFound();

            var classmateToPatch = _mapper.Map<ClassmateUpdateDto>(classmateModelFromRepo);
            patchDoc.ApplyTo(classmateToPatch, ModelState);

            if (!TryValidateModel(classmateToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(classmateToPatch, classmateModelFromRepo);
            _repository.UpdateClassmate(classmateModelFromRepo);
            _repository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<ClassmateReadDto> DeleteClassmate(int id)
        {
            var classmateModelFromRepo = _repository.GetClassmateByIdAsync(id).Result;

            if (classmateModelFromRepo == null)
                return NotFound();

            _repository.DeleteClassmate(classmateModelFromRepo);
            _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}

