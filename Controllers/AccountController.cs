using Finders.Interfaces;
using Finders.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Finders.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous, HttpGet("forgot-password"), ]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [AllowAnonymous, HttpPost("forgot-password")]
        public IActionResult ForgotPassword(ForgotPassword model)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                model.EmailSent = true;
            }
          
            return View();
        }
    }

}
