using Quizify.Common;
using Quizify.Common.BL.Facades;

namespace Quizify.Web.BL.Facades;

public interface IFacade<TListModel, TDetailModel> : IAppFacade
   where TListModel : class,IRequiredId
   where TDetailModel : class, IRequiredId
{
   Task<List<TListModel>> GetAllAsync();
   Task<TDetailModel> GetByIdAsync(Guid id);
   Task<Guid> CreateAsync(TDetailModel data);
   
   Task<Guid?> UpdateAsync(TDetailModel data);
   Task DeleteAsync(Guid id);
}