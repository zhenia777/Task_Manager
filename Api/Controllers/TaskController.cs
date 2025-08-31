using API.Dtos.Tasks;
using API.Extensions;
using AutoMapper;
using Domain.Queries;
using Domain.UseCases.TaskOperations.Commands.CreateTask;
using Domain.UseCases.TaskOperations.Commands.DeleteTask;
using Domain.UseCases.TaskOperations.Commands.UpdateTask;
using Domain.UseCases.TaskOperations.Queries.GetAllTasks;
using Domain.UseCases.TaskOperations.Queries.GetTaskById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class TaskController(IMediator mediator, IMapper mapper) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    private readonly IMapper _mapper = mapper;

    [HttpPost("create-task")]
    public async Task<ActionResult> Create(
         [FromBody] CreateTaskDto task,
         CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateTaskCommand>(task);
        command.UserId = User.GetUserId();
        var model = await _mediator.Send(command, cancellationToken);
        var dto = _mapper.Map<TaskDto>(model);
        return Ok(dto);
    }

    [HttpDelete("delete-task/{id:Guid}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteTaskCommand(id);
        await _mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpPut("update-task")]
    public async Task<ActionResult> Update([FromBody] UpdateTaskDto task, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateTaskCommand>(task);
        var model = await _mediator.Send(command, cancellationToken);
        var dto = _mapper.Map<TaskDto>(model);
        return Ok(dto);
    }

    [HttpGet("get-all-tasks")]
    public async Task<ActionResult> Get([FromQuery] PaginationQuery request,
                                        CancellationToken cancellationToken)
    {
        var query = new GetTasksQuery(User.GetUserId());
        query.Page = request.Page;
        query.ItemPerPage = request.ItemPerPage;
        var model = await _mediator.Send(query, cancellationToken);
        var dto = _mapper.Map<PaginationListDto<TaskDto>>(model);
        return Ok(dto);
    }

    [HttpGet("get-task/{id:Guid}")]
    public async Task<ActionResult> GetTaskById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetTaskByIdQuery(id);
        var model = await _mediator.Send(query, cancellationToken);
        var dto = _mapper.Map<TaskDto>(model);
        return Ok(dto);
    }
}
