using StatisticsTimes.Model.Option;
using StatisticsTimes.Service.Option;
using StatisticsTimes.UI.Areas.Member.Models.VM;
using StatisticsTimes.UI.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatisticsTimes.UI.Areas.Member.Controllers
{
    public class LikeController : Controller
    {

        AppUserService _appUserService;
        LikeService _likeService;
        public LikeController()
        {
            _appUserService = new AppUserService();
            _likeService=new LikeService();

        }

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult AddLike(Guid id)
        {
            LikeVM likeVM = new LikeVM();
            Guid appUserID = _appUserService.FindByUserName(HttpContext.User.Identity.Name).ID;

            if (!(_likeService.Any(x => x.AppUserID == appUserID && x.ArticleID == id)))
            {
                Like like = new Like();
                like.ArticleID = id;
                like.AppUserID = appUserID;
                _likeService.Add(like);


                likeVM.Likes = _likeService.GetDefault(x => x.ArticleID == id).Count();
                likeVM.userMessage = "";
                likeVM.isSuccess = true;

                return Json(likeVM, JsonRequestBehavior.AllowGet);
            }
            likeVM.isSuccess = false;
            likeVM.userMessage = "Bu yazıyı daha önce beğendiniz!";
            likeVM.Likes = _likeService.GetDefault(x => x.ArticleID == id).Count();
            return Json(likeVM, JsonRequestBehavior.AllowGet);

        }
    }
}