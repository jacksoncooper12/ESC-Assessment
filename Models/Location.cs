using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ESC.Models;

[Table("locations")]
public partial class Location
{
    [Key]
    [Column("location_id")]
    public int LocationId { get; set; }

    [Column("street_address")]
    [StringLength(40)]
    [Unicode(false)]
    public string? StreetAddress { get; set; }

    [Column("postal_code")]
    [StringLength(12)]
    [Unicode(false)]
    public string? PostalCode { get; set; }

    [Column("city")]
    [StringLength(30)]
    [Unicode(false)]
    public string City { get; set; } = null!;

    [Column("state_province")]
    [StringLength(25)]
    [Unicode(false)]
    public string? StateProvince { get; set; }

    [Column("country_id")]
    [StringLength(2)]
    [Unicode(false)]
    public string CountryId { get; set; } = null!;

    [ForeignKey("CountryId")]
    [InverseProperty("Locations")]
    public virtual Country Country { get; set; } = null!;

    [InverseProperty("Location")]
    [JsonIgnore]
    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}
