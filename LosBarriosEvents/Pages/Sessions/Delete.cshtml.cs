using LosBarriosEvents.Authorization;
using LosBarriosEvents.Data;
using LosBarriosEvents.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LosBarriosEvents.Pages;

[Authorize(Roles = "Administrators")]
public class SessionDeleteModel : PageModel
{
    private readonly ILogger<SessionDeleteModel> _logger;
    private readonly ApplicationDbContext _context;

    public SessionDeleteModel(ILogger<SessionDeleteModel> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [BindProperty]
    public Session Session { get; set; }

    public async Task<IActionResult> OnGetAsync(int? eventid, int? Sessionid)
    {
        Event? selectedEvent = await _context.Events.Where(e => e.EventId == eventid).Include(l => l.Sessions).FirstOrDefaultAsync();

        Session = await _context.Sessions.Where(s => s.SessionId == Sessionid).FirstOrDefaultAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var isAuthorized = User.IsInRole(UserRoles.LosBarriosEventsAdministratorRole);

        if (!isAuthorized)
        {
            return Forbid();
        }

        _context.Sessions.Remove(Session);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index", new {id = Session.EventId});
    }
}