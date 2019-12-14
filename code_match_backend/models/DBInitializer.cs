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
                RoleID = 1,
                PermissionID = 1
            },
            new RolePermission
            {
                RoleID = 1,
                PermissionID = 3
            },
            new RolePermission
            {
                RoleID = 2,
                PermissionID = 2
            },
            new RolePermission
            {
                RoleID = 2,
                PermissionID = 4
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
            },
            new Company
            {
                StreetAdress = "Gasthuisstraat 102",
                PostalCode = 2300,
                Name = "MusicMaestro"
            }
            );


            context.SaveChanges();

            context.Makers.AddRange(
            new Maker
            {
                Dob = new DateTime(1998, 3, 12),
                Experience = "none",
                Firstname = "Brecht",
                Lastname = "Snoeck",
                LinkedIn = "www.linkedIn/BrechtSnoeck",
                Nickname = "Brekke"
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
            },
            new User
            {
                Biography = "Mensen gepasioneerd door muziek!",
                Email = "maestro@live.com",
                Password = "muziek",
                Phonenumber = "014856478",
                Role = context.Roles.SingleOrDefault(r => r.RoleID == 2),
                CompanyID = 3
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
            },
            new Assignment
            {
                Status = "InProgress",
                StreetAdress = "Gasthuisstraat 102",
                PostalCode = 2300,
                Company = context.Companies.SingleOrDefault(x => x.CompanyID == 3),
                Description = "Hey, wij binnen MusicMaestro zoeken een gedreven persoon met IT-kennis die het ziet zitten om een website voor ons te bouwen." +
                "Hierbij staat een goede vergoeding vast!",
                Name = "Website voor een muziekwinkel"
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
                Assignment = context.Assignments.Where(r => r.Name == "Het bakkerij datamodel").Single(),
                Maker = context.Makers.SingleOrDefault(m => m.MakerID == 2)
            },
            new Application
            {
                IsAccepted = true,
                Assignment = context.Assignments.Where(r => r.Name == "De online bakkerij database").Single(),
                Maker = context.Makers.FirstOrDefault()
            },
            new Application
            {
                IsAccepted = true,
                Assignment = context.Assignments.Where(r => r.Name == "Website voor een muziekwinkel").Single(),
                Maker = context.Makers.SingleOrDefault(r => r.MakerID == 2)
            }
            );

            context.SaveChanges();

            context.Reviews.AddRange(new Review
            {
                Assignment = context.Assignments.FirstOrDefault(),
                Description = "very good",
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
                MakerID = 1,
                TagID = 1
            },
            new MakerTag
            {
                MakerID = 2,
                TagID = 12
            },
            new MakerTag
            {
                MakerID = 2,
                TagID = 13
            },
            new MakerTag
            {
                MakerID = 2,
                TagID = 6
            },
            new MakerTag
            {
                MakerID = 2,
                TagID = 5
            }
            );

            context.SaveChanges();

            context.CompanyTags.AddRange(new CompanyTag
            {

                CompanyID = 1,
                TagID = 2
            }
            );
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
                    ApplicationID = 1,
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
                AssignmentID = 1,
                TagID = 12
            },
            new AssignmentTag
            {
                AssignmentID = 1,
                TagID = 13
            },
            new AssignmentTag
            {
                AssignmentID = 1,
                TagID = 6
            },
            new AssignmentTag
            {
                AssignmentID = 2,
                TagID = 14
            },
            new AssignmentTag
            {


                AssignmentID = 2,
                TagID = 7
            },
            new AssignmentTag
            {


                AssignmentID = 2,
                TagID = 2
            },
            new AssignmentTag
            {


                AssignmentID = 3,
                TagID = 15
            },
            new AssignmentTag
            {


                AssignmentID = 4,
                TagID = 13
            },
            new AssignmentTag
            {


                AssignmentID = 4,
                TagID = 6
            },
            new AssignmentTag
            {


                AssignmentID = 5,
                TagID = 6
            },
            new AssignmentTag
            {


                AssignmentID = 5,
                TagID = 13
            },
            new AssignmentTag
            {


                AssignmentID = 6,
                TagID = 13
            },
            new AssignmentTag
            {


                AssignmentID = 6,
                TagID = 6
            },
            new AssignmentTag
            {


                AssignmentID = 6,
                TagID = 12
            },
            new AssignmentTag
            {

                AssignmentID = 6,
                TagID = 5
            }
            );
            context.SaveChanges();
        }
    }
}
