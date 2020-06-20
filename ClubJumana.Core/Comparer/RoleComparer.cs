using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities.Users;

namespace ClubJumana.Core.Comparer
{
    class RoleComparer : IEqualityComparer<Role>
    {
        public bool Equals(Role x, Role y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(Role obj)
        {
            throw new NotImplementedException();
        }
    }
}
