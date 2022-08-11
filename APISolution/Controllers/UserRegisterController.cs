using System;
using APISolution.Data;
using APISolution.Dtos;
using APISolution.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APISolution.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/register")]
    public class UserRegisterController : ControllerBase
    {
        private readonly IUserRepo _repository;
        private readonly IMapper _mapper;

        public UserRegisterController(IUserRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult<UserReadDto> RegisterUser(UserRegisterDto userRegisterDto)
        {
            var userModel = _mapper.Map<User>(userRegisterDto);
            _repository.RegsiterUser(userModel);
            _repository.SaveChanges();

            var userReadDto = _mapper.Map<UserReadDto>(userModel);

            return Ok(userReadDto);
        }
    }
}

