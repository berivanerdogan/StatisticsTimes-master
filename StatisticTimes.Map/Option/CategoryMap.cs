using StatisticsTimes.Core.Map;
using StatisticsTimes.Model.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticTimes.Map.Option
{
    public class CategoryMap : CoreMap<Category>
    {
        public CategoryMap()
        {
            ToTable("dbo.Categories");
            Property(x => x.CategoryName).HasColumnName("CategoryName").HasMaxLength(90).IsOptional();
            Property(X => X.CategoryDescription).HasColumnName("Description").IsOptional();

            HasMany(x => x.Articles)
                .WithRequired(x => x.Category)
                .HasForeignKey(x => x.CategoryID).WillCascadeOnDelete(false);
        }
    }
}
