using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Cibb.Core.DAL
{
    public class DbContextBase : DbContext
    {
        public DbContextBase(string connectionString)
            : base(connectionString)
        {
        }
        public void Update<T>(T entity) where T : class
        {
            var set = this.Set<T>();
            set.Attach(entity);
            this.Entry<T>(entity).State = EntityState.Modified;
            this.SaveChanges();
        }

        public void Update()
        {
            this.SaveChanges();
        }

        public T Insert<T>(T entity) where T : class
        {
            this.Set<T>().Add(entity);
            this.SaveChanges();
            return entity;
        }
        public void Delete<T>(T entity) where T : class
        {
            this.Entry<T>(entity).State = EntityState.Deleted;
            this.SaveChanges();
        }

        public T Find<T>(params object[] keyValues) where T : class
        {
            return this.Set<T>().Find(keyValues);
        }
        public IQueryable<T> FindAll<T>(Expression<Func<T, bool>> conditions = null) where T : class
        {
            if (conditions == null)
                return this.Set<T>();
            else
                return this.Set<T>().Where(conditions);
        }

        public IQueryable<T> FindAll<T,S>(bool isAsc, Expression<Func<T, S>> orderBy,Expression<Func<T, bool>> conditions = null) where T : class
        {
            var a = Set<T>();
            if (isAsc){
                if (conditions == null)
                    return a.OrderBy(orderBy);
                else
                    return a.Where(conditions).OrderBy(orderBy);
            }
            else {
                if (conditions == null)
                    return a.OrderByDescending(orderBy);
                else
                    return a.Where(conditions).OrderByDescending(orderBy);
            }
        }
        public IQueryable<T> FindAllByPage<T, S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, int PageSize, int page) where T : class
        {
            var queryList = conditions == null ? this.Set<T>() : this.Set<T>().Where(conditions);
            return queryList.OrderByDescending(orderBy).Skip((page - 1) * PageSize).Take(PageSize);
        }

        public IQueryable<T> FindAllByPage<T, S>(Expression<Func<T, bool>> conditions, bool isAsc,Expression<Func<T, S>> orderBy, int PageSize, int page) where T : class
        {
            var queryList = conditions == null ? this.Set<T>() : this.Set<T>().Where(conditions);
            if(isAsc)
                return queryList.OrderBy(orderBy).Skip((page - 1) * PageSize).Take(PageSize);
            return queryList.OrderByDescending(orderBy).Skip((page - 1) * PageSize).Take(PageSize);
        }

        public IQueryable<T> FindAllByPage2<T, S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy1, Expression<Func<T, S>> orderBy2, int PageSize, int page) where T : class
        {
            var queryList = conditions == null ? this.Set<T>() : this.Set<T>().Where(conditions);
            return queryList.OrderBy(orderBy1).ThenByDescending(orderBy2).Skip((page - 1) * PageSize).Take(PageSize);
        }

        public IQueryable<T> FindList<T, S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, int count) where T : class
        {
            var queryList = conditions == null ? this.Set<T>() : this.Set<T>().Where(conditions);
            if (count == 0)
                return queryList.OrderByDescending(orderBy);
            else
                return queryList.OrderByDescending(orderBy).Take(count);
        }
    }
}
