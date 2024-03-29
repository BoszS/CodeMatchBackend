﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using code_match_backend.models;

namespace code_match_backend.models
{
    public class CodeMatchContext : DbContext
    {
        public CodeMatchContext(DbContextOptions<CodeMatchContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<AssignmentTag> AssignmentTags { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyTag> CompanyTags { get; set; }
        public DbSet<Maker> Makers { get; set; }
        public DbSet<MakerTag> MakerTags { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Tag> Tags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Permission>().ToTable("Permission");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<RolePermission>().ToTable("RolePermission");
            modelBuilder.Entity<Application>().ToTable("Application");
            modelBuilder.Entity<Assignment>().ToTable("Assignment");
            modelBuilder.Entity<AssignmentTag>().ToTable("AssignmentTag");
            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<CompanyTag>().ToTable("CompanyTag");
            modelBuilder.Entity<Maker>().ToTable("Maker");
            modelBuilder.Entity<MakerTag>().ToTable("MakerTag");
            modelBuilder.Entity<Tag>().ToTable("Tag");
            modelBuilder.Entity<Review>()
                .HasKey(e => new { e.ReviewID });

            modelBuilder.Entity<Review>()
                .HasOne(e => e.Sender)
                .WithMany(e => e.SendReviews)
                .HasForeignKey(e => e.UserIDSender)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(e => e.Receiver)
                .WithMany(e => e.ReceivedReviews)
                .HasForeignKey(e => e.UserIDReceiver)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<code_match_backend.models.Notification> Notification { get; set; }
    }
}
