using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LosBarriosEvents.Pages;

public class VolunteerModel : PageModel
{
    private readonly ILogger<VolunteerModel> _logger;

    public VolunteerModel(ILogger<VolunteerModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
