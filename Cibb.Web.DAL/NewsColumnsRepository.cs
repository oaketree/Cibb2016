using Cibb.Core.DAL;
using Cibb.Web.Contract;
using Cibb.Web.Contract.Model;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Cibb.Web.DAL
{
    public class NewsColumnsRepository : IRepository<NewsColumn>
    {
        private WebDBContext dbContext = new WebDBContext();
        public NewsColumn GetById(int id)
        {
            var oj = dbContext.Find<NewsColumn>(id);
            return oj;
        }

        public void Save(NewsColumn t)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public NewsColumn Find(Expression<Func<NewsColumn, bool>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<NewsColumn> FindAll(Expression<Func<NewsColumn, bool>> conditions = null)
        {
            var oj = dbContext.FindAll(conditions);
            return oj;
        }

        public IQueryable<NewsColumn> FindAllByPage<S>(Expression<Func<NewsColumn, bool>> conditions, Expression<Func<NewsColumn, S>> orderBy, int PageSize, int page)
        {
            throw new NotImplementedException();
        }

        public IQueryable<NewsColumn> FindList<S>(Expression<Func<NewsColumn, bool>> conditions, Expression<Func<NewsColumn, S>> orderBy, int count)
        {
            var oj = dbContext.FindList(conditions, orderBy, count);
            return oj;
        }


        public IQueryable<NewsColumn> FindAllByPage2<S>(Expression<Func<NewsColumn, bool>> conditions, Expression<Func<NewsColumn, S>> orderBy1, Expression<Func<NewsColumn, S>> orderBy2, int PageSize, int page)
        {
            throw new NotImplementedException();
        }
    }
}
