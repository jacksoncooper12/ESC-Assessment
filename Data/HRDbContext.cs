using System;
using System.Collections.Generic;
using ESC.Models;
using Microsoft.EntityFrameworkCore;

namespace ESC.Data;

public partial class HRDbContext : DbContext
{
    public HRDbContext()
    {
    }

    public HRDbContext(DbContextOptions<HRDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Dependent> Dependents { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__countrie__7E8CD055550443E7");

            entity.Property(e => e.CountryId).IsFixedLength();
            entity.Property(e => e.CountryName).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.Region).WithMany(p => p.Countries).HasConstraintName("FK__countries__regio__60A75C0F");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__departme__C22324225A325CDA");

            entity.Property(e => e.LocationId).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.Location).WithMany(p => p.Departments)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__departmen__locat__6E01572D");
        });

        modelBuilder.Entity<Dependent>(entity =>
        {
            entity.HasKey(e => e.DependentId).HasName("PK__dependen__F25E28CEEE94EF99");

            entity.HasOne(d => d.Employee).WithMany(p => p.Dependents).HasConstraintName("FK__dependent__emplo__797309D9");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__employee__C52E0BA84951E1B9");

            entity.Property(e => e.DepartmentId).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.FirstName).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.ManagerId).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.PhoneNumber).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__employees__depar__75A278F5");

            entity.HasOne(d => d.Job).WithMany(p => p.Employees).HasConstraintName("FK__employees__job_i__74AE54BC");

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager).HasConstraintName("FK__employees__manag__76969D2E");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.JobId).HasName("PK__jobs__6E32B6A57F380A11");

            entity.Property(e => e.MaxSalary).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.MinSalary).HasDefaultValueSql("(NULL)");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__location__771831EA415BB724");

            entity.Property(e => e.CountryId).IsFixedLength();
            entity.Property(e => e.PostalCode).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.StateProvince).HasDefaultValueSql("(NULL)");
            entity.Property(e => e.StreetAddress).HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.Country).WithMany(p => p.Locations).HasConstraintName("FK__locations__count__66603565");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("PK__regions__01146BAE514767F0");

            entity.Property(e => e.RegionName).HasDefaultValueSql("(NULL)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
