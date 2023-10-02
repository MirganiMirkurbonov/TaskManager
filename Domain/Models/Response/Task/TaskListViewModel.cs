using Domain.Enumerators;

namespace Domain.Models.Response.Task;

public record TaskListViewModel(
    string Title,
    string? Description,
    DateTime StartDate,
    ETaskStatus Status,
    EPriority Priority);