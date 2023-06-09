﻿// <auto-generated />
using System;
using FCF.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FCF.Data.Migrations
{
    [DbContext(typeof(MainDBContext))]
    [Migration("20230327145757_fcf_initial")]
    partial class fcf_initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FCF.Entities.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date_time")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TeamId1")
                        .HasColumnType("int");

                    b.Property<int?>("TeamId2")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId1");

                    b.HasIndex("TeamId2");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("FCF.Entities.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeamId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeamId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("FCF.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Designation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Division")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("TeamId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FCF.Entities.Match", b =>
                {
                    b.HasOne("FCF.Entities.Team", "Team1")
                        .WithMany()
                        .HasForeignKey("TeamId1");

                    b.HasOne("FCF.Entities.Team", "Team2")
                        .WithMany()
                        .HasForeignKey("TeamId2")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Team1");

                    b.Navigation("Team2");
                });

            modelBuilder.Entity("FCF.Entities.User", b =>
                {
                    b.HasOne("FCF.Entities.Team", "Team_")
                        .WithMany()
                        .HasForeignKey("TeamId");

                    b.Navigation("Team_");
                });
#pragma warning restore 612, 618
        }
    }
}
