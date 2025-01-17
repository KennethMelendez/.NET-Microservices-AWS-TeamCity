﻿using System.ComponentModel.DataAnnotations;

namespace WebAdvert.Web.Models.Accounts
{
    public class ConfirmViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [Display(Name ="Emal")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; }
    }
}
