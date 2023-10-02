using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enumerators;

namespace Domain.Models.Request.Task;

public class CreateNewTaskRequest
{
    public string Title { get; set; } = null!;
    
    public string? Description { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public EPriority Priority { get; set; }
}