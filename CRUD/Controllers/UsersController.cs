using Common.DTO;
using Contracts.Service;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersCRUD.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("UsersList")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin, UserOnly")] 
        public ResponseDTO GetAllUsers()
        {
            return _userService.GetAllUsers();
        }


        [HttpPost("Add")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public ActionResult<ResponseDTO> AddUser([FromBody] UserDTO newUser)
        {
            var response = _userService.AddUser(newUser);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }


        [HttpPut("Modify")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public ActionResult<ResponseDTO> ModifyUser([FromBody] UserDTO modifyUser)
        {
            var response = _userService.ModifyUser(modifyUser);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }


        [HttpDelete("Delete/{userName}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public ActionResult<ResponseDTO> DeleteUser(string userName)
        {
            var deleteUser = new UserDTO();
            deleteUser.UserName = userName;
            var response = _userService.DeleteUser(deleteUser);

            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }


        

    }
}
