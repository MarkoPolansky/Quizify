﻿using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Namotion.Reflection;
using Quizify.Api.DAL.Common.Tests.Seeds;
using Quizify.Api.DAL.EF.Entities;
using Quizify.Common.Models;
using Xunit;

namespace Quizify.Api.App.EndToEndTests
{
    public class UserControllerTests : BaseTest, IAsyncDisposable
    {
   

        public UserControllerTests(): base(true){}

        [Fact]
        public async Task GetAllUsers_Returns_At_Least_One_User()
        {
            var response = await client.Value.GetAsync("/api/user");

            response.EnsureSuccessStatusCode();

            var users = await response.Content.ReadFromJsonAsync<ICollection<UserListModel>>();
            Assert.NotNull(users);
            Assert.NotEmpty(users);
        }

        [Fact]
        public async Task CreateAndDeleteUser_Test()
        {   
            // Arrange
            Guid userId = Guid.NewGuid();
            string requestUri = string.Format("/api/user/{0}", userId);

            var user = new UserDetailModel
            {
                Id = userId,
                Name = "Test",
            };
            // Act 1 - Create
            var response = await client.Value.PostAsJsonAsync("/api/user", user);
            response.EnsureSuccessStatusCode();

            response = await client.Value.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            var userUrl = await response.Content.ReadFromJsonAsync<UserDetailModel>();
            // Assert 1
            Assert.NotNull(userUrl);

            // Act 2 - Delete
            response = await client.Value.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();

            response = await client.Value.GetAsync("/api/user");
            response.EnsureSuccessStatusCode();


            var users = await response.Content.ReadFromJsonAsync<ICollection<UserListModel>>();
            // Assert 2
            Assert.NotNull(users);
            Assert.Empty(users.Where(usr => usr.Id == userId));
        }

        [Fact]
        public async Task UpdateUser_Test()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            string requestUri = string.Format("{0}/{1}", baseUserUrl, userId);
            var user = new UserDetailModel
            {
                Id = userId,
                Name = "Test",
            };
            var userUpdated = new UserDetailModel
            {
                Id = userId,
                Name = "Thomas",
            };

            var response = await client.Value.PostAsJsonAsync(baseUserUrl, user);
            response.EnsureSuccessStatusCode();
            // Act
            response = await client.Value.PutAsJsonAsync(baseUserUrl, userUpdated);
            response.EnsureSuccessStatusCode();
            // Assert
            response = await client.Value.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();

            UserDetailModel? storedUser = await response.Content.ReadFromJsonAsync<UserDetailModel>();
            Assert.NotNull(storedUser);
            Assert.Equal(storedUser.Id, userUpdated.Id);
            Assert.Equal(storedUser.Name, userUpdated.Name);
            
            // Deleting
            response = await client.Value.DeleteAsync(requestUri);
            response.EnsureSuccessStatusCode();
        }

        public async ValueTask DisposeAsync()
        {
            await application.DisposeAsync();
        }
        
       
       
    }
}
