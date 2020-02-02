using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Abstract;
using Entities.Concrete;
using Entities.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnePassForAll.Helpers;
using OnePassForAll.Model.API;

namespace OnePassForAll.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordsController : ControllerBase
    {
        private IPasswordService _passwordManager;

        private string NO_USER_MSG = "User not found!";
        private string NO_PASS_MSG = "Password not found!";
        private string NOT_ADDED_MSG = "Password not added!";

        public PasswordsController(IPasswordService pPasswordManager)
        {
            _passwordManager = pPasswordManager;
        }

        // GET: api/Passwords
        [HttpGet]
        public IActionResult Get()
        {
            int userId = Extentions.GetUserId(this.User);

            if (userId == -1)
                return BadRequest(new { message = NO_USER_MSG });

            IEnumerable<PassCard> passCards = _passwordManager.GetPasswords(userId);

            return Ok(passCards);
        }

        // GET: api/Passwords/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            int userId = Extentions.GetUserId(this.User);

            if (userId == -1)
                return BadRequest(new { message = NO_USER_MSG });

            PassCard passCard = _passwordManager.GetPassword(userId, id);

            if (passCard == null)
                return BadRequest(new { message = NO_PASS_MSG });

            return Ok(passCard);
        }

        // POST: api/Passwords
        [HttpPost]
        public IActionResult Post([FromBody]PassCardModel model)
        {
            int userId = Extentions.GetUserId(this.User);

            if (userId == -1)
                return BadRequest(new { message = NO_USER_MSG });

            PassCard passCard = new PassCard()
            {
                UserId = userId,
                Name = model.Name,
                Username = model.Username,
                Password = model.Password,
                Description = model.Description
            };

            passCard.Id = _passwordManager.AddPassword(passCard);

            if (passCard.Id <= 0)
                return BadRequest(new { message = NOT_ADDED_MSG });

            return Ok(passCard);
        }

        // PUT: api/Passwords/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]PassCardModel model)
        {
            int userId = Extentions.GetUserId(this.User);

            if (userId == -1)
                return BadRequest(new { message = NO_USER_MSG });

            PassCard passCard = _passwordManager.GetPassword(userId, id);

            if (passCard == null)
                return BadRequest(new { message = NO_PASS_MSG });

            passCard.Name = model.Name;
            passCard.Username = model.Username;
            passCard.Password = model.Password;
            passCard.Description = model.Description;

            _passwordManager.UpdatePassword(passCard);

            return Ok(passCard);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int userId = Extentions.GetUserId(this.User);

            if (userId == -1)
                return BadRequest(new { message = NO_USER_MSG });

            PassCard passCard = _passwordManager.GetPassword(userId, id);

            if (passCard == null)
                return BadRequest(new { message = NO_PASS_MSG });

            _passwordManager.DeletePassword(passCard);

            return Ok();
        }
    }
}
