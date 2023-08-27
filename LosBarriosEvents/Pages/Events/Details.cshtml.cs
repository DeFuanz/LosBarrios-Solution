using LosBarriosEvents.Authorization;
using LosBarriosEvents.Data;
using LosBarriosEvents.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LosBarriosEvents.Pages;

[Authorize(Roles = "Administrators")]
public class EventDetailsModel : PageModel
{
    private readonly ILogger<EventDetailsModel> _logger;
    private readonly ApplicationDbContext _context;

    public EventDetailsModel(ILogger<EventDetailsModel> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;

    }

    public Event Event { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
        {
            _logger.LogInformation($"Getting event details for event with ID {id}");

            Event = await _context.Events.FindAsync(id);

            if (Event == null)
            {
                _logger.LogWarning($"Event with ID {id} not found");
                return NotFound();
            }

            return Page();
        }
    
    
}