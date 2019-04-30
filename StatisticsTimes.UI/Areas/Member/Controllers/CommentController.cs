using StatisticsTimes.Model.Option;
using StatisticsTimes.Service.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatisticsTimes.UI.Areas.Member.Controllers
{
    public class CommentController : Controller
    {

        AppUserService _appUserService;
        CommentService _commentService;
        public CommentController()
        {
            _appUserService = new AppUserService();
            _commentService = new CommentService();
        }

        //public JsonResult CommentAdd(string userComment,Guid id)
        //{
        //    Comment comment = new Comment();
        //    comment.AppUserID = _appUserService.FindByUserName(User.Identity.Name).ID;
        //    comment.ArticleID = id;
        //    comment.Content = userComment;
        //    return View();
        //}
    }
}