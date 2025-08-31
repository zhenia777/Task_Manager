using API.Dtos.Users;
using AutoMapper;
using Domain.UseCases.AccountOperations.Commands.Login;
using Domain.UseCases.AccountOperations.Commands.Registration;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpPost("registration")]
        public async Task<ActionResult> Registration(
        [FromBody] RegistrationDto user,
        CancellationToken cancellationToken)
        {
            var command = _mapper.Map<RegistrationCommand>(user);
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto user, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<LoginCommand>(user);
            var model = await _mediator.Send(command, cancellationToken);
            var dto = _mapper.Map<ResultLoginDto>(model);
            return Ok(dto);
        }
    }
}
