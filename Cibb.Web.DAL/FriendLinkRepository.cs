using Cibb.Core.DAL;
using Cibb.Web.Contract;
using Cibb.Web.Contract.Model;
using System;
using System.Linq;

namespace Cibb.Web.DAL
{
    public class FriendLinkRepository : IRepository<FriendLink>
    {
        private WebDBContext dbContext = new WebDBContext();

        public FriendLink GetById(int id)
        {
            var oj = dbContext.Find<FriendLink>(id);
            return oj;
        }

        public void Save(FriendLink t)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public FriendLink Find(System.Linq.Expressions.Expression<Func<FriendLink, bool>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<FriendLink> FindAll(System.Linq.Expressions.Expression<Func<FriendLink, bool>> conditions = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<FriendLink> FindAllByPage<S>(System.Linq.Expressions.Expression<Func<FriendLink, bool>> conditions, System.Linq.Expressions.Expression<Func<FriendLink, S>> orderBy, int PageSize, int page)
        {
            throw new NotImplementedException();
        }

        public IQueryable<FriendLink> FindAllByPage2<S>(System.Linq.Expressions.Expression<Func<FriendLink, bool>> conditions, System.Linq.Expressions.Expression<Func<FriendLink, S>> orderBy1, System.Linq.Expressions.Expression<Func<FriendLink, S>> orderBy2, int PageSize, int page)
        {
            throw new NotImplementedException();
        }

        public IQueryable<FriendLink> FindList<S>(System.Linq.Expressions.Expression<Func<FriendLink, bool>> conditions, System.Linq.Expressions.Expression<Func<FriendLink, S>> orderBy, int count)
        {
            var oj = dbContext.FindList(conditions, orderBy, count);
            return oj;
        }
    }
}
