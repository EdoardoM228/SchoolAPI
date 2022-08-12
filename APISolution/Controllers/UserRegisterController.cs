using System;
using System.Threading.Tasks;
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
        public async Task<IActionResult> RegisterUser(UserRegisterDto userRegisterDto)
        {
            var userModel = _mapper.Map<User>(userRegisterDto);
            _repository.RegsiterUser(userModel);
            await _repository.SaveChangesAsync();

            var userReadDto = _mapper.Map<UserReadDto>(userModel);

            return Ok(userReadDto);
        }
    }
}

