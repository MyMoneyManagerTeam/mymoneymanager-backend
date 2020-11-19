using System;
using System.Security.Claims;
using Application.Services.Jars;
using Application.Services.Jars.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyMoneyManagerBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JarController : ControllerBase
    {
        private readonly IJarService _jarService;

        public JarController(IJarService jarService)
        {
            _jarService = jarService;
        }

        [HttpGet]
        [Authorize]
        [Route("[action]")]
        public ActionResult<OutputDtoQueryJar> Query()
        {
            return Ok(_jarService.Query(
                new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value))
            );
        }

        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public ActionResult<OutputDtoCreateJar> Create([FromBody] InputDtoCreateJar inputDtoCreateJar)
        {
            var response = _jarService.Create(new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),inputDtoCreateJar);
            if (response == null)
            {
                return BadRequest(new {message = "Impossible de créer une nouvelle Jar"});
            }
            return Ok(response);
        }
        
        [HttpPut]
        [Authorize]
        [Route("[action]")]
        public ActionResult<bool> Update([FromBody] InputDtoUpdateJar inputDtoUpdateJar)
        {
            var response = _jarService.Update(
                new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                inputDtoUpdateJar
                );
            return Ok(response);
        }
        
        [HttpDelete]
        [Authorize]
        [Route("[action]/{jarId:guid}")]
        public ActionResult<bool> Delete(Guid jarId)
        {
            var response = _jarService.Delete(
                new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                jarId
                );
            return Ok(response);
        }
    }
}