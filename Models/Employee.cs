using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ESC.Models;

[Table("employees")]
public partial class Employee
{
    [Key]
    [Column("employee_id")]
    public int EmployeeId { get; set; }

    [Column("first_name")]
    [StringLength(20)]
    [Unicode(false)]
    public string? FirstName { get; set; }

    [Column("last_name")]
    [StringLength(25)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("phone_number")]
    [StringLength(20)]
    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    [Column("hire_date")]
    public DateOnly HireDate { get; set; }

    [Column("job_id")]
    public int JobId { get; set; }

    [Column("salary", TypeName = "decimal(8, 2)")]
    public decimal Salary { get; set; }

    [Column("manager_id")]
    public int? ManagerId { get; set; }

    [Column("department_id")]
    public int? DepartmentId { get; set; }

    [ForeignKey("DepartmentId")]
    [InverseProperty("Employees")]
    public virtual Department? Department { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();

    [InverseProperty("Manager")]
    [JsonIgnore]
    public virtual ICollection<Employee> InverseManager { get; set; } = new List<Employee>();

    [ForeignKey("JobId")]
    [InverseProperty("Employees")]
    public virtual Job Job { get; set; } = null!;

    [ForeignKey("ManagerId")]
    [InverseProperty("InverseManager")]
    [JsonIgnore]
    public virtual Employee? Manager { get; set; }
}
