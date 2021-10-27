using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tech_News.Models.DAO
{
    public class BaseDao
    {
        protected Tech_News.Models.ApplicationDbContext DB;
        public BaseDao()
        {
            DB = ApplicationDbContext.Create();
        }
    }
}