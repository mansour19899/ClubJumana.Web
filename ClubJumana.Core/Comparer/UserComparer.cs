using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities.Users;

namespace ClubJumana.Core.Comparer
{
    class UserComparer : IEqualityComparer<User>
    {
        public bool Equals(User x, User y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(User obj)
        {
            throw new NotImplementedException();
        }
    }
}
