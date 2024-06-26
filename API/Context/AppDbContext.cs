using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Context
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BatchFaculty>().HasKey(bf=>new{bf.UserId,bf.BatchId});

            modelBuilder.Entity<BatchFaculty>()
            .HasOne(bf=>bf.User)
            .WithMany(bf=>bf.BatchFaculties)
            .HasForeignKey(bf=>bf.UserId);

            modelBuilder.Entity<BatchFaculty>()
            .HasOne(bf=>bf.Batch)
            .WithMany(bf=>bf.BatchFaculties)
            .HasForeignKey(bf=>bf.BatchId);

            modelBuilder.Entity<BatchModule>().HasKey(bm=>new{bm.BatchId,bm.ModuleId});

            modelBuilder.Entity<BatchModule>()
            .HasOne(b=>b.Batch)
            .WithMany(b=>b.BatchModules)
            .HasForeignKey(b=>b.BatchId);

            modelBuilder.Entity<BatchModule>()
            .HasOne(m=>m.Module)
            .WithMany(m=>m.BatchModules)
            .HasForeignKey(m=>m.ModuleId);
             modelBuilder.Entity<CourseSkills>().HasKey(cs=>new{cs.CourseId,cs.SkillId});

            modelBuilder.Entity<CourseSkills>()
            .HasOne(cs=>cs.Course)
            .WithMany(cs=>cs.CourseSkills)
            .HasForeignKey(cs=>cs.CourseId);

            modelBuilder.Entity<CourseSkills>()
            .HasOne(cs=>cs.Skills)
            .WithMany(cs=>cs.CourseSkills)
            .HasForeignKey(cs=>cs.SkillId);

            modelBuilder.Entity<SkillModule>().HasKey(sm=>new{sm.SkillId,sm.ModuleId});

            modelBuilder.Entity<SkillModule>()
            .HasOne(s=>s.Skills)
            .WithMany(s=>s.SkillModules)
            .HasForeignKey(s=>s.SkillId);

            modelBuilder.Entity<SkillModule>()
            .HasOne(m=>m.Module)
            .WithMany(m=>m.SkillModules)
            .HasForeignKey(m=>m.ModuleId);
        }

        public DbSet<Skills> Skills {get;set;}
        public DbSet<Batch> Batches {get;set;}
        public DbSet<LearnModule>Modules{get;set;}
        public DbSet<SkillModule> SkillModules {get;set;}
        public DbSet<BatchModule> BatchModules {get;set;}
        public DbSet<BatchFaculty> BatchFaculties {get;set;}
        public DbSet<Course> Courses {get;set;}
        public DbSet<CourseSkills> CourseSkills {get;set;}

    }
}