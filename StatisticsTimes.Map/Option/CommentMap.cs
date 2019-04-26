using StatisticsTimes.Core.Map;
using StatisticsTimes.Model.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsTimes.Map.Option
{
    public class CommentMap : CoreMap<Comment>
    {
        public CommentMap()
        {
            ToTable("dbo.Comments");
            Property(x => x.Content).IsRequired();

            HasRequired(x => x.AppUser)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.AppUserID).WillCascadeOnDelete(false);

            HasRequired(x => x.Article)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.ArticleID).WillCascadeOnDelete(false);
        }
    }
}
