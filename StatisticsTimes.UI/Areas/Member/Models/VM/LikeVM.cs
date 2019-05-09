using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StatisticsTimes.UI.Areas.Member.Models.VM
{
    public class LikeVM
    {
        public string userMessage { get; set; }
        public int Likes { get; set; }
        public bool isSuccess { get; set; }
    }
}