using ClubJumana.DataLayer.Entities.Users;
using ClubJumana.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClubJumana.Core.Convertors;
using Microsoft.EntityFrameworkCore.Internal;
using ClubJumana.Core.DTOs;
using ClubJumana.Core.Security;

namespace ClubJumana.Core.Services.Interfaces
{
    public class UserService : IUserService
    {
        private JummanaContext _context;

        public UserService(JummanaContext context)
        {
            _context = context;
        }
        public bool ActiveAccount(string activeCode)
        {
            throw new NotImplementedException();
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.Id;
        }

        public User GetUserByActiveCode(string activeCode)
        {
            throw new NotImplementedException();
        }

        public User GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(p => p.Email == email);
        }

        public bool IsExistUserName(string userName)
        {
            return _context.Users.Any(p => p.UserName == userName);
        }

        public User LoginUser(LoginViewModel login)
        {
            string hashPasword = PasswordHelper.EncodePasswordMd5(login.Password);
            string emailUser = FixedText.FixEmail(login.UserNameEmail);
            var user= _context.Users.SingleOrDefault(u => (u.Email == emailUser||u.UserName==emailUser)&&u.Password==hashPasword);
            return user;
            //if (user == null)
            //{
            //    return _context.Users.SingleOrDefault(u => u.UserName == emailUser && u.Password == hashPasword);
            //}
            //else
            //{
            //    return user;
            //}
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
