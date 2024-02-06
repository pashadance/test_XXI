﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using stoneXXI;

#nullable disable

namespace stoneXXI.Migrations
{
    [DbContext(typeof(Repository))]
    partial class RepositoryModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("stomeXXI.Models+Candidate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CurrentStage")
                        .HasColumnType("integer");

                    b.Property<int?>("HrId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsNeedTestTask")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPassedProbationaryPeriod")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("VacancyId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HrId");

                    b.HasIndex("VacancyId");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("stomeXXI.Models+Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("stomeXXI.Models+HrSpecialist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Hrs");
                });

            modelBuilder.Entity("stomeXXI.Models+Vacancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("HrSpecialistId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TestTask")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("HrSpecialistId");

                    b.ToTable("Vacancies");
                });

            modelBuilder.Entity("stomeXXI.Models+Candidate", b =>
                {
                    b.HasOne("stomeXXI.Models+HrSpecialist", "Hr")
                        .WithMany()
                        .HasForeignKey("HrId");

                    b.HasOne("stomeXXI.Models+Vacancy", "Vacancy")
                        .WithMany()
                        .HasForeignKey("VacancyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hr");

                    b.Navigation("Vacancy");
                });

            modelBuilder.Entity("stomeXXI.Models+Vacancy", b =>
                {
                    b.HasOne("stomeXXI.Models+Department", "Department")
                        .WithMany("Vacancies")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("stomeXXI.Models+HrSpecialist", null)
                        .WithMany("AssignedVacancies")
                        .HasForeignKey("HrSpecialistId");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("stomeXXI.Models+Department", b =>
                {
                    b.Navigation("Vacancies");
                });

            modelBuilder.Entity("stomeXXI.Models+HrSpecialist", b =>
                {
                    b.Navigation("AssignedVacancies");
                });
#pragma warning restore 612, 618
        }
    }
}