using LosBarriosEvents.Authorization;
using LosBarriosEvents.Data;
using LosBarriosEvents.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LosBarriosEvents.Pages;

[Authorize(Roles = "Administrators,Speakers")]
public class SessionCreateModel : PageModel
{
    private readonly ILogger<SessionCreateModel> _logger;
    private readonly ApplicationDbContext _context;

    public SessionCreateModel(ILogger<SessionCreateModel> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [BindProperty]
    public Event Event { get; set; }

    [BindProperty]
    public Session Session { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        Event = await _context.Events.Where(e => e.EventId == id).FirstAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        Event = await _context.Events.Where(e => e.EventId == id).FirstAsync();

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var e in errors)
            {
                _logger.LogWarning(e.ErrorMessage);
            }
            return Page();
        }

        var isAuthorized = User.IsInRole(UserRoles.LosBarriosEventsAdministratorRole);

        if (!isAuthorized)
        {
            return Forbid();
        }

        await _context.Sessions.AddAsync(Session);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index", new {id = Session.EventId});
    }
}