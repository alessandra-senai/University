﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using University.Context;

#nullable disable

namespace University.Migrations
{
    [DbContext(typeof(UniversityContext))]
    [Migration("20220818004742_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("University.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Nome");

                    b.Property<string>("Requirement")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Requisito");

                    b.Property<double>("Value")
                        .HasColumnType("float")
                        .HasColumnName("Valor");

                    b.Property<int?>("Workload")
                        .HasColumnType("int")
                        .HasColumnName("CargaHoraria");

                    b.HasKey("Id");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("University.Models.Instructor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Certificates")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Certificados");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Nome");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Telefone");

                    b.Property<double?>("ValueHour")
                        .HasColumnType("float")
                        .HasColumnName("ValorHora");

                    b.HasKey("Id");

                    b.ToTable("Instrutor");
                });

            modelBuilder.Entity("University.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdPeriodNote")
                        .HasColumnType("int")
                        .HasColumnName("IdNotaPeriodo");

                    b.Property<int>("IdRegistration")
                        .HasColumnType("int")
                        .HasColumnName("IdMatricula");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Nota");

                    b.HasKey("Id");

                    b.HasIndex("IdPeriodNote");

                    b.HasIndex("IdRegistration");

                    b.ToTable("Nota");
                });

            modelBuilder.Entity("University.Models.PeriodNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Period")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Periodo");

                    b.HasKey("Id");

                    b.ToTable("NotaPeriodo");
                });

            modelBuilder.Entity("University.Models.Registration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdStudent")
                        .HasColumnType("int")
                        .HasColumnName("IdAluno");

                    b.Property<int>("IdTeam")
                        .HasColumnType("int")
                        .HasColumnName("IdTurma");

                    b.Property<DateTime?>("RegistrationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataMatricula");

                    b.HasKey("Id");

                    b.HasIndex("IdStudent");

                    b.HasIndex("IdTeam");

                    b.ToTable("Matricula");
                });

            modelBuilder.Entity("University.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataNascimento");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Nome");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Telefone");

                    b.HasKey("Id");

                    b.ToTable("Aluno");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Birthday = new DateTime(2010, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CPF = "111.222.333.40-0",
                            Email = "aluno1@gmail.com",
                            Name = "Aluno 1",
                            Phone = "2132-3344"
                        },
                        new
                        {
                            Id = 2,
                            Birthday = new DateTime(2010, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            CPF = "222.222.333.40-0",
                            Email = "aluno2@gmail.com",
                            Name = "Aluno 2",
                            Phone = "4556-3344"
                        });
                });

            modelBuilder.Entity("University.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CourseAwayId")
                        .HasColumnType("int");

                    b.Property<int>("CourseTeamId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FinishDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataFinal");

                    b.Property<int>("IdInstructor")
                        .HasColumnType("int")
                        .HasColumnName("IdInstrutor");

                    b.Property<DateTime?>("InitialDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("DataInicio");

                    b.Property<int?>("Workload")
                        .HasColumnType("int")
                        .HasColumnName("CargaHoraria");

                    b.HasKey("Id");

                    b.HasIndex("CourseAwayId");

                    b.HasIndex("CourseTeamId");

                    b.HasIndex("IdInstructor");

                    b.ToTable("Turma");
                });

            modelBuilder.Entity("University.Models.Note", b =>
                {
                    b.HasOne("University.Models.PeriodNote", "PeriodNote")
                        .WithMany()
                        .HasForeignKey("IdPeriodNote")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("University.Models.Registration", "Registration")
                        .WithMany()
                        .HasForeignKey("IdRegistration")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PeriodNote");

                    b.Navigation("Registration");
                });

            modelBuilder.Entity("University.Models.Registration", b =>
                {
                    b.HasOne("University.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("IdStudent")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("University.Models.Team", "Team")
                        .WithMany()
                        .HasForeignKey("IdTeam")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("University.Models.Team", b =>
                {
                    b.HasOne("University.Models.Course", "CourseAway")
                        .WithMany()
                        .HasForeignKey("CourseAwayId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("University.Models.Course", "CourseTeam")
                        .WithMany()
                        .HasForeignKey("CourseTeamId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("University.Models.Instructor", "Instructor")
                        .WithMany()
                        .HasForeignKey("IdInstructor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseAway");

                    b.Navigation("CourseTeam");

                    b.Navigation("Instructor");
                });
#pragma warning restore 612, 618
        }
    }
}
