using Cibb.Core.DAL;
using Cibb.Web.Contract;
using Cibb.Web.Contract.Model;
using System;
using System.Linq;
using System.Linq.Expressions;
namespace Cibb.Web.DAL
{
    public class NewsRepository:IRepository<New>
    {
        private WebDBContext dbContext = new WebDBContext();

        public New GetById(int id)
        {
            var oj = dbContext.Find<New>(id);
            return oj;
        }

        public void Save(New t)
        {
            if (t.newsid > 0)
            {
                dbContext.Update<New>(t);
            }
            else
            {
                dbContext.Insert<New>(t);
            }
        }

        public void Delete(int id)
        {
            var oj = GetById(id);
            dbContext.Delete(oj);
        }

        public New Find(Expression<Func<New, bool>> conditions = null)
        {
            var oj = dbContext.FindAll(conditions).FirstOrDefault();
            return oj;
        }

        public IQueryable<New> FindAll(Expression<Func<New, bool>> conditions = null)
        {
            var oj = dbContext.FindAll(conditions);
            return oj;
        }

        public IQueryable<New> FindAllByPage<S>(Expression<Func<New, bool>> conditions, Expression<Func<New, S>> orderBy, int PageSize, int page)
        {
            var oj = dbContext.FindAllByPage(conditions, orderBy, PageSize, page);
            return oj;
        }

        public IQueryable<New> FindList<S>(Expression<Func<New, bool>> conditions, Expression<Func<New, S>> orderBy, int count)
        {
            var oj = dbContext.FindList(conditions, orderBy, count);
            return oj;
        }


        public IQueryable<New> FindAllByPage2<S>(Expression<Func<New, bool>> conditions, Expression<Func<New, S>> orderBy1, Expression<Func<New, S>> orderBy2, int PageSize, int page)
        {
            throw new NotImplementedException();
        }
    }
}
