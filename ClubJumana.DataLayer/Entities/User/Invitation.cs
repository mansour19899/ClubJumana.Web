using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities.User
{
    public class Invitation
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [MaxLength(200, ErrorMessage = "{0} Can't More {1}")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [MaxLength(200, ErrorMessage = "{0} Can't More {1}")]
        [EmailAddress(ErrorMessage = "Email is not Valid")]
        public string Email { get; set; }

        [Display(Name = "Active Code")]
        [MaxLength(50, ErrorMessage = "{0} Can't More {1}")]
        public string ActiveCode { get; set; }

        public int UserSendInvitation_fk { get; set; }
        public int? UserRegisterWithInvitation_fk { get; set; }

        public Users.User UserSendInvitation { get; set; }
        public Users.User UserRegisterWithInvitation { get; set; }

        public DateTime ExpireInvitationDate { get; set; }
        public DateTime SendInvitationDate { get; set; }

    }
}
