using LosBarriosEvents.Authorization;
using LosBarriosEvents.Data;
using LosBarriosEvents.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LosBarriosEvents.Pages;

[Authorize(Roles = "Students,Speakers,Administrators")]
public class SessionDetailsModel : PageModel
{
    private readonly ILogger<SessionDetailsModel> _logger;
    private readonly ApplicationDbContext _context;

    public SessionDetailsModel(ILogger<SessionDetailsModel> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;

    }

    public Session? Session { get; set; }

    public IActionResult OnGet(int id)
    {
        _logger.LogInformation($"Getting event details for event with ID {id}");

        Session = _context.Sessions.Include(s => s.SessionSpeakers)!.ThenInclude(n => n.Speaker).FirstOrDefault(i => i.SessionId == id);

        if (Session == null)
        {
            return NotFound();
        }

        return Page();
    }


}