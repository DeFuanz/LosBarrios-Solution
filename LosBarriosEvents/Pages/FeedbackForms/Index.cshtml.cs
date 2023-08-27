using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LosBarriosEvents.Data;
using LosBarriosEvents.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LosBarriosEvents.Pages
{
    public class FeedbackIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public FeedbackIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Feedback> Feedback { get;set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Feedback = await _context.Feedback
                .Include(l => l).ToListAsync();

            return Page();
        }
    }
}