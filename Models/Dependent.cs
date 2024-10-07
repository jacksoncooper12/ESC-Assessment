using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ESC.Models;

[Table("dependents")]
public partial class Dependent
{
    [Key]
    [Column("dependent_id")]
    public int DependentId { get; set; }

    [Column("first_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [Column("relationship")]
    [StringLength(25)]
    [Unicode(false)]
    public string Relationship { get; set; } = null!;

    [Column("employee_id")]
    public int EmployeeId { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("Dependents")]
    [JsonIgnore]
    public virtual Employee Employee { get; set; } = null!;
}
