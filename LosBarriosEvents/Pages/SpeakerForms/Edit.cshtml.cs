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
using System.Linq;

namespace LosBarriosEvents.Pages;

[Authorize(Roles = "Administrators,Speakers")]

public class SpeakerFormsEditModel : PageModel
{

    private readonly ILogger<SpeakerFormsEditModel> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<LosBarriosUser> _userManager;


    public SpeakerFormsEditModel(ILogger<SpeakerFormsEditModel> logger, ApplicationDbContext context, UserManager<LosBarriosUser> userManager)
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
    public List<SpeakerFormEquipment> SelectedEquipment { get; set; }
    [BindProperty]
    public List<int> SelectedEquipmentIds { get; set; }
    public string? KeywordString { get; set; }

    public IActionResult OnGet(int? id)
    {
        Form = _context.SpeakerForms.Include(e => e.SpeakerFormEquipment!).ThenInclude(e => e.Equipment).FirstOrDefault(f => f.SpeakerFormId == id)!;

        Equipment = _context.Equipment.ToList();

        KeywordString = String.Join(',', Form.Keywords!);

        SelectedEquipment = _context.SpeakerFormEquipment.Include(e => e.Equipment).Where(s => s.SpeakerFormId == id).ToList();

        _logger.LogWarning(SelectedEquipment.Count.ToString());

        if (Form == null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {

        Equipment = _context.Equipment.ToList();

        KeywordString = String.Join(',', Form.Keywords!);

        SelectedEquipment = _context.SpeakerFormEquipment.Include(e => e.Equipment).Where(s => s.SpeakerFormId == Form.SpeakerFormId).ToList();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var isAuthorized = User.IsInRole(UserRoles.LosBarriosEventsSpeakerRole) || User.IsInRole(UserRoles.LosBarriosEventsAdministratorRole);

        if (!isAuthorized)
        {
            return Forbid();
        }

        _logger.LogWarning(SelectedEquipmentIds.Count.ToString());
        foreach (var eq in Equipment)
        {
            if (!SelectedEquipmentIds.Contains(eq.EquipmentId))
            {
                try
                {
                    var equipmentToRemove = _context.SpeakerFormEquipment.Where(f => f.SpeakerFormId == Form.SpeakerFormId).First(e => e.EquipmentId == eq.EquipmentId);
                    _context.SpeakerFormEquipment.Remove(equipmentToRemove);
                }
                catch
                {
                    continue;
                }
            }

            if (!SelectedEquipment.Where(f => f.SpeakerFormId == Form.SpeakerFormId).Any(e => e.EquipmentId == eq.EquipmentId))
            {
                try
                {
                    SpeakerFormEquipment equipmentToAdd = new SpeakerFormEquipment
                    {
                        SpeakerFormId = Form.SpeakerFormId,
                        SpeakerForm = Form,
                        EquipmentId = eq.EquipmentId,
                        Equipment = Equipment.First(i => i.EquipmentId == eq.EquipmentId)
                    };

                    _context.SpeakerFormEquipment.Add(equipmentToAdd);
                }
                catch
                {
                    continue;
                }
            }
        }


        _context.SpeakerForms.Attach(Form).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}