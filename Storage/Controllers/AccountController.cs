using DomainModel.Models;
using DomainModel.Repositories;
using System.Web.Mvc;
using System.Web.Security;

namespace Storage.Controllers
{
    public class AccountController : Controller
    {
        private UserRepository UserRepository { get; set; }

        public AccountController()
        {
            UserRepository = new UserRepository();
        }

        // GET: Account
        [HttpGet]
        public ActionResult CreateUser()
        {
            var newuser = new UserAccount();
            return View(newuser);
        }

        [HttpPost]
        public ActionResult CreateUser(UserAccount model)
        {
            var newuser = new UserAccount();
            newuser.Username = model.Username;
            newuser.Password = model.Password;
            var rep = UserRepository;
            rep.Add(newuser);

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult Login()
        {
            var newuser = new UserAccount();
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount model)
        {
            var newuser = new UserAccount();
            newuser.Username = model.Username;
            newuser.Password = model.Password;
            var rep = UserRepository;

            if (rep.Find(newuser.Username, newuser.Password) == 1)
            {
                FormsAuthentication.SetAuthCookie(model.Username, true);
                return RedirectToAction("Index", "File");
            }

            else
            {
                ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                return View();
            }
        }

        
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }   


    }
}