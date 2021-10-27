using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tech_News.Models.DAO
{
    public interface IBaseDao<T>
    {
         Task<List<T>> GetALL(T entity);
        Task<List<T>> GetByKeyWord(string keyword);
        Task<T> GetSingleById(int id);
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Remove(int id);
    }
}
