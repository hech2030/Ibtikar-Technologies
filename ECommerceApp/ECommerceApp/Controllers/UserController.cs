using ECommerceApp.Common;
using ECommerceApp.Models;
using ECommerceApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ECommerceApp.Controllers
{
    [Route("api/fw/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("Register")]
        public ActionResult Register(User user)
        {
            try
            {
                var result = _userService.Register(user);
                return Ok(new { result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
                throw ex;
            }
        }
        [HttpPost]
        [Route("Login")]
        public ActionResult Login(LoginRequest Request)
        {
            try
            {
                var result = _userService.Login(Request.email, Request.password);
                return Ok(new { result });
            }
            catch (Exception ex)
            {
                Console.Write("Exception : " + ex.Message);
                return BadRequest(new { Message = "Exception has been occured : " + ex.Message });
            }
        }

    }
}
