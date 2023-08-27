using LosBarriosEvents.Authorization;
using LosBarriosEvents.Data;
using LosBarriosEvents.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using LosBarriosEvents.Areas.Identity.Data;

namespace LosBarriosEvents.Pages
{
    [Authorize(Roles = "Students")]
    public class FeedbackCreateModel : PageModel
    {
        private readonly ILogger<FeedbackCreateModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<LosBarriosUser> _userManager;

        public FeedbackCreateModel(
            ILogger<FeedbackCreateModel> logger,
            ApplicationDbContext context, UserManager<LosBarriosUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Feedback Feedback { get; set; }
        [BindProperty]
        public Session Session { get; set; }

        public IActionResult OnGet(int id)
        {
            Session = _context.Sessions.First(s => s.SessionId == id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = _userManager.GetUserId(User);

            var feedback = new Feedback
            {
                SessionId = Feedback.SessionId,
                Session = Session,
                OverallLectureRating = Feedback.OverallLectureRating,
                SpeakerRating = Feedback.SpeakerRating,
                ContentRating = Feedback.ContentRating,
                FacilityRating = Feedback.FacilityRating,
                Comment = Feedback.Comment,
                CreatedOn = DateTime.UtcNow.ToString()
            };

            _context.Feedback.Add(Feedback);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
