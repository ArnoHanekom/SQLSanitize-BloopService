using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Data.Processing.Models;

#nullable disable

namespace Data.Processing.Context
{
    public partial class BloopDbContext : DbContext
    {
        public BloopDbContext(DbContextOptions<BloopDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Sensitiveword> Sensitivewords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Sensitiveword>(entity =>
            {
                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.WordDefinition)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
