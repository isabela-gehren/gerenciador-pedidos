using GEPED.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GEPED.Repository
{
    public interface IGenericRepository<T> where T : CrudBase
    {
        IList<T> List();
        T Get(string id);
        IList<T> FindBy(Func<T, bool> predicate);
        T Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
    }
}
