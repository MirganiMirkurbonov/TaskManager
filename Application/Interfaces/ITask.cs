using Domain.Enumerators;
using Domain.Models.Request.Task;
using Domain.Models.Response;
using Domain.Models.Response.Task;

namespace Application.Interfaces;

public interface ITask
{
    Task<DefaultResponse<ListResponse<TaskListViewModel>>> CreateTaskAsync(CreateNewTaskRequest request,
        long currentUserId, string traceId, CancellationToken cancellationToken);
    
    Task<DefaultResponse<ListResponse<TaskListViewModel>>> FilterTaskByPriorityAsync(EPriority priority,
        long currentUserId, string traceId, CancellationToken cancellationToken);
    
    Task<DefaultResponse<ListResponse<TaskListViewModel>>> UpdateTask(UpdateTaskRequest request,
        long currentUserId, string traceId, CancellationToken cancellationToken);

    Task<DefaultResponse<ListResponse<TaskListViewModel>>> DeleteTaskAsync(long taskId,
        long currentUserId, string traceId, CancellationToken cancellationToken);

    Task<DefaultResponse<ListResponse<TaskListViewModel>>> SetPriorityAsync(SetTaskPriorityRequest request,
        long currentUserId, string traceId, CancellationToken cancellationToken);
}