using Cibb.Core.DAL;
using Cibb.Web.Contract;
using Cibb.Web.Contract.Model;
using System;
using System.Linq;

namespace Cibb.Web.DAL
{
    public class BranchMemberRepository : IRepository<BranchMember>
    {
        private WebDBContext dbContext = new WebDBContext();

        public BranchMember GetById(int id)
        {
            var oj = dbContext.Find<BranchMember>(id);
            return oj;
        }

        public void Save(BranchMember t)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public BranchMember Find(System.Linq.Expressions.Expression<Func<BranchMember, bool>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<BranchMember> FindAll(System.Linq.Expressions.Expression<Func<BranchMember, bool>> conditions = null)
        {
            var oj = dbContext.FindAll(conditions);
            return oj;
        }

        public IQueryable<BranchMember> FindAllByPage<S>(System.Linq.Expressions.Expression<Func<BranchMember, bool>> conditions, System.Linq.Expressions.Expression<Func<BranchMember, S>> orderBy, int PageSize, int page)
        {
            var oj = dbContext.FindAllByPage(conditions, orderBy, PageSize, page);
            return oj;
        }

        public IQueryable<BranchMember> FindList<S>(System.Linq.Expressions.Expression<Func<BranchMember, bool>> conditions, System.Linq.Expressions.Expression<Func<BranchMember, S>> orderBy, int count)
        {
            var oj = dbContext.FindList(conditions, orderBy, count);
            return oj;
        }
        public IQueryable<BranchMember> FindAllByPage2<S>(System.Linq.Expressions.Expression<Func<BranchMember, bool>> conditions, System.Linq.Expressions.Expression<Func<BranchMember, S>> orderBy1, System.Linq.Expressions.Expression<Func<BranchMember, S>> orderBy2, int PageSize, int page)
        {
            var oj = dbContext.FindAllByPage2(conditions, orderBy1, orderBy2,PageSize, page);
            return oj;
        }
    }
}
