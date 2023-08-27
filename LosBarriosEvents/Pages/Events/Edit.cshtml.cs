using LosBarriosEvents.Authorization;
using LosBarriosEvents.Data;
using LosBarriosEvents.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LosBarriosEvents.Pages;

[Authorize(Roles = "Administrators")]
public class EventEditModel : PageModel
{
    private readonly ILogger<EventEditModel> _logger;
    private readonly ApplicationDbContext _context;

    public EventEditModel(ILogger<EventEditModel> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [BindProperty]
    public Event Event {get; set;}

    public IActionResult OnGet(int? id)
    {
        Event? evnt = _context.Events.FirstOrDefault(e => e.EventId == id);

        if (evnt == null)
        {
            return NotFound();
        }

        Event = evnt;

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

        _context.Attach(Event).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}