using LosBarriosEvents.Areas.Identity.Data;
using LosBarriosEvents.Data;
using LosBarriosEvents.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LosBarriosEvents.Pages
{
    [Authorize(Roles = "Administrators,Students,Volunteers,Speakers")]
    public class SpeakerProfilesIndexModel : PageModel
    {
        private readonly ILogger<SpeakerProfilesIndexModel> _logger;
        private readonly ApplicationDbContext _context;
        

        public SpeakerProfilesIndexModel(ILogger<SpeakerProfilesIndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public Speaker Speaker { get; set; }

        public IActionResult OnGet(string? id)
        {
            Speaker = _context.Speakers.Where(s => s.SpeakerId == id).First();

            return Page();
        }
    }

}