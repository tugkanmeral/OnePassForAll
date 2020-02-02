using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnePassForAll.Model.API;

namespace OnePassForAll.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;

        public AccountController(IAccountService pAccountService)
        {
            _accountService = pAccountService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginModel model)
        {
            User user = _accountService.CheckUser(model.Email, model.Password);
            
            if (user == null)
                return BadRequest(new { message = "Email or password is incorrect"});

            var token = _accountService.Authenticate(user);

            if (!String.IsNullOrWhiteSpace(token))
                return Ok(token);

            return BadRequest(new { message = "Error while token creation" });
        }

        // GET: api/Account
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "giriş", "yapılmış" };
        }
    }
}
