using AutoMapper;
using Quizify.Api.BL.Facades.IFacades;
using Quizify.Api.DAL.EF.Entities.Interfaces;
using Quizify.Api.DAL.EF.Repositories.Interfaces;
using Quizify.Common;

namespace Quizify.Api.BL.Facades
{
    public class BaseFacade<TEntity, TListModel, TDetailModel> : IFacade<TEntity, TListModel, TDetailModel>
        where TEntity : class, IEntity
        where TListModel : IRequiredId
        where TDetailModel : class, IRequiredId
    {
        protected readonly IApiRepository<TEntity> repository;
        protected readonly IMapper mapper;

        public BaseFacade(
            IApiRepository<TEntity> repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public List<TListModel> GetAll()
        {
            var entities = repository.GetAll();
            return mapper.Map<List<TListModel>>(entities);

        }

        public TDetailModel? GetById(Guid id)
        {
            var entity = repository.GetById(id);
            return mapper.Map<TDetailModel>(entity);
        }
        
        public Guid CreateOrUpdate(TDetailModel model)
        {
            return repository.Exists(model.Id)
                ? Update(model)!.Value 
                : Create(model);
        }
        
        public Guid Create(TDetailModel model)
        {
            var entity = mapper.Map<TEntity>(model);
            return repository.Insert(entity);
        }

        public virtual Guid? Update(TDetailModel model)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(Guid id)
        {
            repository.Remove(id);
        }
        public void Delete(TEntity entity)
        {
            repository.Remove(entity);
        }
    }
}
