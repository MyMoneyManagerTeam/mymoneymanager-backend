using System;
using System.Security.Claims;
using Application.Services.Accounts;
using Application.Services.Accounts.Dto;
using Domain.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyMoneyManagerBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Authorize]
        [Route("get")]
        public ActionResult<OutputDtoGetAccount> Get()
        {
            var response = _accountService.Get(
                new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
            if (response == null)
            {
                return BadRequest(new {message="Le compte n'existe pas"});
            }

            return Ok(response);
        }
    }
}