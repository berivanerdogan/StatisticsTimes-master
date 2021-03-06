﻿using StatisticsTimes.Model.Option;
using StatisticsTimes.Service.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatisticsTimes.UI.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        ArticleService _articleService;
        AppUserService _appUserService;
        public HomeController()
        {
            _articleService = new ArticleService();
            _appUserService = new AppUserService();
        }
        public ActionResult AdminHomeIndex()
        {
            TempData["class"] = "custom-hide";
            var model = _articleService.GetActive().OrderBy(x => x.CreatedDate).Take(5);
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return View(model);
            }
            AppUser user = new AppUser();
            user = _appUserService.FindByUserName(HttpContext.User.Identity.Name);
            if (user.Role == StatisticsTimes.Model.Option.Role.Admin)
            {
                TempData["class"] = "custom-show";
            }
            return View(model);
        }
    }
}