﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;
using PoliticPolls.DataModel;

namespace PoliticPolls.DataModel.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("POLLSDB")
                .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            modelBuilder.Entity("PoliticPolls.DataModel.Orders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("NUMBER");

                    b.Property<int?>("IdPolitician")
                        .HasColumnName("id_politician")
                        .HasColumnType("NUMBER");

                    b.Property<string>("Text")
                        .HasColumnName("text")
                        .HasColumnType("VARCHAR2(1000)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("pk_orders_id");

                    b.HasIndex("IdPolitician");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("PoliticPolls.DataModel.OrderSets", b =>
                {
                    b.Property<int>("IdPoll")
                        .HasColumnName("id_poll")
                        .HasColumnType("NUMBER");

                    b.Property<int>("IdOrder")
                        .HasColumnName("id_order")
                        .HasColumnType("NUMBER");

                    b.HasKey("IdPoll", "IdOrder");

                    b.HasIndex("IdOrder");

                    b.HasIndex("IdPoll", "IdOrder")
                        .IsUnique()
                        .HasName("_1");

                    b.ToTable("order_sets");
                });

            modelBuilder.Entity("PoliticPolls.DataModel.Politicians", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("NUMBER");

                    b.Property<int>("IdTerritory")
                        .HasColumnName("id_territory")
                        .HasColumnType("NUMBER");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("VARCHAR2(100)");

                    b.Property<string>("Patro")
                        .HasColumnName("patro")
                        .HasColumnType("VARCHAR2(100)");

                    b.Property<string>("Surname")
                        .HasColumnName("surname")
                        .HasColumnType("VARCHAR2(100)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("pk_politicians_id");

                    b.HasIndex("IdTerritory");

                    b.ToTable("politicians");
                });

            modelBuilder.Entity("PoliticPolls.DataModel.PoliticianSets", b =>
                {
                    b.Property<int>("IdPoll")
                        .HasColumnName("id_poll")
                        .HasColumnType("NUMBER");

                    b.Property<int>("IdPolitician")
                        .HasColumnName("id_politician")
                        .HasColumnType("NUMBER");

                    b.Property<int?>("Rating")
                        .HasColumnName("rating")
                        .HasColumnType("NUMBER");

                    b.HasKey("IdPoll", "IdPolitician");

                    b.HasIndex("IdPolitician");

                    b.HasIndex("IdPoll", "IdPolitician")
                        .IsUnique()
                        .HasName("_0");

                    b.ToTable("politician_sets");
                });

            modelBuilder.Entity("PoliticPolls.DataModel.Poll", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("NUMBER");

                    b.Property<int>("IdRespondent")
                        .HasColumnName("id_respondent")
                        .HasColumnType("NUMBER");

                    b.Property<DateTime?>("PollDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("poll_date")
                        .HasColumnType("DATE")
                        .HasDefaultValueSql("CURRENT_DATE ");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("pk_poll_id");

                    b.HasIndex("IdRespondent")
                        .IsUnique()
                        .HasName("unq_poll_id_respondent");

                    b.ToTable("poll");
                });

            modelBuilder.Entity("PoliticPolls.DataModel.Respondents", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("NUMBER");

                    b.Property<DateTime?>("BirthDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("birth_date")
                        .HasColumnType("DATE")
                        .HasDefaultValueSql("CURRENT_DATE ");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("VARCHAR2(100)");

                    b.Property<string>("Patro")
                        .HasColumnName("patro")
                        .HasColumnType("VARCHAR2(100)");

                    b.Property<string>("Surname")
                        .HasColumnName("surname")
                        .HasColumnType("VARCHAR2(100)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("pk_respondents_id");

                    b.ToTable("respondents");
                });

            modelBuilder.Entity("PoliticPolls.DataModel.Terrtitory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("NUMBER");

                    b.Property<string>("TerritoryName")
                        .HasColumnName("territory_name")
                        .HasColumnType("VARCHAR2(500)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasName("pk_terrtitory_id");

                    b.ToTable("terrtitory");
                });

            modelBuilder.Entity("PoliticPolls.DataModel.Orders", b =>
                {
                    b.HasOne("PoliticPolls.DataModel.Politicians", "Politician")
                        .WithMany("Orders")
                        .HasForeignKey("IdPolitician")
                        .HasConstraintName("fk_orders_politicians");
                });

            modelBuilder.Entity("PoliticPolls.DataModel.OrderSets", b =>
                {
                    b.HasOne("PoliticPolls.DataModel.Orders", "Order")
                        .WithMany("OrderSets")
                        .HasForeignKey("IdOrder")
                        .HasConstraintName("fk_order_sets_orders");

                    b.HasOne("PoliticPolls.DataModel.Poll", "Poll")
                        .WithMany("Orders")
                        .HasForeignKey("IdPoll")
                        .HasConstraintName("fk_order_sets_poll");
                });

            modelBuilder.Entity("PoliticPolls.DataModel.Politicians", b =>
                {
                    b.HasOne("PoliticPolls.DataModel.Terrtitory", "Terrtitory")
                        .WithMany("Politicians")
                        .HasForeignKey("IdTerritory")
                        .HasConstraintName("fk_politicians_terrtitory");
                });

            modelBuilder.Entity("PoliticPolls.DataModel.PoliticianSets", b =>
                {
                    b.HasOne("PoliticPolls.DataModel.Politicians", "Politician")
                        .WithMany("PoliticianSets")
                        .HasForeignKey("IdPolitician")
                        .HasConstraintName("fk_politician_sets_politicians");

                    b.HasOne("PoliticPolls.DataModel.Poll", "Poll")
                        .WithMany("Politicians")
                        .HasForeignKey("IdPoll")
                        .HasConstraintName("fk_politician_sets_poll");
                });

            modelBuilder.Entity("PoliticPolls.DataModel.Poll", b =>
                {
                    b.HasOne("PoliticPolls.DataModel.Respondents", "Respondent")
                        .WithOne("Poll")
                        .HasForeignKey("PoliticPolls.DataModel.Poll", "IdRespondent")
                        .HasConstraintName("fk_poll_respondents");
                });
#pragma warning restore 612, 618
        }
    }
}
