using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using LosBarriosEvents.Areas.Identity.Data;
using LosBarriosEvents.Authorization;
using LosBarriosEvents.Data;
using LosBarriosEvents.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace LosBarriosEvents.Areas.Identity.Pages.Account
{
    public class RegisterStudentModel : PageModel
    {

        private readonly SignInManager<LosBarriosUser> _signInManager;
        private readonly UserManager<LosBarriosUser> _userManager;
        private readonly IUserStore<LosBarriosUser> _userStore;
        private readonly IUserEmailStore<LosBarriosUser> _emailStore;
        private readonly ILogger<RegisterStudentModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleStore;
        private readonly ApplicationDbContext _context;

        public RegisterStudentModel(
            UserManager<LosBarriosUser> userManager,
            IUserStore<LosBarriosUser> userStore,
            SignInManager<LosBarriosUser> signInManager,
            ILogger<RegisterStudentModel> logger,
            RoleManager<IdentityRole> roleStore,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _userStore = userStore;
            _roleStore = roleStore;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public required InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        [Required]
        [BindProperty]
        [PageRemote(ErrorMessage = "Username already exists", AdditionalFields = "__RequestVerificationToken",
        HttpMethod = "post", PageHandler = "IsUsernameAvailable")]
        [Display(Name = "UserName")]
        public required string UserName { get; set; }

        public class InputModel
        {


            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "First name")]
            public required string FirstName { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Last name")]
            public required string LastName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public required string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public required string ConfirmPassword { get; set; }

            public required string SchoolName { get; set; }
        }

        public List<School> Schools { get; set; }

        public void OnGet()
        {
            Schools = _context.Schools.ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                bool UserNameTaken = _userManager.Users.Any(u => u.UserName == UserName);

                if (UserNameTaken)
                {
                    return Page();
                }

                _logger.LogWarning("Model is valid");
                var user = CreateUser();

                user.UserName = UserName;
                user.Name = Input.FirstName + " " + Input.LastName;
                user.SelectedUserRole = UserRoles.LosBarriosEventsStudentRole;

                await _userStore.SetUserNameAsync(user, UserName, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    await EnsureRole(_roleStore, _userManager, _context, userId, UserRoles.LosBarriosEventsStudentRole);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToPage("/Index");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            _logger.LogWarning("Model is NOT valid");

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private LosBarriosUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<LosBarriosUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(LosBarriosUser)}'. " +
                    $"Ensure that '{nameof(LosBarriosUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        //verify user can and is assigned a role
        private static async Task<IdentityResult> EnsureRole(RoleManager<IdentityRole> roleStore, UserManager<LosBarriosUser> usersManager, ApplicationDbContext context, string uid, string role)
        {
            var roleManager = roleStore;

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = usersManager;

            //if (userManager == null)
            //{
            //    throw new Exception("userManager is null");
            //}

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);


            switch (role)
            {
                case "Students":
                    Student student = new Student();

                    student.StudentId = uid;

                    await context.AddAsync(student);

                    await context.SaveChangesAsync();
                    break;
                case "Speakers":
                    Speaker speaker = new Speaker();

                    speaker.SpeakerId = uid;

                    await context.AddAsync(speaker);

                    await context.SaveChangesAsync();
                    break;
            }

            return IR;
        }

        public JsonResult OnPostIsUsernameAvailable()
        {
            _logger.LogWarning("Request sent");
            var isAvailable = !_context.Users.Where(u => u.UserName.ToLower() == UserName.ToLower()).Any();
            return new JsonResult(isAvailable);
        }
    }
}