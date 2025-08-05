using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VotacionesApi.Models;

public partial class ElectoralContext : DbContext
{
    public ElectoralContext()
    {
    }

    public ElectoralContext(DbContextOptions<ElectoralContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Candidate> Candidates { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }

    public virtual DbSet<Voter> Voters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Electoral;Username=postgres;Password=mypostgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Candidate_pkey");

            entity.ToTable("Candidate");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .HasColumnName("name");
            entity.Property(e => e.Party)
                .HasMaxLength(320)
                .HasColumnName("party");
            entity.Property(e => e.Votes)
                .HasDefaultValue(0)
                .HasColumnName("votes");
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Vote_pkey");

            entity.ToTable("Vote");

            entity.HasIndex(e => e.CandidateId, "fki_candidate_fk");

            entity.HasIndex(e => e.VoterId, "fki_voter_fk");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CandidateId).HasColumnName("candidate_id");
            entity.Property(e => e.VoterId).HasColumnName("voter_id");

            entity.HasOne(d => d.Candidate).WithMany(p => p.VotesNavigation)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("candidate_fk");

            entity.HasOne(d => d.Voter).WithMany(p => p.Votes)
                .HasForeignKey(d => d.VoterId)
                .HasConstraintName("voter_fk");
        });

        modelBuilder.Entity<Voter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Voter_pkey");

            entity.ToTable("Voter");

            entity.HasIndex(e => e.Email, "email_unique").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .HasColumnName("email");
            entity.Property(e => e.HasVoted)
                .HasDefaultValue(false)
                .HasColumnName("has_voted");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
