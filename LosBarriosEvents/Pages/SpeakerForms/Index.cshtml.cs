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

[Authorize(Roles = "Speakers")]
public class SpeakerFormIndexModel : PageModel
{
    private readonly ILogger<SpeakerFormIndexModel> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<LosBarriosUser> _userManager;

    public SpeakerFormIndexModel(ILogger<SpeakerFormIndexModel> logger, ApplicationDbContext context, UserManager<LosBarriosUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    public List<SpeakerForm> SpeakerForms { get; set; }
    public Speaker Speaker { get; set; }

    public IActionResult OnGet()
    {
        var userId = _userManager.GetUserId(User);
        Speaker = _context.Speakers.Where(s => s.SpeakerId == userId).First();

        SpeakerForms = _context.SpeakerForms.Where(f => f.SpeakerId == Speaker.SpeakerId).ToList();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!User.IsInRole(UserRoles.LosBarriosEventsSpeakerRole))
        {
            return Forbid();
        }

        return Page();
    }
}