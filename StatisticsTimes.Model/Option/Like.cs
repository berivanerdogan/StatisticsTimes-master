﻿using StatisticsTimes.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsTimes.Model.Option
{
    public class Like : CoreEntity
    {
        public Guid AppUserID { get; set; }
        public virtual AppUser AppUser { get; set; }

        public Guid ArticleID { get; set; }
        public virtual Article Article { get; set; }

        public virtual List<Like> Likes { get; set; }

        public virtual List<Comment> Comments { get; set; }

    }
}