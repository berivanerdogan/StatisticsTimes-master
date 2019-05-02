using StatisticsTimes.Model.Option;
using StatisticsTimes.Service.Option;
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
        //public JsonResult AddLike(Guid id)
        //{
        //    AppUser user = _appUserService.GetByDefault(x => x.UserName == User.Identity.Name);
        //    Like like = new Like();

        //}
    }
}