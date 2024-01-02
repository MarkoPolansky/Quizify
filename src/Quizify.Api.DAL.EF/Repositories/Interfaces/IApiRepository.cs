using Microsoft.EntityFrameworkCore;
using Quizify.Api.DAL.EF.Entities.Interfaces;

namespace Quizify.Api.DAL.EF.Repositories.Interfaces
{
    public interface IApiRepository<TEntity>
        where TEntity : IEntity
    {
        IList<TEntity> GetAll();
        TEntity? GetById(Guid id);
        Guid Insert(TEntity entity);
        Guid? Update(TEntity entity);
        void Remove(Guid id);
        bool Exists(Guid id);

        void Remove(TEntity entity);

        IQueryable<TEntity> Get();
    }
}
