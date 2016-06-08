using Cibb.Core.DAL;
using Cibb.Web.Contract;
using Cibb.Web.Contract.Model;
using System;
using System.Linq;

namespace Cibb.Web.DAL
{
    public class BranchDirectorRepository : IRepository<BranchDirector>
    {
        private WebDBContext dbContext = new WebDBContext();
        public BranchDirector GetById(int id)
        {
            var oj = dbContext.Find<BranchDirector>(id);
            return oj;
        }

        public void Save(BranchDirector t)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public BranchDirector Find(System.Linq.Expressions.Expression<Func<BranchDirector, bool>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BranchDirector> FindAll(System.Linq.Expressions.Expression<Func<BranchDirector, bool>> conditions = null)
        {
            var oj = dbContext.FindAll(conditions);
            return oj;
        }

        public IQueryable<BranchDirector> FindAllByPage<S>(System.Linq.Expressions.Expression<Func<BranchDirector, bool>> conditions, System.Linq.Expressions.Expression<Func<BranchDirector, S>> orderBy, int PageSize, int page)
        {
            var oj = dbContext.FindAllByPage(conditions, orderBy, PageSize, page);
            return oj;
        }

        public IQueryable<BranchDirector> FindList<S>(System.Linq.Expressions.Expression<Func<BranchDirector, bool>> conditions, System.Linq.Expressions.Expression<Func<BranchDirector, S>> orderBy, int count)
        {
            throw new NotImplementedException();
        }


        public IQueryable<BranchDirector> FindAllByPage2<S>(System.Linq.Expressions.Expression<Func<BranchDirector, bool>> conditions, System.Linq.Expressions.Expression<Func<BranchDirector, S>> orderBy1, System.Linq.Expressions.Expression<Func<BranchDirector, S>> orderBy2, int PageSize, int page)
        {
            throw new NotImplementedException();
        }
    }
}
