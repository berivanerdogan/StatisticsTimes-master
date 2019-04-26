using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StatisticsTimes.UI.Areas.Admin.Models.DTO
{
    public class CategoryDTO
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Please Add Your CategoryName")]
        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }
    }
}