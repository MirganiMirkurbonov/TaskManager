using Domain.Enumerators;

namespace Domain.Models.Request.Task;

public class SetTaskPriorityRequest
{
    public long Id { get; set; }
    public EPriority Priority { get; set; }
}