using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OuTouchFilms.Server.Entity.Models;

public partial class AivenDbContext : DbContext
{
    public AivenDbContext()
    {
    }

    public AivenDbContext(DbContextOptions<AivenDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Condition> Conditions { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<FilmGroup> FilmGroups { get; set; }

    public virtual DbSet<FilmStaff> FilmStaffs { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Staff> Staffs { get; set; }

    public virtual DbSet<Studio> Studios { get; set; }

    public virtual DbSet<Translation> Translations { get; set; }

    public virtual DbSet<UserFilm> UserFilms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=outouchdb-outouchdb.aivencloud.com;Username=avnadmin;Database=Films;Password=AVNS_GC4NcSf0AYj7lCC8pVx;Port=14507");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FilmComments");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"FilmComments_Id_seq\"'::regclass)");

            entity.HasOne(d => d.Film).WithMany(p => p.Comments)
                .HasForeignKey(d => d.FilmId)
                .HasConstraintName("FK_FilmComments_Films_FilmId");
        });

        modelBuilder.Entity<Condition>(entity =>
        {
            entity.Property(e => e.IsReverse).HasDefaultValue(false);
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.Property(e => e.ImdbRating).HasDefaultValueSql("0");
            entity.Property(e => e.KinopoiskRating).HasDefaultValueSql("0");
        });

        modelBuilder.Entity<FilmGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("untitled_table_201_pkey");

            entity.ToTable("FilmGroup");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"untitled_table_201_Id_seq\"'::regclass)");

            entity.HasOne(d => d.Condition).WithMany(p => p.FilmGroups)
                .HasForeignKey(d => d.ConditionId)
                .HasConstraintName("untitled_table_201_ConditionId_fkey");
        });

        modelBuilder.Entity<FilmStaff>(entity =>
        {
            entity.HasIndex(e => e.FilmId, "IX_FilmStaffs_FilmId");

            entity.HasIndex(e => e.StaffId, "IX_FilmStaffs_StaffId");

            entity.HasOne(d => d.Film).WithMany(p => p.FilmStaffs).HasForeignKey(d => d.FilmId);

            entity.HasOne(d => d.Staff).WithMany(p => p.FilmStaffs).HasForeignKey(d => d.StaffId);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FilmGenres");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"FilmGenres_Id_seq\"'::regclass)");
        });

        modelBuilder.Entity<Translation>(entity =>
        {
            entity.Property(e => e.KodikId).HasDefaultValue(0);
        });

        modelBuilder.Entity<UserFilm>(entity =>
        {
            entity.HasIndex(e => e.FilmId, "IX_UserFilms_FilmId");

            entity.Property(e => e.AddedDate).HasDefaultValueSql("'-infinity'::timestamp with time zone");

            entity.HasOne(d => d.Film).WithMany(p => p.UserFilms).HasForeignKey(d => d.FilmId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
