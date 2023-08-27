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

[Authorize(Roles = "Administrator,Speakers")]
public class SpeakerFormCreateModel : PageModel
{
    private readonly ILogger<SpeakerFormCreateModel> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<LosBarriosUser> _userManager;

    public SpeakerFormCreateModel(ILogger<SpeakerFormCreateModel> logger, ApplicationDbContext context, UserManager<LosBarriosUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    [BindProperty]
    public SpeakerForm Form { get; set; }
    [BindProperty]
    public List<Equipment> Equipment { get; set; }
    [BindProperty]
    public List<int> SelectedEquipmentIds { get; set; }
    public Speaker CurrentSpeaker { get; set; }

    public IActionResult OnGet()
    {
        //Query Speaker Model to bind to new form foregn keys on post.
        var userId = _userManager.GetUserId(User);
        CurrentSpeaker = _context.Speakers.Where(s => s.SpeakerId == userId).First();
        Equipment = _context.Equipment.ToList();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        //Recall getSpeakerQuery incase validation posts page.
        var userId = _userManager.GetUserId(User);
        CurrentSpeaker = _context.Speakers.Where(s => s.SpeakerId == userId).First();
        Equipment = _context.Equipment.ToList();

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var e in errors)
            {
                _logger.LogWarning(e.ErrorMessage);
            }
            return Page();
        }

        if (!User.IsInRole(UserRoles.LosBarriosEventsSpeakerRole))
        {
            return Forbid();
        }

        //Grab ids of equipment selected and add to speakerformequipment join table
        foreach (int id in SelectedEquipmentIds)
        {
            SpeakerFormEquipment equipment = new SpeakerFormEquipment {
                SpeakerFormId = Form.SpeakerFormId,
                SpeakerForm = Form,
                EquipmentId = id,
                Equipment = Equipment.First(i => i.EquipmentId == id)
            };
            _context.SpeakerFormEquipment.Add(equipment);
        }

        await _context.SpeakerForms.AddAsync(Form);

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}