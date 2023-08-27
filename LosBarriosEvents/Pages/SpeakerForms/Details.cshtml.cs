

using LosBarriosEvents.Data;
using LosBarriosEvents.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LosBarriosEvents.Pages;

[Authorize(Roles = "Speakers,Administrator")]

public class SpeakerFormDetailsModel : PageModel
{
    private readonly ILogger<SpeakerFormDetailsModel> _logger;
    private readonly ApplicationDbContext _context;

    public SpeakerFormDetailsModel(ILogger<SpeakerFormDetailsModel> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public SpeakerForm Form { get; set; }

    public IActionResult OnGet(int? id)
    {
        Form = _context.SpeakerForms.Include(f => f.SpeakerFormEquipment).ThenInclude(e => e.Equipment).First(s => s.SpeakerFormId == id);

        if (Form == null)
        {
            return NotFound();
        }
        
        return Page();
    }
}