﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayrer;
using System.Web.Security;

namespace My_NewsSite.Controllers
{
    public class AccountController : Controller
    {
        My_SiteContext db = new My_SiteContext();
        private ILoginRepository loginRepository;
        public AccountController()
        {
            loginRepository = new LoginRepository(db);
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login,string ReturnUrl="/")
        {
            if (ModelState.IsValid)
            {

                if (loginRepository.IsExistUser(login.UserName, login.Password))
                {
                    FormsAuthentication.SetAuthCookie(login.UserName, login.RememberMe);
                    return Redirect(ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("UserName", "شما نمیتونی وارد سایت بشی.");
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