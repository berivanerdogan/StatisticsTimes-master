using StatisticsTimes.Model.Option;
using StatisticsTimes.Service.Option;
using StatisticsTimes.UI.Areas.Admin.Models.DTO;
using StatisticsTimes.UI.Areas.Admin.Models.VM;
using StatisticsTimes.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatisticsTimes.UI.Areas.Admin.Controllers
{
    public class ArticleController : Controller
    {
        ArticleService _articleService;
        CategoryService _categoryService;
        AppUserService _appUserService;
        public ArticleController()
        {
            _articleService = new ArticleService();
            _categoryService = new CategoryService();
            _appUserService = new AppUserService();
        }

        public ActionResult ArticleAdd()
        {
            ArticleVM model = new ArticleVM()
            {
                Categories = _categoryService.GetActive(),
                AppUsers = _appUserService.GetDefault(x=>(x.Role==StatisticsTimes.Model.Option.Role.Admin ||x.Role == StatisticsTimes.Model.Option.Role.Author)&& (x.Status==StatisticsTimes.Core.Enum.Status.Active|| x.Status == StatisticsTimes.Core.Enum.Status.Active)),
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult ArticleAdd(Article article, HttpPostedFileBase Image)
        {

            List<string> UploadedImagePaths = new List<string>();
            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalImageProfilePath, Image, 1);
            article.ImagePath = UploadedImagePaths[0];
            if (article.ImagePath == "0" || article.ImagePath == "1" || article.ImagePath == "2")
            {
                article.ImagePath = ImageUploader.DefaultProfileImagePath;
                article.ImagePath = ImageUploader.DefaultCruptedImagesProfileImagePath;
                article.ImagePath = ImageUploader.DefaultXSmallProfileImagePath;
            }

            string currentUser = User.Identity.Name;
            AppUser user = _appUserService.GetByDefault(x => x.UserName == currentUser);
            article.AppUserID = user.ID;
            _articleService.Add(article);
            TempData["Başarılı"] = "Successful";
            return Redirect("/Admin/Article/ArticleList");
          
        }
        public ActionResult ArticleList()
        {
            List<Article> model = _articleService.GetActive();
            return View(model);

        }

        public ActionResult UpdateArticle(Guid id)
        {
            Article article = _articleService.GetByID(id);
            ArticleVM model = new ArticleVM();
            model.Articles.ID = article.ID;
            model.Articles.Header = article.Header;
            model.Articles.Content = article.Content;
            model.Articles.SubTitle = article.SubTitle;
            model.Articles.PublishDate = DateTime.Now;

            List<Category> Categories = _categoryService.GetActive();
            model.Categories = Categories;

            List<AppUser> AppUsers = _appUserService.GetDefault(x => (x.Role == StatisticsTimes.Model.Option.Role.Admin || x.Role == StatisticsTimes.Model.Option.Role.Author) && (x.Status == StatisticsTimes.Core.Enum.Status.Active || x.Status == StatisticsTimes.Core.Enum.Status.Active));
            model.AppUsers = AppUsers;

            return View(model);           
        }
        [HttpPost]
        public ActionResult UpdateArticle(ArticleDTO model, HttpPostedFileBase Image)
        {
            List<string> UploadedImagePaths = new List<string>();
            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalImageProfilePath, Image, 1);
            model.ImagePath = UploadedImagePaths[0];

            Article update = _articleService.GetByID(model.ID);
            if (model.ImagePath == "0" || model.ImagePath == "1" || model.ImagePath == "2")
            {
                if (update.ImagePath == null || update.ImagePath == ImageUploader.DefaultProfileImagePath)
                {
                    update.ImagePath = ImageUploader.DefaultProfileImagePath;
                    update.ImagePath = ImageUploader.DefaultCruptedImagesProfileImagePath;
                    update.ImagePath = ImageUploader.DefaultXSmallProfileImagePath;
                }
                else
                {
                    update.ImagePath = model.ImagePath;
                }
            }
            else
            {
                update.ImagePath = UploadedImagePaths[0];
                update.ImagePath = UploadedImagePaths[1];
                update.ImagePath = UploadedImagePaths[2];
            }

            update.Header = model.Header;
            update.SubTitle = model.SubTitle;
            update.Content = model.Content;
            update.ImagePath = model.ImagePath;
            update.AppUserID = model.AppUserID;
            update.CategoryID = model.CategoryID;
            update.PublishDate = DateTime.Now;
            _articleService.Save();
            return Redirect("/Admin/Article/ArticleList");
          
        }

        public ActionResult Delete(Guid id)
        {
            _articleService.Remove(id);
            return Redirect("/Admin/Article/ArticleList");
        }
    }
}