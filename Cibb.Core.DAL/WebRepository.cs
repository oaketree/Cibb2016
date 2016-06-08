using System;
using System.Linq;
using System.Linq.Expressions;
namespace Cibb.Core.DAL
{
    public class WebRepository<T, D> : IWebRepository<T>
        where T : class
        where D : DbContextBase, new()
    {
        //private WebDBContext dbContext = new WebDBContext();
        private D dbContext = new D();

        ///// <summary>
        ///// 事务
        ///// </summary>
        //private DbContextTransaction _transaction = null;

        //public DbContextTransaction Transaction
        //{
        //    get
        //    {
        //        if (this._transaction == null)
        //        {
        //            this._transaction = dbContext.Database.BeginTransaction();
        //        }
        //        return this._transaction;
        //    }
        //    set { this._transaction = value; }
        //}

        ///// <summary>
        /////  事务状态
        ///// </summary>
        //public bool Committed { get; set; }
        ///// <summary>
        ///// 异步锁定
        ///// </summary>
        //private readonly object sync = new object();
        //public void Commit()
        //{
        //    if (!Committed)
        //    {
        //        lock (sync)
        //        {
        //            if (this._transaction != null)
        //                _transaction.Commit();
        //        }
        //        Committed = true;
        //    }
        //}

        //public void Rollback()
        //{
        //    Committed = false;
        //    if (this._transaction != null)
        //        this._transaction.Rollback();
        //}

        public T GetById(int id)
        {
            var oj = dbContext.Find<T>(id);
            return oj;
        }
        public void Update(T entity)
        {
            dbContext.Update(entity);
        }

        public void Update()
        {
            dbContext.Update();
        }
        public T Insert(T entity)
        {
            return dbContext.Insert(entity);
        }
        public void Delete(int id)
        {
            var oj = GetById(id);
            dbContext.Delete(oj);
        }
        public void Delete(T entity)
        {
            dbContext.Delete(entity);
        }

        public T Find(Expression<Func<T, bool>> conditions = null)
        {
            var oj = dbContext.FindAll(conditions).FirstOrDefault();
            return oj;
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> conditions = null)
        {
            var oj = dbContext.FindAll(conditions);
            return oj;
        }

        public IQueryable<T> FindAll<S>(bool isAsc, Expression<Func<T, S>> orderBy, Expression<Func<T, bool>> conditions = null)
        {
            var oj = dbContext.FindAll(isAsc,orderBy,conditions);
            return oj;
        }


        public IQueryable<T> FindAllByPage<S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, int PageSize, int page)
        {
            var oj = dbContext.FindAllByPage(conditions, orderBy, PageSize, page);
            return oj;
        }

        public IQueryable<T> FindAllByPage<S>(Expression<Func<T, bool>> conditions, bool isAsc,Expression<Func<T, S>> orderBy, int PageSize, int page)
        {
            var oj = dbContext.FindAllByPage(conditions, isAsc,orderBy, PageSize, page);
            return oj;
        }
        public IQueryable<T> FindAllByPage2<S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy1, Expression<Func<T, S>> orderBy2, int PageSize, int page)
        {
            var oj = dbContext.FindAllByPage2(conditions, orderBy1, orderBy2, PageSize, page);
            return oj;
        }
        public IQueryable<T> FindList<S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, int count)
        {
            var oj = dbContext.FindList(conditions, orderBy, count);
            return oj;
        }
    }
}
