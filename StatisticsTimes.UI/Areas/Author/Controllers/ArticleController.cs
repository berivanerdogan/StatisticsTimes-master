using StatisticsTimes.Model.Option;
using StatisticsTimes.Service.Option;
using StatisticsTimes.UI.Areas.Author.Models.DTO;
using StatisticsTimes.UI.Areas.Author.Models.VM;
using StatisticsTimes.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatisticsTimes.UI.Areas.Author.Controllers
{
    public class ArticleController : Controller
    {
        ArticleService _articleService;
        AppUserService _appUserService;
        CategoryService _categorryService;
        public ArticleController()
        {
            _articleService = new ArticleService();
            _appUserService = new AppUserService();
            _categorryService = new CategoryService();
        }


        public ActionResult ArticleList()
        {
            Guid userid = _appUserService.FindByUserName(User.Identity.Name).ID;
            List<Article> model = _articleService.GetDefault(x => x.AppUserID == userid && (x.Status == StatisticsTimes.Core.Enum.Status.Active || x.Status == StatisticsTimes.Core.Enum.Status.Updated));
            return View(model);
        }

        public ActionResult ArticleAdd()
        {
            return View(_categorryService.GetActive());
        }

        [HttpPost]
        public ActionResult ArticleAdd(Article article,HttpPostedFileBase Image)
        {
            List<string> UploadedImagePaths = new List<string>();
            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalImageProfilePath, Image, 1);
            article.ImagePath = UploadedImagePaths[0];
            if (article.ImagePath=="0"||article.ImagePath=="1"||article.ImagePath=="2")
            {
                article.ImagePath = ImageUploader.DefaultProfileImagePath;
                article.ImagePath = ImageUploader.DefaultXSmallProfileImagePath;
                article.ImagePath = ImageUploader.DefaultCruptedImagesProfileImagePath;
            }
                AppUser user = _appUserService.GetByDefault(x => x.UserName == HttpContext.User.Identity.Name);
                article.AppUserID = user.ID;
                _articleService.Add(article);
                TempData["Başarılı"] = "Successful";
                return Redirect("/Author/Article/ArticleList");
       
        }
        public ActionResult UpdateArticle(Guid id)
        {
            Article article = _articleService.GetByID(id);
            ArticleVM model = new ArticleVM();
            model.Articles.ID = article.ID;
            model.Articles.Header = article.Header;
            model.Articles.Content = article.Content;
            model.Articles.PublishDate = DateTime.Now;
            model.Articles.ImagePath = article.ImagePath;

            List<Category> Categories = _categorryService.GetActive();
            model.Categories = Categories;
            List<AppUser> AppUsers = _appUserService.GetDefault(x => (x.Role == StatisticsTimes.Model.Option.Role.Admin || x.Role == StatisticsTimes.Model.Option.Role.Author) && (x.Status == StatisticsTimes.Core.Enum.Status.Active || x.Status == StatisticsTimes.Core.Enum.Status.Updated));
            model.AppUsers = AppUsers;
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateArticle(ArticleDTO article, HttpPostedFileBase Image)
        {
            List<string> UploadedImagePaths = new List<string>();
            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalImageProfilePath, Image, 1);
            article.ImagePath = UploadedImagePaths[0];

            Article update = _articleService.GetByID(article.ID);
            if (article.ImagePath=="0"||article.ImagePath=="1"||article.ImagePath=="2")
            {
                if (update.ImagePath == null || update.ImagePath == ImageUploader.DefaultXSmallProfileImagePath)
                {
                    update.ImagePath = ImageUploader.DefaultXSmallProfileImagePath;
                    update.ImagePath = ImageUploader.DefaultProfileImagePath;
                    update.ImagePath = ImageUploader.DefaultCruptedImagesProfileImagePath;
                }
                else
                {
                    update.ImagePath = article.ImagePath;
                }
            }
            else
            {
                update.ImagePath = UploadedImagePaths[0];
                update.ImagePath = UploadedImagePaths[1];
                update.ImagePath = UploadedImagePaths[2];
            }
            update.Header = article.Header;
            update.SubTitle = article.SubTitle;
            update.Content = article.Content;
            update.PublishDate = DateTime.Now;
            update.AppUserID = article.AppUserID;
            update.CategoryID = article.CategoryID;
            update.ImagePath = article.ImagePath;
            _articleService.Save();
            return Redirect("/Author/Article/ArticleList");


        }

        public ActionResult ArticleShow(Article article)
        {
            AppUser user = _appUserService.GetByDefault(x => x.UserName == HttpContext.User.Identity.Name);

            List<Article> model = _articleService.GetDefault(x => x.AppUserID != user.ID && (x.Status == StatisticsTimes.Core.Enum.Status.Active || x.Status == StatisticsTimes.Core.Enum.Status.Updated));
            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            _articleService.Remove(id);
            return Redirect("/Author/Article/ArticleList");
        }
    }
}