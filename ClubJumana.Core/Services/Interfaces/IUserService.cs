using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.Core.DTOs;
using ClubJumana.DataLayer.Entities.User;
using ClubJumana.DataLayer.Entities.Users;

namespace ClubJumana.Core.Services.Interfaces
{
   public interface IUserService
    {
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        int AddUser(User user);
         User LoginUser(LoginViewModel login);
        User GetUserByEmail(string email);
        User GetUserByActiveCode(string activeCode);
        void UpdateUser(User user);
        Invitation AllowRegister(string activeCode);

        int AddInvitation(Invitation invitation);
    }
}
