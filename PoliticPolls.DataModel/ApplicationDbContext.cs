using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PoliticPolls.DataModel
{
    public partial class ApplicationDbContext : DbContext
    {
        //public ApplicationDbContext()
        //{
        //}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrderSets> OrderSets { get; set; }
        public virtual DbSet<Politicians> Politicians { get; set; }
        public virtual DbSet<PoliticianSets> PoliticianSets { get; set; }
        public virtual DbSet<Poll> Poll { get; set; }
        public virtual DbSet<Respondents> Respondents { get; set; }
        public virtual DbSet<Terrtitory> Terrtitory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=StudyOracle)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=XE)));User Id=POLLSDB;Password=POLLSDB;");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:DefaultSchema", "POLLSDB");

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("orders");

                entity.HasIndex(e => e.Id)
                    .HasName("pk_orders_id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.IdPolitician)
                    .HasColumnName("id_politician")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Text)
                    .HasColumnName("text")
                    .HasColumnType("VARCHAR2(1000)");

                entity.HasOne(d => d.IdPoliticianNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdPolitician)
                    .HasConstraintName("fk_orders_politicians");
            });

            modelBuilder.Entity<OrderSets>(entity =>
            {
                entity.HasKey(e => new { e.IdPoll, e.IdOrder });

                entity.ToTable("order_sets");

                entity.HasIndex(e => new { e.IdPoll, e.IdOrder })
                    .HasName("_1")
                    .IsUnique();

                entity.Property(e => e.IdPoll)
                    .HasColumnName("id_poll")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.IdOrder)
                    .HasColumnName("id_order")
                    .HasColumnType("NUMBER");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.OrderSets)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_order_sets_orders");

                entity.HasOne(d => d.IdPollNavigation)
                    .WithMany(p => p.OrderSets)
                    .HasForeignKey(d => d.IdPoll)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_order_sets_poll");
            });

            modelBuilder.Entity<Politicians>(entity =>
            {
                entity.ToTable("politicians");

                entity.HasIndex(e => e.Id)
                    .HasName("pk_politicians_id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.IdTerritory)
                    .HasColumnName("id_territory")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.Patro)
                    .HasColumnName("patro")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.Surname)
                    .HasColumnName("surname")
                    .HasColumnType("VARCHAR2(100)");

                entity.HasOne(d => d.IdTerritoryNavigation)
                    .WithMany(p => p.Politicians)
                    .HasForeignKey(d => d.IdTerritory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_politicians_terrtitory");
            });

            modelBuilder.Entity<PoliticianSets>(entity =>
            {
                entity.HasKey(e => new { e.IdPoll, e.IdPolitician });

                entity.ToTable("politician_sets");

                entity.HasIndex(e => new { e.IdPoll, e.IdPolitician })
                    .HasName("_0")
                    .IsUnique();

                entity.Property(e => e.IdPoll)
                    .HasColumnName("id_poll")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.IdPolitician)
                    .HasColumnName("id_politician")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("NUMBER");

                entity.HasOne(d => d.IdPoliticianNavigation)
                    .WithMany(p => p.PoliticianSets)
                    .HasForeignKey(d => d.IdPolitician)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_politician_sets_politicians");

                entity.HasOne(d => d.IdPollNavigation)
                    .WithMany(p => p.PoliticianSets)
                    .HasForeignKey(d => d.IdPoll)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_politician_sets_poll");
            });

            modelBuilder.Entity<Poll>(entity =>
            {
                entity.ToTable("poll");

                entity.HasIndex(e => e.Id)
                    .HasName("pk_poll_id")
                    .IsUnique();

                entity.HasIndex(e => e.IdRespondent)
                    .HasName("unq_poll_id_respondent")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.IdRespondent)
                    .HasColumnName("id_respondent")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.PollDate)
                    .HasColumnName("poll_date")
                    .HasColumnType("DATE")
                    .HasDefaultValueSql("CURRENT_DATE ");

                entity.HasOne(d => d.IdRespondentNavigation)
                    .WithOne(p => p.Poll)
                    .HasForeignKey<Poll>(d => d.IdRespondent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_poll_respondents");
            });

            modelBuilder.Entity<Respondents>(entity =>
            {
                entity.ToTable("respondents");

                entity.HasIndex(e => e.Id)
                    .HasName("pk_respondents_id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birth_date")
                    .HasColumnType("DATE")
                    .HasDefaultValueSql("CURRENT_DATE ");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.Patro)
                    .HasColumnName("patro")
                    .HasColumnType("VARCHAR2(100)");

                entity.Property(e => e.Surname)
                    .HasColumnName("surname")
                    .HasColumnType("VARCHAR2(100)");
            });

            modelBuilder.Entity<Terrtitory>(entity =>
            {
                entity.ToTable("terrtitory");

                entity.HasIndex(e => e.Id)
                    .HasName("pk_terrtitory_id")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.TerritoryName)
                    .HasColumnName("territory_name")
                    .HasColumnType("VARCHAR2(500)");
            });
        }
    }
}
