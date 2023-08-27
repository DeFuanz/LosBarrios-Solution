using LosBarriosEvents.Authorization;
using LosBarriosEvents.Data;
using LosBarriosEvents.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LosBarriosEvents.Pages;

[Authorize(Roles = "Administrator")]
public class EventCreateModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ApplicationDbContext _context;

    public EventCreateModel(ILogger<IndexModel> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [BindProperty]
    public Event Event {get; set;}

    public IActionResult OnGet()
    {
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

        _context.Events.Add(Event);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}