using Quizify.Api.DAL.EF.Entities.Interfaces;
using Quizify.Common;
using Quizify.Common.BL.Facades;

namespace Quizify.Api.BL.Facades.IFacades
{
    public interface IFacade<TEntity, TListModel, TDetailModel> : IAppFacade
        where TEntity : class, IEntity
        where TListModel : IRequiredId
        where TDetailModel : class, IRequiredId

    {
        List<TListModel> GetAll();
        TDetailModel? GetById(Guid id);
        Guid CreateOrUpdate(TDetailModel model);
        Guid Create(TDetailModel model);
        Guid? Update(TDetailModel model);
        void Delete(Guid id);
        void Delete(TEntity id);
        
    }
}
