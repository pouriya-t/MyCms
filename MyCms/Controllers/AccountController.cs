using Data.Context;
using Data.Repositories;
using Data.Services;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace MyCms.Controllers
{
    public class AccountController : Controller
    {
        private ILoginRepository _loginRepository;
        private MyCmsContext _db = new MyCmsContext();

        public AccountController()
        {
            _loginRepository = new LoginRepository(_db);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login,string ReturnUrl="/")
        {
            if (ModelState.IsValid)
            {

                if (_loginRepository.IsExistUser(login.UserName, login.Password))
                {
                    FormsAuthentication.SetAuthCookie(login.UserName,login.RememberMe);
                    return Redirect(ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("UserName", "کاربری یافت نشد");
                }
            }
            return View(login);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            return Redirect("/");
        }
    }
}
