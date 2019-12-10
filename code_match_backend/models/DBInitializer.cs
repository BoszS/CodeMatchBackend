using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace code_match_backend.models
{
    public class DBInitializer
    {
        public static void Initialize(CodeMatchContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            context.Permissions.AddRange(
            new Permission
            {
                Name = "permission 1"
            });
            context.SaveChanges();

            context.Roles.AddRange(
            new Role
            {
                Name = "Maker"
            },
            new Role 
            { 
                Name = "Company"
            }
            );
            context.SaveChanges();

            context.RolePermissions.AddRange(new RolePermission
            {
                Permission = context.Permissions.FirstOrDefault(),
                Role = context.Roles.FirstOrDefault()
            });

            context.SaveChanges();

            context.Companies.AddRange(new Company
            {
                Location = "location company"
            });

            context.SaveChanges();

            context.Makers.AddRange(new Maker
            {
                Dob = new DateTime(1998, 3, 12),
                Experience = "none",
                Firstname = "te",
                Lastname = "st",
                LinkedIn = "www.link.com",
            });
            context.SaveChanges();

            context.Users.AddRange(new User
            {
                Role = context.Roles.FirstOrDefault(),
                Biography = "biobio",
                Email = "test@test.com",
                Password = "test123",
                Phonenumber = "123456789",
                MakerID = 1
            }, new User
            {
                Biography = "user 2 biography",
                Email = "test2@test.com",
                Password = "test123",
                Phonenumber = "987654321",
                Role = context.Roles.SingleOrDefault(r => r.RoleID == 2),
                CompanyID = 1
            });

            context.SaveChanges();

            context.Assignments.AddRange(new Assignment
            {
                Status = "...",
                Location = "assignment location",
                Company = context.Companies.FirstOrDefault(),
                Description = "This is the description of the assignment"
            });

            context.SaveChanges();

            context.Applications.AddRange(new Application
            {
                IsAccepted = true,
                Assignment = context.Assignments.FirstOrDefault(),
                Maker = context.Makers.FirstOrDefault()
            });

            context.SaveChanges();

            context.Reviews.AddRange(new Review
            {
                Assignment = context.Assignments.FirstOrDefault(),
                Description = "very good",
                Receiver = context.Users.Where(r => r.Email == "test@test.com").Single(),
                Sender = context.Users.Where(r => r.Email == "test2@test.com").Single()
            });

            context.SaveChanges();

            context.Tags.AddRange(new Tag
            {
                Name = "front"
            },
            new Tag
            {
                Name = "back"
            });

            context.SaveChanges();

            context.MakerTags.AddRange(new MakerTag
            {
                Maker = context.Makers.FirstOrDefault(),
                Tag = context.Tags.Single(r => r.Name == "front")
            });

            context.SaveChanges();

            context.CompanyTags.AddRange(new CompanyTag
            {
                Company = context.Companies.FirstOrDefault(),
                Tag = context.Tags.Single(r => r.Name == "back")
            });
            context.SaveChanges();}
        }
}
