﻿// <auto-generated />
using System;
using API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Data.ApplicationTables
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("API.Models.Batch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BatchName")
                        .HasColumnType("longtext");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Technology")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Batches");
                });

            modelBuilder.Entity("API.Models.BatchModule", b =>
                {
                    b.Property<int>("BatchId")
                        .HasColumnType("int");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext");

                    b.HasKey("BatchId", "ModuleId");

                    b.HasIndex("ModuleId");

                    b.ToTable("BatchModules");
                });

            modelBuilder.Entity("API.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BatchId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("BatchId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("API.Models.CourseSkills", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.HasKey("CourseId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("CourseSkills");
                });

            modelBuilder.Entity("API.Models.LearnModule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Certification_Type")
                        .HasColumnType("longtext");

                    b.Property<string>("Learning_Type")
                        .HasColumnType("longtext");

                    b.Property<string>("Module_Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Proefficiency_level")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("API.Models.SkillModule", b =>
                {
                    b.Property<int>("SkillId")
                        .HasColumnType("int");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext");

                    b.HasKey("SkillId", "ModuleId");

                    b.HasIndex("ModuleId");

                    b.ToTable("SkillModules");
                });

            modelBuilder.Entity("API.Models.Skills", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Skill_Family")
                        .HasColumnType("longtext");

                    b.Property<string>("Skill_Name")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("API.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int?>("BatchId")
                        .HasColumnType("int");

                    b.Property<int?>("BatchModuleBatchId")
                        .HasColumnType("int");

                    b.Property<int?>("BatchModuleModuleId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<int?>("LearnModuleId")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<int?>("SkillModuleModuleId")
                        .HasColumnType("int");

                    b.Property<int?>("SkillModuleSkillId")
                        .HasColumnType("int");

                    b.Property<int?>("SkillsId")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("BatchId");

                    b.HasIndex("CourseId");

                    b.HasIndex("LearnModuleId");

                    b.HasIndex("SkillsId");

                    b.HasIndex("BatchModuleBatchId", "BatchModuleModuleId");

                    b.HasIndex("SkillModuleSkillId", "SkillModuleModuleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("API.Models.BatchModule", b =>
                {
                    b.HasOne("API.Models.Batch", "Batch")
                        .WithMany("BatchModules")
                        .HasForeignKey("BatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.LearnModule", "Module")
                        .WithMany("BatchModules")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Batch");

                    b.Navigation("Module");
                });

            modelBuilder.Entity("API.Models.Course", b =>
                {
                    b.HasOne("API.Models.Batch", "Batch")
                        .WithMany()
                        .HasForeignKey("BatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Batch");
                });

            modelBuilder.Entity("API.Models.CourseSkills", b =>
                {
                    b.HasOne("API.Models.Course", "Course")
                        .WithMany("CourseSkills")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Skills", "Skills")
                        .WithMany("CourseSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Skills");
                });

            modelBuilder.Entity("API.Models.SkillModule", b =>
                {
                    b.HasOne("API.Models.LearnModule", "Module")
                        .WithMany("SkillModules")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Models.Skills", "Skills")
                        .WithMany("SkillModules")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");

                    b.Navigation("Skills");
                });

            modelBuilder.Entity("API.Models.User", b =>
                {
                    b.HasOne("API.Models.Batch", null)
                        .WithMany("Users")
                        .HasForeignKey("BatchId");

                    b.HasOne("API.Models.Course", null)
                        .WithMany("Users")
                        .HasForeignKey("CourseId");

                    b.HasOne("API.Models.LearnModule", null)
                        .WithMany("Users")
                        .HasForeignKey("LearnModuleId");

                    b.HasOne("API.Models.Skills", null)
                        .WithMany("User")
                        .HasForeignKey("SkillsId");

                    b.HasOne("API.Models.BatchModule", null)
                        .WithMany("Users")
                        .HasForeignKey("BatchModuleBatchId", "BatchModuleModuleId");

                    b.HasOne("API.Models.SkillModule", null)
                        .WithMany("Users")
                        .HasForeignKey("SkillModuleSkillId", "SkillModuleModuleId");
                });

            modelBuilder.Entity("API.Models.Batch", b =>
                {
                    b.Navigation("BatchModules");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("API.Models.BatchModule", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("API.Models.Course", b =>
                {
                    b.Navigation("CourseSkills");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("API.Models.LearnModule", b =>
                {
                    b.Navigation("BatchModules");

                    b.Navigation("SkillModules");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("API.Models.SkillModule", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("API.Models.Skills", b =>
                {
                    b.Navigation("CourseSkills");

                    b.Navigation("SkillModules");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
