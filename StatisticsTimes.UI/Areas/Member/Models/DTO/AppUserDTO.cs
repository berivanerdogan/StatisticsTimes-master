using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatisticsTimes.UI.Areas.Member.Models.DTO
{
    public class AppUserDTO
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserImage { get; set; }
        public string ImagePath { get; set; }
        public string XSmallUserImage { get; set; }
        public string CruptedUserImage { get; set; }
        public string Adress { get; set; }

        public Guid CommentID { get; set; }
        public Guid ArticleID { get; set; }
        public Guid LikeID { get; set; }

    }
}