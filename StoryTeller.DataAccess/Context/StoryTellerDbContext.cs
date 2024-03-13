using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StoryTeller.DataAccess.Models;

namespace StoryTeller.DataAccess.Context;

public partial class StoryTellerDbContext : DbContext
{
    public StoryTellerDbContext()
    {
    }

    public StoryTellerDbContext(DbContextOptions<StoryTellerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<StoryTellerModel> StoryTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-D3EAT3B\\SQLEXPRESS;Database=StoryTeller;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StoryTellerModel>(entity =>
        {
            entity.HasKey(e => e.StoryId);

            entity.ToTable("StoryTable");

            entity.Property(e => e.StoryId).HasColumnName("StoryID");
            entity.Property(e => e.Date).HasColumnType("date");
            entity.Property(e => e.Story).HasMaxLength(1000);
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
