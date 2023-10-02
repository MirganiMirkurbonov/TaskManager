using Domain.Models.Request.Task;
using Domain.Models.Response;
using Domain.Models.Response.Task;

namespace Application.Interfaces;

public interface ITask
{
    Task<DefaultResponse<ListResponse<TaskListViewModel>>> CreateTaskAsync(CreateNewTaskRequest request,
        long currentUserId, string traceId, CancellationToken cancellationToken);
}