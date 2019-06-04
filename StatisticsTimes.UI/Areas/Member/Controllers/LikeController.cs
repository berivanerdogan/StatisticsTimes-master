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
        CommentService _commentService;
        public LikeController()
        {
            _appUserService = new AppUserService();
            _likeService=new LikeService();
            _commentService = new CommentService();
        }

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult AddLike(Guid id)
        {
            LikeVM likeVM = new LikeVM();
            Guid appuserID = _appUserService.FindByUserName(HttpContext.User.Identity.Name).ID;

            if (!(_likeService.Any(x => x.AppUserID == appuserID && x.ArticleID == id)))
            {
                Like like = new Like();
                like.ArticleID = id;
                like.AppUserID = appuserID;
                _likeService.Add(like);

                //Kullanıcıya gönderilecek mesaj oluşturulur.

                likeVM.Likes = _likeService.GetDefault(x => x.ArticleID == id).Count();
                likeVM.userMessage = "likes it";
                likeVM.isSuccess = true;
                likeVM.Likes = _likeService.GetDefault(x => x.ArticleID == id && (x.Status == Core.Enum.Status.Active || x.Status == Core.Enum.Status.Updated)).Count();
                likeVM.Comments = _commentService.GetDefault(x => x.ArticleID == id && (x.Status == Core.Enum.Status.Active || x.Status == Core.Enum.Status.Updated)).Count();
                return Json(likeVM, JsonRequestBehavior.AllowGet);
            }
            else
            {
                likeVM.isSuccess = false;
                likeVM.userMessage = "You've liked this article before!";

                return Json(likeVM, JsonRequestBehavior.AllowGet);
            }
        }
    }
}