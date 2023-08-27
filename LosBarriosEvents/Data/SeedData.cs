using LosBarriosEvents.Models;
using LosBarriosEvents.Data;
using Microsoft.EntityFrameworkCore;

// dotnet aspnet-codegenerator razorpage -m Contact -dc ApplicationDbContext -udl -outDir Pages\Contacts --referenceScriptLibraries

namespace LosBarriosEvents.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                SeedDB(context);
            }
        }

        public static void SeedDB(ApplicationDbContext context)
        {
            if (context.Equipment.Any())
            {
                return;
            }

            context.Equipment.AddRange(
                new Equipment {
                    Name = "Computer"
                },
                new Equipment {
                    Name = "Projector"
                }
            );

            context.SaveChanges();
        }

    }
}