using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RestWithMysql.dbContext;

public partial class DotnetdbContext : DbContext
{
   /* public DotnetdbContext()
    {
    }*/

    public DotnetdbContext(DbContextOptions<DotnetdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

  /*  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=root123;database=dotnetdb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));
*/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Cid).HasName("PRIMARY");

            entity.ToTable("course");

            entity.Property(e => e.Cid)
                .ValueGeneratedNever()
                .HasColumnName("cid");
            entity.Property(e => e.CourseName)
                .HasMaxLength(20)
                .HasColumnName("courseName");
            entity.Property(e => e.Intake).HasColumnName("intake");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Sid).HasName("PRIMARY");

            entity.ToTable("student");

            entity.HasIndex(e => e.Cid, "cid");

            entity.Property(e => e.Sid)
                .ValueGeneratedNever()
                .HasColumnName("sid");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Cid).HasColumnName("cid");
            entity.Property(e => e.City)
                .HasMaxLength(25)
                .IsFixedLength()
                .HasColumnName("city");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .HasColumnName("firstName");

            entity.HasOne(d => d.CidNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.Cid)
                .HasConstraintName("student_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
