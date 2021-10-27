using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech_News.Models.DAO
{
    public interface IPagesList<T>
    {
        Task<List<T>> GetByPages(int currentPage, int pageSize);
    }
}
