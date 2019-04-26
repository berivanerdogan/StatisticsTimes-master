using StatisticsTimes.Model.Option;
using StatisticsTimes.Service.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatisticsTimes.Service.Option
{
   
        public class AppUserService : ServiceBase<AppUser>
        {
            public bool CheckCredential(string userName, string password)
            {
                return Any(x => x.UserName == userName && x.Password == password);
            }

            public AppUser FindByUserName(string userName)
            {
                return GetByDefault(x => x.UserName == userName);
            }
        }
}

