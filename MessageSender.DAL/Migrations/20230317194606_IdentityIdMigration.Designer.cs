﻿// <auto-generated />
using MessageSender.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MessageSender.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230317194606_IdentityIdMigration")]
    partial class IdentityIdMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MessageSender.DAL.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id", "Name");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("MessageSender.DAL.Models.Professor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ExtraEmail")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MiddleName")
                        .HasColumnType("longtext");

                    b.HasKey("Id", "Email");

                    b.ToTable("Professors");
                });

            modelBuilder.Entity("MessageSender.DAL.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("ExtraEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MiddleName")
                        .HasColumnType("longtext");

                    b.Property<int>("ProfessorId")
                        .HasColumnType("int");

                    b.Property<int>("StudyYearId")
                        .HasColumnType("int");

                    b.HasKey("Id", "Email");

                    b.HasIndex("EventId");

                    b.HasIndex("ProfessorId");

                    b.HasIndex("StudyYearId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("MessageSender.DAL.Models.StudyYear", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id", "Year");

                    b.ToTable("StudyYears");
                });

            modelBuilder.Entity("MessageSender.DAL.Models.Student", b =>
                {
                    b.HasOne("MessageSender.DAL.Models.Event", "Event")
                        .WithMany("Students")
                        .HasForeignKey("EventId")
                        .HasPrincipalKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MessageSender.DAL.Models.Professor", "Professor")
                        .WithMany("Students")
                        .HasForeignKey("ProfessorId")
                        .HasPrincipalKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MessageSender.DAL.Models.StudyYear", "StudyYear")
                        .WithMany("Students")
                        .HasForeignKey("StudyYearId")
                        .HasPrincipalKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");

                    b.Navigation("Professor");

                    b.Navigation("StudyYear");
                });

            modelBuilder.Entity("MessageSender.DAL.Models.Event", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("MessageSender.DAL.Models.Professor", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("MessageSender.DAL.Models.StudyYear", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}