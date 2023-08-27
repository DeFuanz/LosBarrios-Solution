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

[Authorize(Roles = "Speakers,Administrator")]

public class SpeakerFormDeleteModel : PageModel
{
    private readonly ILogger<SpeakerFormDeleteModel> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<LosBarriosUser> _userManager;

    public SpeakerFormDeleteModel(ILogger<SpeakerFormDeleteModel> logger, ApplicationDbContext context, UserManager<LosBarriosUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    [BindProperty]
    public SpeakerForm Form { get; set; }

    public IActionResult OnGet(int? id)
    {
        SpeakerForm frm = _context.SpeakerForms.FirstOrDefault(f => f.SpeakerFormId == id)!;

        if (frm == null)
        {
            return NotFound();
        }

        Form = frm;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var e in errors)
            {
                _logger.LogWarning(e.ErrorMessage);
            }
            return Page();
        }

        var isAuthorized = User.IsInRole(UserRoles.LosBarriosEventsSpeakerRole);

        if (!isAuthorized)
        {
            return Forbid();
        }

        _context.SpeakerForms.Remove(Form);

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

}