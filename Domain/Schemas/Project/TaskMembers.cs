using System.ComponentModel.DataAnnotations.Schema;
using Domain.Schemas.Auth;
using Domain.Schemas.BaseEntity;

namespace Domain.Schemas.Project;

[Table("task_members", Schema = "project")]
public class TaskMembers : Entity
{
    [Column("task_id")]
    public long TaskId { get; set; }
    
    [ForeignKey(nameof(TaskId))]
    public virtual Task Task { get; set; }
    
    
    [Column("member_id")]
    public long MemberId { get; set; }
    
    [ForeignKey(nameof(MemberId))]
    public virtual AuthUser Member { get; set; }
}