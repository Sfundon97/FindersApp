using Finders.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;

namespace Finders.Models
{
    public class ForgotPassword
    {
        [Required, EmailAddress, Display(Name = "Enter your registerd email")]
        public string Email { get; set; }
        public bool EmailSent { get; set; }
    }

}
