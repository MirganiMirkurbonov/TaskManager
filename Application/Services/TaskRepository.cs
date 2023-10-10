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

    public TaskRepository(
        IGenericRepository<Task> genericRepository,
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
            await _genericRepository.Create(newTask, cancellationToken);

            var taskList = await GetUserTaskList(currentUserId, cancellationToken);
            
            return new ListResponse<TaskListViewModel>(taskList);
        }
        catch (Exception exception)
        {
            _logger.LogCritical(traceId, exception, $"Exception thrown | \r\n request : {request.ToJsonString()}");
            return new ErrorResponse(HttpStatusCode.InternalServerError, EResponseCode.InternalServerError);
        }
    }
    
    public async Task<DefaultResponse<ListResponse<TaskListViewModel>>> FilterTaskByPriorityAsync(
        EPriority priority,
        long currentUserId,
        string traceId,
        CancellationToken cancellationToken)
    {
        try
        {
            var userTasks = await _genericRepository.QueryNoTracking()
                .Where(x => x.Priority == priority)
                .Select(x=>x.MapTo<TaskListViewModel>())
                .ToListAsync(cancellationToken);
            
            return new ListResponse<TaskListViewModel>(userTasks);
        }
        catch (Exception exception)
        {
            _logger.LogCritical(traceId, exception, $"Exception thrown | \r\n request : nulll");
            return new ErrorResponse(HttpStatusCode.InternalServerError, EResponseCode.InternalServerError);
        }
    }

    public async Task<DefaultResponse<ListResponse<TaskListViewModel>>> UpdateTask(
        UpdateTaskRequest request,
        long currentUserId,
        string traceId,
        CancellationToken cancellationToken)
    {
        try
        {
            var userTask = await _genericRepository.QueryWithTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.AuthUserId == currentUserId, cancellationToken);     
            if (userTask == null || userTask.AuthUserId != currentUserId)
                return new ErrorResponse(HttpStatusCode.NotFound, EResponseCode.TaskNotFound);
            
            userTask.Priority = request.Priority;
            userTask.StartDate = request.StartDate;
            userTask.Description = request.Description;
            userTask.Title = request.Title;
            
            await _genericRepository.SaveChangesAsync(cancellationToken);
            
            var taskList =  await GetUserTaskList(currentUserId, cancellationToken);
            return new ListResponse<TaskListViewModel>(taskList);
        }
        catch (Exception exception)
        {
            _logger.LogCritical(traceId, exception, $"Exception thrown | \r\n request : {request.ToJsonString()}");
            return new ErrorResponse(HttpStatusCode.InternalServerError, EResponseCode.InternalServerError);
        }
    }
    
    public async Task<DefaultResponse<ListResponse<TaskListViewModel>>> DeleteTaskAsync(
        long taskId,
        long currentUserId,
        string traceId,
        CancellationToken cancellationToken)
    {
        try
        {
            var userTask = await _genericRepository.QueryWithTracking()
                .FirstOrDefaultAsync(x => x.Id == taskId && x.AuthUserId == currentUserId, cancellationToken);            
            if (userTask == null)
                return new ErrorResponse(HttpStatusCode.NotFound, EResponseCode.TaskNotFound);
            
            userTask.State = EState.Deleted;
            await _genericRepository.SaveChangesAsync(cancellationToken);

            var taskList =  await GetUserTaskList(currentUserId, cancellationToken);
            return new ListResponse<TaskListViewModel>(taskList);
        }
        catch (Exception exception)
        {
            _logger.LogCritical(traceId, exception, $"Exception thrown | \r\n request : null");
            return new ErrorResponse(HttpStatusCode.InternalServerError, EResponseCode.InternalServerError);
        }
    }

    public async Task<DefaultResponse<ListResponse<TaskListViewModel>>> SetPriorityAsync(
        SetTaskPriorityRequest request,
        long currentUserId,
        string traceId,
        CancellationToken cancellationToken)
    {
        try
        {
            var userTask = await _genericRepository.QueryWithTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.AuthUserId == currentUserId, cancellationToken);
            if (userTask == null)
                return new ErrorResponse(HttpStatusCode.NotFound, EResponseCode.TaskNotFound);

            userTask.Priority = request.Priority;
            await _genericRepository.SaveChangesAsync(cancellationToken);
            
            var userTaskList =  await GetUserTaskList(currentUserId, cancellationToken);
            return new ListResponse<TaskListViewModel>(userTaskList);
        }
        catch (Exception exception)
        {
            _logger.LogCritical(traceId, exception, $"Exception thrown | \r\n request : null");
            return new ErrorResponse(HttpStatusCode.InternalServerError, EResponseCode.InternalServerError);
        }
    }

    private async Task<List<TaskListViewModel>> GetUserTaskList(
        long currentUserId,
        CancellationToken cancellationToken)
    {
        return await _genericRepository.QueryNoTracking()
            .Where(x => x.AuthUserId == currentUserId)
            .OrderBy(x=>x.Id)
            .Select(x => x.MapTo<TaskListViewModel>())
            .ToListAsync(cancellationToken);
    }
}