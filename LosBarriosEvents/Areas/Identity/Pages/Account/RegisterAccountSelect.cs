using System.ComponentModel.DataAnnotations;
using LosBarriosEvents.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace LosBarriosEvents.Areas.Identity.Pages.Account
{
    public class RegisterAccountSelectModel : PageModel
    {

        public string ReturnUrl { get; set; }

        [Required]
        [BindProperty]
        public string SelectedUserRole { get; set; } = default!;

        //Dropdown options linked to user role types
        public static IEnumerable<SelectListItem> UserRoleOptions()
        {
            return new[]
            {
            new SelectListItem {Text = "Student", Value = UserRoles.LosBarriosEventsStudentRole},
            new SelectListItem {Text = "Volunteer", Value = UserRoles.LosBarriosEventsVolunteerRole},
            new SelectListItem {Text = "Admin", Value = UserRoles.LosBarriosEventsAdministratorRole},
            new SelectListItem {Text = "Speaker", Value = UserRoles.LosBarriosEventsSpeakerRole}
            };
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                return RedirectToPage($"./Register{SelectedUserRole}");
            }
            
            return Page();
        }
    }
}