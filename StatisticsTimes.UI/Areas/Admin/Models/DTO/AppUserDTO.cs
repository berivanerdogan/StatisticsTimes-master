using StatisticsTimes.Model.Option;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StatisticsTimes.UI.Areas.Admin.Models.DTO
{
    public class AppUserDTO
    {
        public Guid ID { get; set; }
        [Required(ErrorMessage ="Please Add Your UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Please Add Your Password")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Please Add Your Email")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Please Add Your FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Add Your LastNmae")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Add Your PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please Choose Your Role")]
        public Role Role { get; set; }

        [Required(ErrorMessage = "Please Add Your Address")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Please Add Your ImagePath")]
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Please Add Your UserImage")]
        public string UserImage { get; set; }
        [Required(ErrorMessage = "Please Add Your XSmallUserImage")]
        public string XSmallUserImage { get; set; }
        [Required(ErrorMessage = "Please Add Your CruptedUserImage")]
        public string CruptedUserImage { get; set; }
    }
}