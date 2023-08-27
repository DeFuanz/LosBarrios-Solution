using System.Security.Claims;
using LosBarriosEvents.Areas.Identity.Data;
using LosBarriosEvents.Authorization;
using LosBarriosEvents.Data;
using LosBarriosEvents.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace LosBarriosEvents.Pages;

[Authorize(Roles = "Administrators,Students,Speakers")]

public class SessionIndexModel : PageModel
{
    private readonly ILogger<SessionIndexModel> _logger;
    private readonly ApplicationDbContext _context;

    public SessionIndexModel(ILogger<SessionIndexModel> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IList<Session> Sessions { get; set; }

    [BindProperty]
    public int eventId { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        eventId = (int)id!;

        var eventSessions = await _context.Sessions.Where(l => l.EventId == id).ToListAsync();

        Sessions = eventSessions;

        return Page();
    }
}
