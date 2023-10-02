using System.Net;
using Application.Extensions;
using Application.Interfaces;
using Domain.Enumerators;
using Domain.Extensions;
using Domain.Models.Inner;
using Domain.Models.Request.Task;
using Domain.Models.Response;
using Domain.Models.Response.Task;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Common;
using Task = Domain.Schemas.Project.Task;

namespace Application.Services;

internal class TaskRepository : ITask
{
    private readonly IGenericRepository<Task> _genericRepository;
    private readonly ILogger<TaskRepository> _logger;

    public TaskRepository(IGenericRepository<Task> genericRepository,
        ILogger<TaskRepository> logger)
    {
        _genericRepository = genericRepository;
        _logger = logger;
    }

    public async Task<DefaultResponse<ListResponse<TaskListViewModel>>> CreateTaskAsync(
        CreateNewTaskRequest request,
        long currentUserId,
        string traceId,
        CancellationToken cancellationToken)
    {
        try
        {
            request = request.TrimAndLower();

            var newTask = request.MapTo<Task>();
            newTask.AuthUserId = currentUserId;
            await _genericRepository.Create(newTask);
            
            var taskList =await _genericRepository.Query()
                .Where(x => x.AuthUserId == currentUserId)
                .OrderBy(x => x.Id)
                .Select(x=>x.MapTo<TaskListViewModel>())
                .ToListAsync(cancellationToken);
            
            return new ListResponse<TaskListViewModel>(taskList);
        }
        catch (Exception exception)
        {
            _logger.LogCritical(traceId, exception, $"Exception thrown | \r\n request : {request.ToJsonString()}");
            return new ErrorResponse(HttpStatusCode.InternalServerError, EResponseCode.InternalServerError);
        }
    }
}