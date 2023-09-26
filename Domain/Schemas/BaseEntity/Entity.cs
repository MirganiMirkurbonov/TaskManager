using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enumerators;

namespace Domain.Schemas.BaseEntity;

public abstract class Entity
{
    [Column("id")]
    public long Id { get; set; }
    
    [Column("state")]
    public EState State { get; set; }
}