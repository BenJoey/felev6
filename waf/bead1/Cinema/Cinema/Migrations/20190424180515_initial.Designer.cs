﻿// <auto-generated />
using System;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cinema.Migrations
{
    [DbContext(typeof(CinemaContext))]
    [Migration("20190424180515_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cinema.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FullName");

                    b.Property<string>("Password");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Cinema.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Director");

                    b.Property<TimeSpan>("Length");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("PosterPath");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Cinema.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("NumOfCols");

                    b.Property<int>("NumOfRows");

                    b.Property<string>("RoomName");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Cinema.Models.Seat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Col");

                    b.Property<string>("NameReserved");

                    b.Property<string>("PhoneNum");

                    b.Property<int?>("Rooms");

                    b.Property<int>("Row");

                    b.Property<int>("ShowRefId");

                    b.Property<int>("State");

                    b.HasKey("Id");

                    b.HasIndex("Rooms");

                    b.HasIndex("ShowRefId");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("Cinema.Models.Show", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Movies");

                    b.Property<int>("RoomRefId");

                    b.Property<DateTime>("StartTime");

                    b.HasKey("Id");

                    b.HasIndex("Movies");

                    b.HasIndex("RoomRefId");

                    b.ToTable("Shows");
                });

            modelBuilder.Entity("Cinema.Models.Seat", b =>
                {
                    b.HasOne("Cinema.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("Rooms");

                    b.HasOne("Cinema.Models.Show", "Show")
                        .WithMany()
                        .HasForeignKey("ShowRefId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Cinema.Models.Show", b =>
                {
                    b.HasOne("Cinema.Models.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("Movies");

                    b.HasOne("Cinema.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomRefId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}