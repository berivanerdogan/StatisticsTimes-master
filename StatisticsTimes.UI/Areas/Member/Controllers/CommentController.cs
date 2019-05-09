using StatisticsTimes.Model.Option;
using StatisticsTimes.Service.Option;
using StatisticsTimes.UI.Areas.Member.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatisticsTimes.UI.Areas.Member.Controllers
{
    public class CommentController : Controller
    {

        ArticleService _articleService;
        AppUserService _appUserService;
        CommentService _commentService;
        LikeService _likeService;
        public CommentController()
        {
            _articleService = new ArticleService();
            _appUserService = new AppUserService();
            _commentService = new CommentService();
            _likeService = new LikeService();
        }

        public ActionResult Show(Guid id)
        {
            ArticleDetailVM model = new ArticleDetailVM();
            model.Article = _articleService.GetByID(id);
            model.AppUser = _appUserService.GetByID(model.Article.AppUser.ID);
            model.Comments = _commentService.GetDefault(x => x.ArticleID == id);
            model.Likes = _likeService.GetDefault(x => x.ArticleID == id);
            model.CommentCount = _commentService.GetDefault(x => x.ArticleID == id).Count();
            model.LikeCount = _likeService.GetDefault(x => x.ArticleID == id).Count();
            return View(model);
        }

        public JsonResult AddComment(string userComment, Guid id)
        {
            Comment comment = new Comment();
            comment.AppUserID = _appUserService.FindByUserName(User.Identity.Name).ID;
            comment.ArticleID = id;
            comment.Content = userComment;
            bool isAdded = false;
            try
            {
                _commentService.Add(comment);
                isAdded = true;
            }
            catch (Exception ex)
            {

                isAdded = false;
            }
            return Json(isAdded,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAtricleComment(string id)
        {
            Guid articleID = new Guid(id);

            Comment comment = _commentService.GetDefault(x => x.ArticleID == articleID && x.Status == Core.Enum.Status.Active).LastOrDefault();


            return Json(new
            {
                AppUserImagePath = comment.AppUser.UserImage,
                FirstName = comment.AppUser.FirstName,
                LastName = comment.AppUser.LastName,
                CreatedDate = comment.CreatedDate.ToString(),
                Content = comment.Content
            }, JsonRequestBehavior.AllowGet);
        }

      
        public JsonResult Delete(Guid id)
        {
            Guid userID = _appUserService.FindByUserName(HttpContext.User.Identity.Name).ID;
            bool isDelete = false;


            if (_commentService.Any(x => x.AppUserID == userID))
            {
                isDelete = true;
                _commentService.Remove(id);
                return Json(isDelete, JsonRequestBehavior.AllowGet);
            }
            else
            {
                isDelete = false;
                return Json(isDelete, JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult Delete(Guid id)
        //{
        //    _commentService.Remove(id);
        //    return Redirect("/Member/Comment/Show");
        //}
    }
}