// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using LosBarriosEvents.Areas.Identity.Data;
using LosBarriosEvents.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LosBarriosEvents.Data;
using Microsoft.EntityFrameworkCore;

namespace LosBarriosEvents.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<LosBarriosUser> _userManager;
        private readonly SignInManager<LosBarriosUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public IndexModel(
            UserManager<LosBarriosUser> userManager,
            SignInManager<LosBarriosUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Full name")]
            public string Name { get; set; }

            [Required]
            [Display(Name = "Birth Date")]
            [DataType(DataType.Date)]
            public DateTime DOB { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "Tell us about yourself!")]
            public string BIO { get; set; }
        }

        public static IEnumerable<SelectListItem> UserRoleOptions()
        {
            return new[]
            {
            new SelectListItem {Text = "Student", Value = UserRoles.LosBarriosEventsStudentRole},
            new SelectListItem {Text = "Volunteer", Value = UserRoles.LosBarriosEventsVolunteerRole},
            new SelectListItem {Text = "Speaker", Value = UserRoles.LosBarriosEventsSpeakerRole},
            new SelectListItem {Text = "Administrator", Value = UserRoles.LosBarriosEventsAdministratorRole}
            };
        }

        private async Task LoadAsync(LosBarriosUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Name = user.Name,
                DOB = user.DOB,
                BIO = user.BIO,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                if (User.IsInRole(UserRoles.LosBarriosEventsSpeakerRole))
                {
                    var currentSpeaker = _context.Speakers.Where(s => s.SpeakerId == _userManager.GetUserId(User)).First();
                    currentSpeaker.phoneNumber = Input.PhoneNumber;

                    _context.Speakers.Attach(currentSpeaker).State = EntityState.Modified;

                    _context.SaveChanges();
                }


                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (Input.Name != user.Name)
            {
                user.Name = Input.Name;

                if (User.IsInRole(UserRoles.LosBarriosEventsSpeakerRole))
                {
                    var currentSpeaker = _context.Speakers.Where(s => s.SpeakerId == _userManager.GetUserId(User)).First();
                    currentSpeaker.Name = Input.Name;

                    _context.Speakers.Attach(currentSpeaker).State = EntityState.Modified;

                    _context.SaveChanges();
                }


            }

            if (Input.DOB != user.DOB)
            {
                user.DOB = Input.DOB;
            }
            if (Input.BIO != user.BIO)
            {
                user.BIO = Input.BIO;

                if (User.IsInRole(UserRoles.LosBarriosEventsSpeakerRole))
                {
                    var currentSpeaker = _context.Speakers.Where(s => s.SpeakerId == _userManager.GetUserId(User)).First();
                    currentSpeaker.BIO = Input.BIO;

                    _context.Speakers.Attach(currentSpeaker).State = EntityState.Modified;

                    _context.SaveChanges();
                }
            }

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
