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

[Authorize(Roles = "Administrators")]
public class RequestInbox : PageModel
{
    private readonly ILogger<RequestInbox> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<LosBarriosUser> _userManager;

    public RequestInbox(ILogger<RequestInbox> logger, ApplicationDbContext context, UserManager<LosBarriosUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    [BindProperty]
    public List<SpeakerForm> PendingForms { get; set; }
    [BindProperty]
    public Session Session { get; set; }

    public IActionResult OnGet(int? id)
    {
        Session = _context.Sessions.Include(s => s.SessionSpeakers)!.ThenInclude(n => n.Speaker).FirstOrDefault(i => i.SessionId == id)!;

        PendingForms = _context.SpeakerForms.Include(s => s.Speaker).Where(f => f.Status == FormStatus.Pending).ToList();

        return Page();
    }

    public IActionResult OnPost(string id, int sessId, int formId, string assignType)
    {
        _logger.LogWarning("Posting Page");

        var isAuthorized = User.IsInRole(UserRoles.LosBarriosEventsAdministratorRole);

        if (!isAuthorized)
        {
            return Forbid();
        }

        switch (assignType)
        {
            case "assign":
                AssignSpeaker(sessId, id, formId);
                break;
            case "unassign":
            UnAssignSpeaker(sessId, id, formId);
                break;
        }

        //Recall this page to update any changes after post
        return RedirectToPage("./RequestInbox", new {id = sessId});
    }

    public bool CheckSpeakerAssignment(string speakerId, int sessId)
    {   
        var assigned = _context.sessionSpeakers.Find(sessId, speakerId) != null;

        return assigned;
    }

    public void AssignSpeaker(int sessId, string id, int formId)
    {
        SessionSpeaker sessionSpeaker = new SessionSpeaker();

        sessionSpeaker.SessionId = sessId;
        sessionSpeaker.SpeakerId = id;
        sessionSpeaker.FormIdAssigned = formId;

        _context.sessionSpeakers.Add(sessionSpeaker);

        var speakerFormToUpdate = _context.SpeakerForms.First(f => f.SpeakerFormId == formId);

        speakerFormToUpdate.Status = FormStatus.Approved;

        _context.SpeakerForms.Attach(speakerFormToUpdate).State = EntityState.Modified;

        _context.SaveChanges();
    }

    public void UnAssignSpeaker(int sessId, string id, int formId)
    {
        var sessionSpeakerToRemove = _context.sessionSpeakers.Find(sessId,id);

        _context.sessionSpeakers.Remove(sessionSpeakerToRemove);

        var speakerFormToUpdate = _context.SpeakerForms.First(f => f.SpeakerFormId == formId);

        speakerFormToUpdate.Status = FormStatus.Pending;

        _context.SpeakerForms.Attach(speakerFormToUpdate).State = EntityState.Modified;

        _context.SaveChanges();
    }
}