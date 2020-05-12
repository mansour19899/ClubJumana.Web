using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.Core.DTOs
{
    public class RegisterViewModel
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [MaxLength(200, ErrorMessage = "{0} Can't More {1}")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [MaxLength(200, ErrorMessage = "{0} Can't More {1}")]
        [EmailAddress(ErrorMessage = "Email is not Valid")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = " Please Enter {0} ")]
        [MaxLength(200, ErrorMessage = "{0} Can't More  {1}")]
        public string Password { get; set; }

        [Display(Name = "RePassword")]
        [Required(ErrorMessage = " Please Enter {0} ")]
        [MaxLength(200, ErrorMessage = "{0} Can't More  {1}")]
        [Compare("Password",ErrorMessage = "Not Same With Password")]
        public string RePassword { get; set; }

        [Display(Name = "Active Code")]
        [MaxLength(200, ErrorMessage = "{0} Can't More  {1}")]
        public string ActiveCode { get; set; }
    }

    public class LoginViewModel
    {
        [Display(Name = "User Name or Email")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [MaxLength(200, ErrorMessage = "{0} Can't More {1}")]
        public string UserNameEmail { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = " Please Enter {0} ")]
        [MaxLength(200, ErrorMessage = "{0} Can't More  {1}")]
        public string Password { get; set; }
    }

    public class InvitationViewModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [MaxLength(200, ErrorMessage = "{0} Can't More {1}")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = " Please Enter {0} ")]
        [MaxLength(200, ErrorMessage = "{0} Can't More  {1}")]
        public string Email { get; set; }

        [Display(Name = "Active Code")]
        [MaxLength(200, ErrorMessage = "{0} Can't More  {1}")]
        public string ActiveCode { get; set; }
    }

    public class ForgotPasswordViewMode
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [MaxLength(200, ErrorMessage = "{0} Can't More {1}")]
        [EmailAddress(ErrorMessage = "Email is not Valid")]
        public string Email { get; set; }
    }

    public class RestPasswordViewMode
    {
        public string ActiveCode { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = " Please Enter {0} ")]
        [MaxLength(200, ErrorMessage = "{0} Can't More  {1}")]
        public string Password { get; set; }

        [Display(Name = "RePassword")]
        [Required(ErrorMessage = " Please Enter {0} ")]
        [MaxLength(200, ErrorMessage = "{0} Can't More  {1}")]
        [Compare("Password", ErrorMessage = "Not Same With Password")]
        public string RePassword { get; set; }
    }
}
