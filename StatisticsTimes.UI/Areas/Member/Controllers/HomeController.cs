using StatisticsTimes.Model.Option;
using StatisticsTimes.Service.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatisticsTimes.UI.Areas.Member.Controllers
{
    public class HomeController : Controller
    {
        ArticleService _articleService;
        AppUserService _appUserService;
        CommentService _commentService;
        LikeService _likeService;
        public HomeController()
        {
            _articleService = new ArticleService();
            _appUserService = new AppUserService();
            _commentService = new CommentService();
            _likeService = new LikeService();
        }
         
        public ActionResult MemberHomeIndex()
        {
            TempData["class"] = "custom-hide";
            var model = _articleService.GetActive().OrderBy(x => x.CreatedDate).Take(5);
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return View(model);
            }
            AppUser user = _appUserService.FindByUserName(HttpContext.User.Identity.Name);
            if (user.Role==StatisticsTimes.Model.Option.Role.Member)
            {
                TempData["class"] = "custom-show";
            }
            return View(model);
        }

        public ActionResult ArticleList()
        {
            List<Article> model = _articleService.GetActive().OrderBy(x => x.CreatedDate).Take(5).ToList();
            return View(model);
        }
    }
}