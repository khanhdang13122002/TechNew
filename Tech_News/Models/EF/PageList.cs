using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Tech_News.Models.EF
{
    public class PageList
    {
        public int TotalCount;
        public int TotalPage;


        public int GetPageInfo(int limit,int new_total_cout)
       {
            this.TotalCount = new_total_cout;
            double ef = TotalCount / limit;
            this.TotalPage = Convert.ToInt32(Math.Ceiling(ef))+1;
            return TotalPage;
        }
    }
}