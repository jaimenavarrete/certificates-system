// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using CertificatesSystem.Models.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CertificatesSystem.WebUI.Areas.Identity.Pages.Account.Manage
{
    public class Disable2faModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<Disable2faModel> _logger;

        public Disable2faModel(
            UserManager<ApplicationUser> userManager,
            ILogger<Disable2faModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"No fue posible cargar el usuario con el ID: '{_userManager.GetUserId(User)}'.");
            }

            if (!await _userManager.GetTwoFactorEnabledAsync(user))
            {
                throw new InvalidOperationException($"No se pudo deshabilitar la 2FA porque el usuario actualmente no la tiene habilitada.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"No fue posible cargar el usuario con el ID: '{_userManager.GetUserId(User)}'.");
            }

            var disable2faResult = await _userManager.SetTwoFactorEnabledAsync(user, false);
            if (!disable2faResult.Succeeded)
            {
                throw new InvalidOperationException($"Un error inesperado ocurrió al intentar deshabilitar la 2FA.");
            }

            _logger.LogInformation("User with ID '{UserId}' has disabled 2fa.", _userManager.GetUserId(User));
            StatusMessage = "La 2FA ha sido deshabilitada. Puede habilitarla nuevamente cuando configure una app de autenticación";
            return RedirectToPage("./TwoFactorAuthentication");
        }
    }
}
