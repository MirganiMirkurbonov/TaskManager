namespace Domain.Models.Request.Task;

public class UpdateTaskRequest : CreateNewTaskRequest
{
    public long Id { get; set; }
}