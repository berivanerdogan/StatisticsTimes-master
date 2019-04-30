using StatisticsTimes.Model.Option;
using StatisticsTimes.Service.Option;
using StatisticsTimes.UI.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace StatisticsTimes.UI.Controllers
{
    public class AccountController : Controller
    {
        AppUserService _appuserservice;
        public AccountController()
        {
            _appuserservice = new AppUserService();
        }

        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser appUser = _appuserservice.FindByUserName(User.Identity.Name);
                if (appUser.Role==StatisticsTimes.Model.Option.Role.Admin)
                {
                    Session["FullName"] = appUser.FirstName + " " + appUser.LastName;
                    Session["UserImage"] = appUser.UserImage;

                    return Redirect("/Admin/Home/AdminHomeIndex");
                }
                else if (appUser.Role == StatisticsTimes.Model.Option.Role.Author)
                {
                    Session["FullName"] = appUser.FirstName + " " + appUser.LastName;
                    Session["UserImage"] = appUser.UserImage;

                    return Redirect("/Author/Home/AuthorHomeIndex");
                }
                else if (appUser.Role == StatisticsTimes.Model.Option.Role.Member)
                {
                    Session["FullName"] = appUser.FirstName + " " + appUser.LastName;
                    Session["UserImage"] = appUser.UserImage;

                    return Redirect("/Member/Home/MemberHomeIndex");
                }
            }
            TempData["class"] = "custom-hide";
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM credential)
        {
            if (ModelState.IsValid)
            {
                if (_appuserservice.CheckCredential(credential.UserName, credential.Password))
                {
                    AppUser user = _appuserservice.FindByUserName(credential.UserName);
                    string cookie = user.UserName;
                    FormsAuthentication.SetAuthCookie(cookie, true);
                    if (user.Role == StatisticsTimes.Model.Option.Role.Admin)
                    {
                        Session["FullName"] = user.FirstName + " " + user.LastName;
                        Session["UserImage"] = user.UserImage;
                        return Redirect("/Admin/Home/AdminHomeIndex");
                    }
                    else if (user.Role == StatisticsTimes.Model.Option.Role.Author)
                    {
                        Session["FullName"] = user.FirstName + " " + user.LastName;
                        Session["UserImage"] = user.UserImage;

                        return Redirect("/Author/Home/AuthorHomeIndex");
                    }
                    else if (user.Role == StatisticsTimes.Model.Option.Role.Member)
                    {
                        Session["FullName"] = user.FirstName + " " + user.LastName;
                        Session["UserImage"] = user.ImagePath;

                        return Redirect("/Member/Home/MemberHomeIndex");
                    }
                }
                else
                {
                    ViewData["error"] = "Username or password is wrong";
                    return View();
                }
            }

            TempData["class"] = "custom-show";
            return View();
        }

        public ActionResult LoginOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Account/Login");
        }

    }

}
