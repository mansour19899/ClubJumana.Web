using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities.User;

namespace ClubJumana.Core.Comparer
{
    class InvitationComparer : IEqualityComparer<Invitation>
    {
        public bool Equals(Invitation x, Invitation y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }



            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }


            return x.Id == y.Id && x.Name == y.Name && x.Email == y.Email && x.ActiveCode == y.ActiveCode &&
                   x.UserSendInvitation_fk == y.UserSendInvitation_fk && x.UserRegisterWithInvitation_fk == y.UserRegisterWithInvitation_fk && x.ExpireInvitationDate == y.ExpireInvitationDate &&
                   x.SendInvitationDate == y.SendInvitationDate ;
        }

        public int GetHashCode(Invitation obj)
        {
            throw new NotImplementedException();
        }
    }
}
