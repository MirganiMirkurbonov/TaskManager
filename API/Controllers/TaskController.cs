using System.Net;
using API.Helpers;
using Application.Interfaces;
using Domain.Enumerators;
using Domain.Models.Inner;
using Domain.Models.Request.Task;
using Domain.Models.Response;
using Domain.Models.Response.Task;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task = Domain.Schemas.Project.Task;

namespace API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class TaskController : MyController<TaskController>
{
    private readonly IValidator<CreateNewTaskRequest> _createTaskRequestValidator;
    private readonly IValidator<UpdateTaskRequest> _updateTaskRequestValidator;
    private readonly ITask _task;

    public TaskController(
        IValidator<CreateNewTaskRequest> createTaskRequestValidator,
        ITask task, IValidator<UpdateTaskRequest> updateTaskRequestValidator)
    {
        _updateTaskRequestValidator = updateTaskRequestValidator;
        _createTaskRequestValidator = createTaskRequestValidator;
        _task = task;
    }

    [HttpPost("create")]
    public async Task<DefaultResponse<ListResponse<TaskListViewModel>>> CreateNewTask(CreateNewTaskRequest request)
    {
        var requestValidationResult = await _createTaskRequestValidator.ValidateAsync(request);
        if (!requestValidationResult.IsValid)
            return new ErrorResponse(HttpStatusCode.BadRequest, EResponseCode.InvalidInputParams);

        return await _task.CreateTaskAsync(
            request,
            (long)UserId!,
            TraceId,
            CancellationToken);
    }
    
    [HttpPut("update")]
    public async Task<DefaultResponse<ListResponse<TaskListViewModel>>> UpdateTaskAsync(UpdateTaskRequest request)
    {
        var requestIsValid = await _updateTaskRequestValidator.ValidateAsync(request);
        if (!requestIsValid.IsValid)
            return new ErrorResponse(HttpStatusCode.BadRequest, EResponseCode.InvalidInputParams);
        
        return await _task.UpdateTask(
            request,
            (long)UserId!,
            TraceId,
            CancellationToken);
    }
    
    [HttpDelete]
    public async Task<DefaultResponse<ListResponse<TaskListViewModel>>> DeleteTaskAsync([FromQuery] long id)
    {
        return await _task.DeleteTaskAsync(
            id,
            (long)UserId!,
            TraceId,
            CancellationToken);
    }
    
    [HttpGet("filter")]
    public async Task<DefaultResponse<ListResponse<TaskListViewModel>>> FilterTaskAsync([FromQuery] EPriority priority)
    {
        return await _task.FilterTaskByPriorityAsync(
            priority,
            (long)UserId!,
            TraceId,
            CancellationToken);
    }

    [HttpPost("set-priority")]
    public async Task<DefaultResponse<ListResponse<TaskListViewModel>>> SetTaskPriorityAsync(SetTaskPriorityRequest request)
    {
        return await _task.SetPriorityAsync(
            request,
            (long)UserId!,
            TraceId,
            CancellationToken);
    }
}