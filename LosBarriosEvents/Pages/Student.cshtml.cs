using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LosBarriosEvents.Pages;

public class StudentModel : PageModel
{
    private readonly ILogger<StudentModel> _logger;

    public StudentModel(ILogger<StudentModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }
}
