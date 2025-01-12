using Application.User.Queries.FindById;
using Application.Users.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ConfigController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = ((UserInfo)(HttpContext.Items["User"])).Id;
            var currentUser = await _mediator.Send(new FindUserByIdQuery { Id = userId });
            return Ok(new
            {
                CurrentUser = currentUser,
            });
        }

    }
}
