﻿using Quizify.Api.DAL.EF.Entities;
using Quizify.Common.Models;

namespace Quizify.Api.BL.Facades.IFacades
{
    public interface IUserFacade : IFacade<UserEntity, UserListModel, UserDetailModel>
    {
        public List<UserListModel> GetUsersByName(string? userName);
        public Guid Login (string? userName);
        
        
        public UserDetailModel? Profile();

        public Guid? SubmitQuiz(UserDetailModel model,Guid quizId);
    }
}
