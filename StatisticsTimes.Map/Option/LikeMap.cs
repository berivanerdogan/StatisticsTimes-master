using StatisticsTimes.Core.Map;
using StatisticsTimes.Model.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsTimes.Map.Option
{
    public class LikeMap : CoreMap<Like>
    {
        public LikeMap()
        {
            ToTable("dbo.Likes");

            HasRequired(x => x.AppUser)
                .WithMany(x => x.Likes)
                .HasForeignKey(x => x.AppUserID).WillCascadeOnDelete(false);

            HasRequired(x => x.Article)
                .WithMany(x => x.Likes)
                .HasForeignKey(x => x.ArticleID).WillCascadeOnDelete(false);
        }
    }
}
