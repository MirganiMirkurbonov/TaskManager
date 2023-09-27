using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Domain.Enumerators;
using Domain.Schemas.Auth;
using Domain.Schemas.BaseEntity;

namespace Domain.Schemas.Project;

[Table("task", Schema = "project")]
public class Task : Entity
{
    [Column("title"), MaxLength(100)]
    public string Title { get; set; } = null!;
    
    [Column("description"), MaxLength(500)]
    public string? Description { get; set; }
    
    [Column("created_date")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    
    [Column("start_date")]
    public DateTime StartDate { get; set; }
    
    [Column("finish_date")]
    public DateTime? FinishedDate { get; set; }

    [Column("status")]
    public ETaskStatus Status { get; set; } = ETaskStatus.Created;
    
    [Column("priority")]
    public EPriority Priority { get; set; }
    
    [Column("auth_user_id")]
    public long AuthUserId { get; set; }
    
    [ForeignKey(nameof(AuthUserId))]
    public virtual AuthUser AuthUser { get; set; }
}