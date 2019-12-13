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
            },
            new Permission
            {
                Name = "read_maker_profile"
            },
            new Permission
            {
                Name = "read_company_profile"
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
                Role = context.Roles.SingleOrDefault(r => r.RoleID == 2)
            },
            new RolePermission
            {
                Permission = context.Permissions.SingleOrDefault(p => p.PermissionID == 8),
                Role = context.Roles.FirstOrDefault()
            },
            new RolePermission
            {
                Permission = context.Permissions.SingleOrDefault(p => p.PermissionID == 9),
                Role = context.Roles.SingleOrDefault(r => r.RoleID == 2)
            }
            );

            context.SaveChanges();

            context.Companies.AddRange(new Company
            {
                StreetAdress = "Schoolstraat",
                PostalCode = 2260,
                Name = "Kwb Heultje"

            },
            new Company
            {
                StreetAdress = "Biezenveld 34",
                PostalCode = 2460,
                Name = "Geko bvba"
            });


            context.SaveChanges();

            context.Makers.AddRange(
            new Maker
            {
                Dob = new DateTime(1998, 3, 12),
                Experience = "none",
                Firstname = "te",
                Lastname = "st",
                LinkedIn = "www.link.com",
                Nickname= "Test"
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
            },
            new Assignment
            {
                Status = "Initial",
                StreetAdress = "Schoolstraat",
                PostalCode = 2260,
                Company = context.Companies.FirstOrDefault(),
                Description = "Hallo, ik ben slager Jan. Ik ben op zoek naar iemand die voor mijn prestigieuze slagerij een website kan maken! Ik ben op zoek naar een vriendelijke student/freelancer" +
                "die me hierbij kan helpen",
                Name = "De slager website"
            },
            new Assignment
            {
                Status = "Initial",
                StreetAdress = "Schoolstraat",
                PostalCode = 2260,
                Company = context.Companies.FirstOrDefault(),
                Description = "Hallo, ik ben directeur Bart. Ik ben op zoek naar iemand die voor mijn prestigieuze school een website kan maken! Ik ben op zoek naar een vriendelijke student/freelancer" +
                "die me hierbij kan helpen",
                Name = "De school website"
            }
            );

            context.SaveChanges();

            context.Applications.AddRange(
            new Application
            {
                IsAccepted = true,
                Assignment = context.Assignments.Where(r => r.Name == "Het bakkerij datamodel").Single(),
                Maker = context.Makers.FirstOrDefault()
            }, 
            new Application
            {
                IsAccepted = true,
                Assignment = context.Assignments.Where(r => r.Name == "De bakkerij website").Single(),
                Maker = context.Makers.FirstOrDefault()
            },
            new Application
            {
                IsAccepted = true,
                Assignment = context.Assignments.Where(r => r.Name == "De online bakkerij database").Single(),
                Maker = context.Makers.FirstOrDefault()
            }
            );

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
                Name = "Front-end"
            },
            new Tag
            {
                Name = "Back-end"
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
            },
            new Tag
            {
                Name = "Sql"
            },
            new Tag
            {
                Name = ".NET"
            }
            );

            context.SaveChanges();

            context.MakerTags.AddRange(new MakerTag
            {
                Maker = context.Makers.FirstOrDefault(),
                Tag = context.Tags.Single(r => r.Name == "Front-end")
            });

            context.SaveChanges();

            context.CompanyTags.AddRange(new CompanyTag
            {
                Company = context.Companies.FirstOrDefault(),
                Tag = context.Tags.Single(r => r.Name == "Back-end")
            });
            context.SaveChanges();


            context.Notification.AddRange(
                new Notification
                {
                    Sender = context.Users.FirstOrDefault(),
                    Receiver = context.Users.Single(u => u.Email == "company@test.com"),
                    ApplicationID = 2,
                    Read = false
                },
                new Notification
                {
                    Sender = context.Users.FirstOrDefault(),
                    Receiver = context.Users.Single(u => u.Email == "company@test.com"),
                    ApplicationID = 2,
                    Read = false
                },
                new Notification
                {
                    Sender = context.Users.FirstOrDefault(),
                    Receiver = context.Users.Single(u => u.Email == "company@test.com"),
                    ApplicationID = 2,
                    Read = false
                },
                new Notification
                {
                    Sender = context.Users.FirstOrDefault(),
                    Receiver = context.Users.Single(u => u.Email == "company@test.com"),
                    ApplicationID = 2,
                    Read = false
                },
                new Notification
                {
                    Sender = context.Users.FirstOrDefault(),
                    Receiver = context.Users.Single(u => u.Email == "company@test.com"),
                    ApplicationID = 2,
                    Read = false
                },
                new Notification
                {
                    Sender = context.Users.Single(u => u.Email == "company@test.com"),
                    Receiver = context.Users.FirstOrDefault(),
                    ApplicationID = 1,
                    Read = false
                },
                new Notification
                {
                    Sender = context.Users.Single(u => u.Email == "company@test.com"),
                    Receiver = context.Users.FirstOrDefault(),
                    ReviewID = 1,
                    Read = false
                },
                new Notification
                {
                    Sender = context.Users.Single(u => u.Email == "company@test.com"),
                    Receiver = context.Users.FirstOrDefault(),
                    AssignmentID = 3,
                    Read = false
                }
                );
            context.SaveChanges();

            context.AssignmentTags.AddRange(
            new AssignmentTag
            {
                Assignment = context.Assignments.FirstOrDefault(),
                Tag = context.Tags.Single(r => r.Name == "HTML")
            },
            new AssignmentTag
            {
                Assignment = context.Assignments.FirstOrDefault(),
                Tag = context.Tags.Single(r => r.Name == "CSS")
            },
            new AssignmentTag
            {
                Assignment = context.Assignments.FirstOrDefault(),
                Tag = context.Tags.Single(r => r.Name == "Design")
            },
            new AssignmentTag
            {
                Assignment = context.Assignments.SingleOrDefault(a => a.AssignmentID == 2),
                Tag = context.Tags.Single(r => r.Name == "Sql")
            },
            new AssignmentTag
            {
                Assignment = context.Assignments.SingleOrDefault(a => a.AssignmentID == 2),
                Tag = context.Tags.Single(r => r.Name == "Datamodeling")
            },
            new AssignmentTag
            {
                Assignment = context.Assignments.SingleOrDefault(a => a.AssignmentID == 2),
                Tag = context.Tags.Single(r => r.Name == "Back-end")
            },
            new AssignmentTag
            {
                Assignment = context.Assignments.SingleOrDefault(a => a.AssignmentID == 3),
                Tag = context.Tags.Single(r => r.Name == ".NET")
            },
            new AssignmentTag
            {
                Assignment = context.Assignments.SingleOrDefault(a => a.AssignmentID == 4),
                Tag = context.Tags.Single(r => r.Name == "CSS")
            },
            new AssignmentTag
            {
                Assignment = context.Assignments.SingleOrDefault(a => a.AssignmentID == 4),
                Tag = context.Tags.Single(r => r.Name == "Design")
            },
            new AssignmentTag
            {
                Assignment = context.Assignments.SingleOrDefault(a => a.AssignmentID == 5),
                Tag = context.Tags.Single(r => r.Name == "Design")
            },
            new AssignmentTag
            {
                Assignment = context.Assignments.SingleOrDefault(a => a.AssignmentID == 5),
                Tag = context.Tags.Single(r => r.Name == "CSS")
            }
            );
            context.SaveChanges();
        }
    }
}
