﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TesteBackEnd.Business.Interfaces.Repository;
using TesteBackEnd.Core.Models.Base;
using TesteBackEnd.Data.Context;

namespace TesteBackEnd.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly TesteDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(TesteDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task Create(TEntity entity)
        {
            Db.Add(entity);
            await SaveChanges();
        }

        public async Task Update(TEntity entity)
        {
            Db.Update(entity);
            await SaveChanges();
        }

        public async Task Delete(Guid id)
        {
            Db.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public virtual async Task<TEntity> ReadById(Guid id)
        {
            return await DbSet.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> ReadExpression(Expression<Func<TEntity, bool>> predicateExpression)
        {
            return await DbSet.AsNoTracking().Where(predicateExpression).ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> Read()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public async void Dispose()
        {
            Db?.Dispose();
        }
    }
}
