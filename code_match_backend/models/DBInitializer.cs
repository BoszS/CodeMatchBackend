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
                Name = "MakerSearchApplyAssignment"
            },
            new Permission
            {
                Name = "CompanyCreateAssignment"
            },
            new Permission
            {
                Name = "AdminCRUDReview"
            },
            new Permission
            {
                Name = "AdminCRUDUsers"
            },
            new Permission
            {
                Name = "AdminCRUDAssignments"
            },
            new Permission
            {
                Name = "update_maker_profile"
            },
            new Permission
            {
                Name = "update_company_profile"
            }
            );
            context.SaveChanges();

            context.Roles.AddRange(
            new Role
            {
                Name = "Maker"
            },
            new Role
            {
                Name = "Company"
            },
            new Role
            {
                Name = "Admin"
            }
            );
            context.SaveChanges();

            context.RolePermissions.AddRange(
            new RolePermission
            {
                Permission = context.Permissions.FirstOrDefault(),
                Role = context.Roles.FirstOrDefault()
            },
            new RolePermission
            {
                Permission = context.Permissions.SingleOrDefault(p => p.PermissionID == 2),
                Role = context.Roles.SingleOrDefault(r => r.RoleID == 2)
            },
            new RolePermission
            {
                Permission = context.Permissions.SingleOrDefault(p => p.PermissionID == 3),
                Role = context.Roles.SingleOrDefault(r => r.RoleID == 3)
            },
            new RolePermission
            {
                Permission = context.Permissions.SingleOrDefault(p => p.PermissionID == 4),
                Role = context.Roles.SingleOrDefault(r => r.RoleID == 3)
            },
            new RolePermission
            {
                Permission = context.Permissions.SingleOrDefault(p => p.PermissionID == 5),
                Role = context.Roles.SingleOrDefault(r => r.RoleID == 3)
            },
            new RolePermission
            {
                Permission = context.Permissions.SingleOrDefault(p => p.PermissionID == 6),
                Role = context.Roles.FirstOrDefault()
            },
            new RolePermission
            {
                Permission = context.Permissions.SingleOrDefault(p => p.PermissionID == 7),
                Role = context.Roles.FirstOrDefault()
            }
            );

            context.SaveChanges();

            context.Companies.AddRange(new Company
            {
                StreetAdress = "Schoolstraat",
                PostalCode = 2260
            },
            new Company
            {
                StreetAdress = "Biezenveld 34",
                PostalCode = 2460,
                Name = "Geko bvba"
            }
            );

            context.SaveChanges();

            context.Makers.AddRange(
            new Maker
            {
                Dob = new DateTime(1998, 3, 12),
                Experience = "none",
                Firstname = "te",
                Lastname = "st",
                LinkedIn = "www.link.com",
            },
            new Maker
            {
                Dob = new DateTime(1998, 05, 11),
                Experience = "C#, Angular",
                Firstname = "Bosz",
                Lastname = "Srisan",
                LinkedIn = "www.linkedIn/BoszSrisan",
                Nickname = "Bozie",
            }
            );
            context.SaveChanges();

            context.Users.AddRange(new User
            {
                Role = context.Roles.FirstOrDefault(),
                Biography = "biobio",
                Email = "maker@test.com",
                Password = "maker123",
                Phonenumber = "123456789",
                MakerID = 1
            }, new User
            {
                Biography = "user 2 biography",
                Email = "company@test.com",
                Password = "company123",
                Phonenumber = "987654321",
                Role = context.Roles.SingleOrDefault(r => r.RoleID == 2),
                CompanyID = 1
            },
            new User
            {
                Biography = "administrator",
                Email = "admin@test.com",
                Password = "admin123",
                Phonenumber = "987654321",
                Role = context.Roles.SingleOrDefault(r => r.RoleID == 3),
            },
            new User
            {
                Biography = "ITF student",
                Email = "bosz.srisan@live.com",
                Password = "bozie",
                Phonenumber = "0483476363",
                Role = context.Roles.FirstOrDefault(),
                MakerID = 2
            },
            new User
            {
                Biography = "Plastic bedrijf",
                Email = "plastic@live.com",
                Password = "plastic",
                Phonenumber = "0483473626",
                Role = context.Roles.SingleOrDefault(r => r.RoleID == 2),
                CompanyID = 2
            }
            );

            context.SaveChanges();

            context.Assignments.AddRange(
            new Assignment
            {
                Status = "Initial",
                StreetAdress = "Schoolstraat",
                PostalCode = 2260,
                Company = context.Companies.FirstOrDefault(),
                Description = "Webdesign",
                Name = "De bakkerij website"
            },
            new Assignment
            {
                Status = "InProgress",
                StreetAdress = "Schoolstraat",
                PostalCode = 2260,
                Company = context.Companies.FirstOrDefault(),
                Description = "Datamodeling",
                Name = "Het bakkerij datamodel"
            },
            new Assignment
            {
                Status = "Completed",
                StreetAdress = "Schoolstraat",
                PostalCode = 2260,
                Company = context.Companies.FirstOrDefault(),
                Description = "Backend .NET",
                Name = "De online bakkerij database"
            }
            );

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
                Receiver = context.Users.Where(r => r.Email == "maker@test.com").Single(),
                Sender = context.Users.Where(r => r.Email == "company@test.com").Single()
            });

            context.SaveChanges();

            context.Tags.AddRange(new Tag
            {
                Name = "front"
            },
            new Tag
            {
                Name = "back"
            },
            new Tag
            {
                Name = "English"
            },
            new Tag
            {
                Name = "French"
            },
            new Tag
            {
                Name = "Dutch"
            },
            new Tag
            {
                Name = "Design"
            },
            new Tag
            {
                Name = "Datamodeling"
            },
            new Tag
            {
                Name = "Teamwork"
            },
            new Tag
            {
                Name = "C#"
            },
            new Tag
            {
                Name = "Javascript"
            },
            new Tag
            {
                Name = "node.js"
            },
            new Tag
            {
                Name = "HTML"
            },
            new Tag
            {
                Name = "CSS"
            }
            );

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
            context.SaveChanges();
        }
    }
}
