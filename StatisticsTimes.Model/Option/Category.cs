using StatisticsTimes.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsTimes.Model.Option
{
    public class Category : CoreEntity
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public virtual List<Article> Articles { get; set; }
    }
}
