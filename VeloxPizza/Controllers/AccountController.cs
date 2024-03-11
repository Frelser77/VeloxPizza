using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using VeloxPizza.Models;

namespace Frelsex.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(MyLogin model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (ModelDbContext db = new ModelDbContext())
                {
                    // Utilizza il campo Username per trovare l'utente
                    Utente user = db.Utente.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

                    if (user != null)
                    {
                        string[] roles = new string[] { user.Role };

                        string userData = $"{user.IdUtente}|{string.Join(",", roles)}";

                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                            1, user.Username, DateTime.Now, DateTime.Now.AddMinutes(60), false, userData, FormsAuthentication.FormsCookiePath);

                        string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        HttpContext.Response.Cookies.Add(authCookie);

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tentativo di accesso non valido.");
                    }
                }
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
