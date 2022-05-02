// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using MediaHub.Data.PersistencyLayer;
using MediaHub.Data.ProfileModule.Model;

namespace MediaHub.Areas.Identity.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ConfirmEmailModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }
        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                try
                {
                    CreateUserProfile(userId);
                    StatusMessage = "Thank you for confirming your email.";
                }
                catch (Exception)
                {
                    StatusMessage = "Error creating user profile";
                }
            }
            else
            {
                StatusMessage = "Error confirming your email.";
            }
            return Page();
        }

        private void CreateUserProfile(string userId)
        {
            using MediaHubDBContext context = new();

            if (context.UserProfiles.Any(up => up.UserId == userId))
            {
                return;
            }

            var userProfile = new UserProfile(userId);
            context.Add(userProfile);
            context.SaveChanges();
        }
    }
}
