using System;
using System.Collections.Generic;
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
    [ApiController]
    [Route("api/v{version:apiVersion}/classmates")]
    [ApiVersion("2.0")]
    public class ClassmatesControllerV2 : ControllerBase
    {
        private readonly IClassmateRepo _repository;
        private readonly IMapper _mapper;

        public ClassmatesControllerV2(IClassmateRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClassmateReadDto>> GetAllClassmates()
        {
            var classmateItems = _repository.GetAllClassmates();

            return Ok(_mapper.Map<IEnumerable<ClassmateReadDto>>(classmateItems));
        }

        //[HttpGet("{id}", Name = "GetClassmateById")]
        //public ActionResult<ClassmateReadDto> GetClassmateById(int id)
        //{
        //    var classmateItem = _repository.GetClassmateById(id);

        //    if (classmateItem != null)
        //        return Ok(_mapper.Map<ClassmateReadDto>(classmateItem));

        //    return NotFound();
        //}

        //[HttpPost]
        //public ActionResult<ClassmateReadDto> CreateClassmate(ClassmateCreatDto classmate)
        //{
        //    var classmateModel = _mapper.Map<Classmate>(classmate);
        //    _repository.CreateClassmate(classmateModel);
        //    _repository.SaveChanges();

        //    var classmateReadDto = _mapper.Map<ClassmateReadDto>(classmateModel);

        //    return CreatedAtRoute(nameof(GetClassmateById), new { Id = classmateReadDto.Id }, classmateReadDto);
        //}

        //[HttpPut("{id}")]
        //public ActionResult UpdateClassmate(int id, ClassmateUpdateDto classmateUpdateDto)
        //{
        //    var classmateModelFromRepo = _repository.GetClassmateById(id);

        //    if (classmateModelFromRepo == null)
        //        return NotFound();

        //    _mapper.Map(classmateUpdateDto, classmateModelFromRepo);

        //    _repository.UpdateClassmate(classmateModelFromRepo);
        //    _repository.SaveChanges();

        //    return NoContent();
        //}

        //[HttpPatch("{id}")]
        //public ActionResult PartialClassmateUpdate(int id, JsonPatchDocument<ClassmateUpdateDto> patchDoc)
        //{
        //    var classmateModelFromRepo = _repository.GetClassmateById(id);

        //    if (classmateModelFromRepo == null)
        //        return NotFound();

        //    var classmateToPatch = _mapper.Map<ClassmateUpdateDto>(classmateModelFromRepo);
        //    patchDoc.ApplyTo(classmateToPatch, ModelState);

        //    if (!TryValidateModel(classmateToPatch))
        //        return ValidationProblem(ModelState);

        //    _mapper.Map(classmateToPatch, classmateModelFromRepo);
        //    _repository.UpdateClassmate(classmateModelFromRepo);
        //    _repository.SaveChanges();

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public ActionResult<ClassmateReadDto> DeleteClassmate(int id)
        //{
        //    var classmateModelFromRepo = _repository.GetClassmateById(id);

        //    if (classmateModelFromRepo == null)
        //        return NotFound();

        //    _repository.DeleteClassmate(classmateModelFromRepo);
        //    _repository.SaveChanges();

        //    return NoContent();
        //}
    }
}

