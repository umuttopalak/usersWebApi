using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userRepository.Get(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUsers(int id)
        {
            var users = await _userRepository.GetAll();
            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateUserDto createUserDto)
        {
            var user = new User
            {

                FirstName = createUserDto.FirstName,
                SurName = createUserDto.SurName,
                Id = createUserDto.Id,
                Month = createUserDto.Month,
                Descriptor = createUserDto.Descriptor
            };

            await _userRepository.Add(user);
            return Ok();
        }  

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userRepository.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id ,UpdateUserDto updateUserDto)
        {
            User user = new()
            {
                FirstName = updateUserDto.FirstName,
                SurName = updateUserDto.SurName,
                Id = updateUserDto.Id,
                Descriptor = updateUserDto.Descriptor
            };

            await _userRepository.Update(user);
            return Ok();
        }
    }
    
}   



