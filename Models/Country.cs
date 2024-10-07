using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ESC.Models;

[Table("countries")]
public partial class Country
{
    [Key]
    [Column("country_id")]
    [StringLength(2)]
    [Unicode(false)]
    public string CountryId { get; set; } = null!;

    [Column("country_name")]
    [StringLength(40)]
    [Unicode(false)]
    public string? CountryName { get; set; }

    [Column("region_id")]
    public int RegionId { get; set; }

    [InverseProperty("Country")]
    [JsonIgnore]
    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    [ForeignKey("RegionId")]
    [InverseProperty("Countries")]
    public virtual Region Region { get; set; } = null!;
}
