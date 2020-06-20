using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities.Users;

namespace ClubJumana.Core.Comparer
{
    class UserRoleComparer : IEqualityComparer<UserRole>
    {
        public bool Equals(UserRole x, UserRole y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(UserRole obj)
        {
            throw new NotImplementedException();
        }
    }
}
