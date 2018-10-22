using GEPED.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GEPED.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
              where T : CrudBase
        {
            protected List<T> listCrud;
            protected string key;
            public GenericRepository()
            {
                key = typeof(T).Name.ToLower();
                if (System.Web.HttpContext.Current.Session[key] != null)
                    listCrud = (List<T>)HttpContext.Current.Session[key];
                else
                    listCrud = new List<T>();
            }

            public virtual IList<T> List()
            {
                return listCrud;
            }

            public virtual T Get(string id)
            {
                return FindBy(i => i.Id == id).FirstOrDefault();
            }

            public IList<T> FindBy(Func<T, bool> predicate)
            {
                IList<T> query = listCrud.Where(predicate).ToList();
                return query;
            }

            public virtual T Add(T entity)
            {
                listCrud.Add(entity);
                HttpContext.Current.Session[key] = listCrud;
                return entity;
            }

            public virtual void Delete(T entity)
            {
                listCrud.Remove(entity);
                HttpContext.Current.Session[key] = listCrud;
            }

            public virtual void Edit(T entity)
            {
                var entityBd = listCrud.FirstOrDefault(x => x.Id == entity.Id);
                listCrud[listCrud.IndexOf(entityBd)] = entity;
                HttpContext.Current.Session[key] = listCrud;
            }
        }
    }
