using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using subscribers.Models;

namespace subscribers.Data
{
    // A class used to seed the database with
    // some initial data in case the table is empty. 
    public class ApiDBSeeder
    {
        public static void InsertSeed (IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApiDBContext>();

                // If database is empty, seed it with initial data. 
                if (!context.Subscribers.Any())
                {
                    context.Subscribers.AddRange(
                        new Subscriber {
                            PersonalId = 197001019999,
                            Firstname = "Jane",
                            Lastname = "Doe",
                            DistributionAddress = "Main Street 1",
                            ZipCode = "90101",
                            City = "Townsville",
                            PhoneNumber = "0701234567"
                        },
                        new Subscriber {
                            PersonalId = 198012311234,
                            Firstname = "John",
                            Lastname = "Smith",
                            DistributionAddress = "Northwest Boulevard 100",
                            ZipCode = "99922",
                            City = "Atlantis",
                            PhoneNumber = "0739876543"
                        },
                        new Subscriber {
                            PersonalId = 199004045656,
                            Firstname = "Maria",
                            Lastname = "Andersson",
                            DistributionAddress = "Storgatan 42",
                            ZipCode = "70070",
                            City = "Sveaköping",
                            PhoneNumber = "0761199228"
                        },
                        new Subscriber {
                            PersonalId = 199707147178,
                            Firstname = "Johnny",
                            Lastname = "Bravo",
                            DistributionAddress = "Presley Street 5",
                            ZipCode = "45045",
                            City = "Aron City",
                            PhoneNumber = "0701122334"
                        }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}