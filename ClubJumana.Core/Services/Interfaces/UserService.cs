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
using ClubJumana.DataLayer.Entities.User;

namespace ClubJumana.Core.Services.Interfaces
{
    public class UserService : IUserService
    {
        private JummanaContext _context;

        public UserService()
        {
            _context = new JummanaContext();
        }
        public Invitation AllowRegister(string activeCode)
        {
            var invitation = _context.invitations.SingleOrDefault(u => u.ActiveCode == activeCode);
            return invitation ?? new Invitation(){Id = 0};
        }

        public List<User> AllSalesPeople()
        {
            return _context.users.ToList();
        }

        public int AddInvitation(Invitation invitation)
        {
            _context.invitations.Add(invitation);
            _context.SaveChanges();
            return invitation.Id;
        }

        public int AddUser(User user)
        {
            _context.users.Add(user);
            var invitation = _context.invitations.SingleOrDefault(p => p.ActiveCode == user.ActiveCode);
            if (invitation == null)
                return -2;

            _context.SaveChanges();
            invitation.ActiveCode = $"The user Registered88";
            invitation.UserRegisterWithInvitation_fk = user.Id;
            _context.SaveChanges();
            return user.Id;
        }

        public User GetUserByActiveCode(string activeCode)
        {
            return _context.users.SingleOrDefault(u => u.ActiveCode == activeCode);
        }

        public User GetUserByEmail(string email)
        {
            return _context.users.SingleOrDefault(u => u.Email == email);
        }

        public bool IsExistEmail(string email)
        {
            return _context.users.Any(p => p.Email == email);
        }

        public bool IsExistUserName(string userName)
        {
            return _context.users.Any(p => p.UserName == userName);
        }

        public User LoginUser(LoginViewModel login)
        {
            string hashPasword = PasswordHelper.EncodePasswordMd5(login.Password);
            string emailUser = FixedText.FixEmail(login.UserNameEmail);
            var user= _context.users.SingleOrDefault(u => (u.Email == emailUser||u.UserName==emailUser)&&u.Password==hashPasword);
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
            _context.users.Update(user);
            _context.SaveChanges();
        }

        public User LoginUser()
        {
            return _context.users.FirstOrDefault();
        }
    }
}
