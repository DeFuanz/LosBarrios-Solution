using Microsoft.EntityFrameworkCore;
using LosBarriosEvents.Data;


namespace LosBarriosEvents.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {

            }
        }
    }
}