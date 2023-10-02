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

namespace API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize]
public class TaskController : MyController<TaskController>
{
    private readonly IValidator<CreateNewTaskRequest> _createTaskRequestValidator;
    private readonly ITask _task;

    public TaskController(
        IValidator<CreateNewTaskRequest> createTaskRequestValidator,
        ITask task)
    {
        _createTaskRequestValidator = createTaskRequestValidator;
        _task = task;
    }
    
    [HttpPost("create")]
    public async Task<DefaultResponse<ListResponse<TaskListViewModel>>> CreateNewTask(CreateNewTaskRequest request)
    {
        var requestValidationResult = await _createTaskRequestValidator.ValidateAsync(request);
        if (!requestValidationResult.IsValid)
            return new ErrorResponse(HttpStatusCode.BadRequest, EResponseCode.InvalidInputParams);

        return await _task.CreateTaskAsync(request,
            (long)UserId!,
            TraceId,
            CancellationToken);
    }
}