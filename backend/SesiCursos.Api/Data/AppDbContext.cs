using Microsoft.EntityFrameworkCore;
using SesiCursos.Api.Models;

namespace SesiCursos.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Course> Courses => Set<Course>();
    public DbSet<CourseModule> Modules => Set<CourseModule>();
    public DbSet<CourseLesson> Lessons => Set<CourseLesson>();
    public DbSet<CourseFaq> Faqs => Set<CourseFaq>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(250);
            entity.Property(e => e.ShortName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(2000);
            entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Modality).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
            entity.Property(e => e.TargetAudience).HasMaxLength(300);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.Certification).HasMaxLength(100);

            entity.HasIndex(e => e.Category);
            entity.HasIndex(e => e.Modality);
            entity.HasIndex(e => e.Status);

            entity.HasMany(e => e.Modules)
                  .WithOne(m => m.Course)
                  .HasForeignKey(m => m.CourseId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Faqs)
                  .WithOne(f => f.Course)
                  .HasForeignKey(f => f.CourseId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CourseModule>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);

            entity.HasMany(e => e.Lessons)
                  .WithOne(l => l.Module)
                  .HasForeignKey(l => l.ModuleId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CourseLesson>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
        });

        modelBuilder.Entity<CourseFaq>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Question).IsRequired().HasMaxLength(300);
            entity.Property(e => e.Answer).IsRequired().HasMaxLength(1500);
        });
    }
}
