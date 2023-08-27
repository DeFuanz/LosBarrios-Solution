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

[Authorize(Roles = "Administrators,Students,Volunteers,Speakers")]
public class EventIndexModel : PageModel
{
    private readonly ILogger<EventIndexModel> _logger;
    private readonly ApplicationDbContext _context;
    public readonly IAuthorizationService authorizationService;
    private readonly UserManager<LosBarriosUser> _userManager;

    public EventIndexModel(ILogger<EventIndexModel> logger, ApplicationDbContext context, UserManager<LosBarriosUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    public IList<Event> Events { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        _logger.LogWarning("Getting page");

        Events = await _context.Events.ToListAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        _logger.LogWarning("Posting Page");

        var isAuthorized = User.IsInRole(UserRoles.LosBarriosEventsStudentRole);

        if (!isAuthorized)
        {
            return Forbid();
        }

        await RegisterStudentsAsync(id);

        //Recall this page to update any changes after post
        return RedirectToPage("./Index");
    }

    //Registers student in eventstudents table/Unregisters if already in table
    public async Task RegisterStudentsAsync(int? id)
    {
        var userId = _userManager.GetUserId(User);

        _logger.LogWarning(userId!.ToString());

        EventStudent eventStudent = new EventStudent();

        eventStudent.StudentId = userId;
        eventStudent.EventId = (int)id!;

        if (CheckRegistrationStatus(id))
        {
            //Remove tracking from entites to ensure ability to delete
            foreach (var entity in _context.ChangeTracker.Entries())
            {
                entity.State = EntityState.Detached;
            }
            
            _context.EventStudents.Remove(eventStudent);
            _context.SaveChanges();
            return;
        }

        await _context.EventStudents.AddAsync(eventStudent);

        await _context.SaveChangesAsync();
    }

    //Checks if user is registered and returns true if they are
    public bool CheckRegistrationStatus(int? eventId)
    {
        var userId = _userManager.GetUserId(User);

        bool registered = (_context.EventStudents.Find(eventId, userId) != null);

        return registered;
    }
}