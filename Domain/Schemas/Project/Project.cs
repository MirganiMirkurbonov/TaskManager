using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Schemas.Auth;
using Domain.Schemas.BaseEntity;

namespace Domain.Schemas.Project;

[Table("project", Schema = "project")]
public class Project : Entity
{
    [Column("name"), MaxLength(100)]
    public string Name { get; set; } = null!;
    
    [Column("description"), MaxLength(500)]
    public string? Description { get; set; }
    
    [Column("created_date")]
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    
    [Column("last_updated_date")]
    public DateTime LastUpdatedDate { get; set; }
    
    [Column("owner_id")]
    public long OwnerId { get; set; }
    
    [ForeignKey(nameof(OwnerId))]
    public virtual AuthUser Owner { get; set; }
}