using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClubJumana.Core.DTOs;
using ClubJumana.Core.Convertors;
using ClubJumana.Core.Generator;
using ClubJumana.Core.Services.Interfaces;
using ClubJumana.DataLayer.Entities.Users;
using Microsoft.AspNetCore.Mvc;
using ClubJumana.Core.Security;
using ClubJumana.Core.Senders;
using ClubJumana.DataLayer.Entities.User;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ClubJumana.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IViewRenderService _viewRender;

        public AccountController(IUserService userService, IViewRenderService viewRender)
        {
            _userService = userService;
            _viewRender = viewRender;
        }


        #region Register
        [Route("Register/{id?}")]
        [HttpGet]
        public IActionResult Register(string id)
        {
            var Invitation = _userService.AllowRegister(id);
            if (Invitation.Id != 0)
            {
                return View(new RegisterViewModel(){ActiveCode = id});
            }
            else
            {
                return Redirect("/Login");
            }

        }

        [Route("Register/{id?}")]
        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            

            if (!ModelState.IsValid)
            {
                return View("Register");
            }

            if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "Chose another UserName");
                return View("Register");
            }

            if (_userService.IsExistEmail(FixedText.FixEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "This Email Taken");
                return View("Register");
            }

            User user = new User()
            {
                ActiveCode = register.ActiveCode,
                Email = FixedText.FixEmail(register.Email),
                IsActive = true,
                Password = PasswordHelper.EncodePasswordMd5(register.Password),
                UserAvatar = "DefultUserAvater.png",
                UserName = register.UserName,
                RegisterDate = DateTime.Now,
            };

            _userService.AddUser(user);

            #region Send Success Regestration

            string body = _viewRender.RenderToStringAsync("_SuccessRegestration", user);
            SendEmail.Send(user.Email,"TestFirstSend",body);

            #endregion
            return View("SuccessRegister", user);
        }

        #endregion

        #region Login

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }

            var user = _userService.LoginUser(login);
            if (user != null)
            {
                if (!user.IsActive)
                {
                    ModelState.AddModelError("Email", "Email is not active.");
                    return View(login);
                }

                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var properties = new AuthenticationProperties
                {
                    IsPersistent = false
                };
                HttpContext.SignInAsync(principal, properties);
                ViewBag.IsSuccess = true;
                return View();
            }
            else
            {
                ModelState.AddModelError("Email","User or Password not correct.");
            }

            return View(login);
        }


        #endregion

        #region Login

        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }

        #endregion

        #region Invitation

        [Route("Invitation")]
        public IActionResult Invitation()
        {
            return View();
        }

        [HttpPost]
        [Route("Invitation")]
        public IActionResult Invitation(InvitationViewModel invitation)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Invitation Newinvitation = new Invitation()
            {
                ActiveCode = CodeGenerator.GenerateUniqCode(),
                Email = FixedText.FixEmail(invitation.Email),
                Name = invitation.Name,
                SendInvitationDate = DateTime.Now,
                ExpireInvitationDate = DateTime.Today.AddDays(15),
                UserSendInvitation_fk =Convert.ToInt16(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value)
            };

            #region Send Success Regestration

            invitation.ActiveCode = Newinvitation.ActiveCode;
            string body = _viewRender.RenderToStringAsync("_InvitationEmail", invitation);
            SendEmail.Send(invitation.Email, "TestFirstSendInvitation", body);

            #endregion

            _userService.AddInvitation(Newinvitation);

            ViewBag.SuccessSendEmail = true;
            return View();
        }

        #endregion

        #region ForgotPassword

        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordViewMode forgotPassword)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            string fixedEmail = FixedText.FixEmail(forgotPassword.Email);

            var user = _userService.GetUserByEmail(fixedEmail);

            if (user == null)
            {
                ModelState.AddModelError(fixedEmail,"Email not find");
                return View(forgotPassword);
            }

            string bodyEmail = _viewRender.RenderToStringAsync("_ForgotPassword", user);
            SendEmail.Send(user.Email,"Recovery Password",bodyEmail);

            ViewBag.IsSuccess = true;
            return View(forgotPassword);
        }

        #endregion

        #region RestPassword

        [Route("ResetPassword/{id?}")]
        public IActionResult ResetPassword(string id)
        {
            return View(new RestPasswordViewMode()
            {
                ActiveCode = id
            });
        }

        [HttpPost]
        [Route("ResetPassword/{id?}")]
        public IActionResult ResetPassword(RestPasswordViewMode restPassword)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = _userService.GetUserByActiveCode(restPassword.ActiveCode);

            if (user == null)
                return NotFound();

            string hashNewPassword = PasswordHelper.EncodePasswordMd5(restPassword.Password);
            user.Password = hashNewPassword;
            _userService.UpdateUser(user);


            return Redirect("/Login");
        }

        #endregion

    }
}