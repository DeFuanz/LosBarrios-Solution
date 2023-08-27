using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LosBarriosEvents.Areas.Identity.Data;

// Add profile data for application users by adding properties to the LosBarriosUser class
public class LosBarriosUser : IdentityUser
{
    [PersonalData]
    public string? Name { get; set; }
    [PersonalData]
    public DateTime DOB { get; set; }
    [PersonalData]
    public string? BIO { get; set; }

    [PersonalData]
    public string SelectedUserRole { get; set; } = default!;
}

