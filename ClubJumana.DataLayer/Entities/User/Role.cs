using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities.Users
{
   public class Role
    {
        public Role()
        {
            
        }

        [Key]
        public int RoleId { get; set; }

        [Display(Name = "Role Tittle")]
        [Required(ErrorMessage = "Please Enter {0} ")]
        [MaxLength(200, ErrorMessage = "{0} Can't More  {1} ")]
        public string RoleTitle { get; set; }


        #region Relations

        public virtual List<UserRole> UserRoles { get; set; }


        #endregion
    }
}
