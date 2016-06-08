using System;
using System.Linq;
using System.Linq.Expressions;

namespace Cibb.Core.DAL
{
    public interface IWebRepository<T>
    {
        T GetById(int id);
        void Update(T t);
        void Update();
        T Insert(T t);
        void Delete(int id);
        void Delete(T t);
        T Find(Expression<Func<T, bool>> conditions = null);
        IQueryable<T> FindAll(Expression<Func<T, bool>> conditions = null);
        IQueryable<T> FindAll<S>(bool isAsc, Expression<Func<T, S>> orderBy, Expression<Func<T, bool>> conditions = null);
        IQueryable<T> FindAllByPage<S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, int PageSize, int page);
        IQueryable<T> FindAllByPage<S>(Expression<Func<T, bool>> conditions, bool isAsc, Expression<Func<T, S>> orderBy, int PageSize, int page);
        IQueryable<T> FindAllByPage2<S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy1, Expression<Func<T, S>> orderBy2, int PageSize, int page);
        IQueryable<T> FindList<S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, int count);


        ///// <summary>
        ///// EF事务
        ///// </summary>
        //DbContextTransaction Transaction { get; set; }
        ///// <summary>
        ///// 事务提交结果
        ///// </summary>
        //bool Committed { get; set; }
        ///// <summary>
        ///// 提交事务
        ///// </summary>
        //void Commit();
        ///// <summary>
        ///// 回滚事务
        ///// </summary>
        //void Rollback();
    }
}
