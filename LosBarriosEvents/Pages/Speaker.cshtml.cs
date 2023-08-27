using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LosBarriosEvents.Pages;

public class SpeakerModel : PageModel
{
    private readonly ILogger<SpeakerModel> _logger;

    public SpeakerModel(ILogger<SpeakerModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
