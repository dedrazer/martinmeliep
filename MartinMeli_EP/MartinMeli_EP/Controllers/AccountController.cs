using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MartinMeli_EP.Models;
using System.Collections.Specialized;

namespace MartinMeli_EP.Controllers
{
    public class AccountController : Controller
    {
        Logic.AccountLogic al = new Logic.AccountLogic();
        //
        // GET: /Account/

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Data.journalist newUser = new Data.journalist
                    { username = model.UserName, password = model.Password, email=model.Email, about=model.About, name=model.Name, surname=model.Surname};
                
                if (al.Register(newUser))
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (al.Login(model.UserName, model.Password))
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        
        public ActionResult Logout()
        {
            if (Logic.AccountLogic.loggedIn)
                al.Logout();
            return View();
        }

        public ActionResult Journalists()
        {
            int id = Logic.AccountLogic.currentUserId;

            if (id != -1)
            {
                ViewBag.Journalists = al.GetAllUsers();
                return View();
            }
            return RedirectToAction("Unauthorized", "Journal");
        }

        public ActionResult Delete(int id)
        {
            int uid = Logic.AccountLogic.currentUserId;

            if (uid != -1)
            {
                al.DeleteUser(id);
                return View();
            }
            return RedirectToAction("Unauthorized", "Journal");
        }

        public ActionResult Edit(int id)
        {
            int uid = Logic.AccountLogic.currentUserId;

            if (uid != -1)
            {
                NameValueCollection nvc = Request.Form;
                string username, password, name, surname, about, email;
                if (!string.IsNullOrEmpty(nvc["UserName"]))
                {
                    username = nvc["UserName"];

                    if (!string.IsNullOrEmpty(nvc["Password"]))
                    {
                        password = nvc["Password"];

                        if (!string.IsNullOrEmpty(nvc["Name"]))
                        {
                            name = nvc["Name"];

                            if (!string.IsNullOrEmpty(nvc["Surname"]))
                            {
                                surname = nvc["Surname"];

                                if (!string.IsNullOrEmpty(nvc["About"]))
                                {
                                    about = nvc["About"];

                                    if (!string.IsNullOrEmpty(nvc["Email"]))
                                    {
                                        email = nvc["Email"];
                                        al.UpdateJournalist(id, new Data.journalist() { username = username, password = password, name = name, surname = surname, about = about, email = email });
                                        return RedirectToAction("Home", "Index");
                                    }
                                }
                            }
                        }
                    }
                }

                return View();
            }
            return RedirectToAction("Unauthorized", "Journal");
        }
    }
}
