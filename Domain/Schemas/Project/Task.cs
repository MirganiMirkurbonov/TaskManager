using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enumerators;
using Domain.Schemas.BaseEntity;

namespace Domain.Schemas.Project;

[Table("task", Schema = "project")]
public class Task : Entity
{
    [Column("title"), MaxLength(100)]
    public string Title { get; set; } = null!;
    
    [Column("description"), MaxLength(500)]
    public string? Description { get; set; }

    [Column("status")]
    public ETaskStatus Status { get; set; } = ETaskStatus.Created;
    
    [Column("priority")]
    public EPriority Priority { get; set; }
    
    [Column("project_id")]
    public long ProjectId { get; set; }
    
    [ForeignKey(nameof(ProjectId))]
    public virtual Project Project { get; set; }
}