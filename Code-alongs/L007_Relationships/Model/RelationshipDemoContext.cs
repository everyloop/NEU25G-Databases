using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L007_Relationships.Model;

internal class RelationshipDemoContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = new SqlConnectionStringBuilder()
        {
            ServerSPN = "localhost",
            InitialCatalog = "RelationshipDemoDb",
            IntegratedSecurity = true,
            TrustServerCertificate = true
        }.ToString();

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var courseData = new[]
        {
            new Course() { Id = 1, Name = "C# Programming" },
            new Course() { Id = 2, Name = "Databases" },
        };

        modelBuilder.Entity<Course>().HasData(courseData);

        var studentData = new[]
        {
            new Student() { Id = 1, FirstName = "Anders", LastName = "Andersson", CourseId = 1 },
            new Student() { Id = 2, FirstName = "Bertil", LastName = "Bertilsson", CourseId = 1 },
            new Student() { Id = 3, FirstName = "Carl", LastName = "Carlsson", CourseId = 1 },
            new Student() { Id = 4, FirstName = "David", LastName = "Davidsson", CourseId = 2 },
            new Student() { Id = 5, FirstName = "Erik", LastName = "Eriksson", CourseId = 2 },
        };

        modelBuilder.Entity<Student>().HasData(studentData);
    }
}

public class Student
{
    public int Id { get; set; }

    public required string? FirstName { get; set; }

    public required string? LastName { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public int CourseId { get; set; }

    public Course Course { get; set; }
}

public class Course
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<Student> Students { get; set; }
}