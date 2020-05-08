using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities.Users
{
    public class User
    {
        public User()
        {
            
        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [MaxLength(200, ErrorMessage = "{0} Can't More {1}")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [MaxLength(200, ErrorMessage = "{0} Can't More {1}")]
        [EmailAddress(ErrorMessage = "Email is not Valid")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = " Please Enter {0} ")]
        [MaxLength(200, ErrorMessage = "{0} Can't More  {1}")]
        public string Password { get; set; }

        [Display(Name = "Active Code")]
        [MaxLength(50, ErrorMessage = "{0} Can't More {1}")]
        public string ActiveCode { get; set; }

        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }

        [Display(Name = "Avatar")]
        [MaxLength(200, ErrorMessage = "{0} Can't More  {1}")]
        public string UserAvatar { get; set; }

        [Display(Name = "Register Date")]
        public DateTime RegisterDate { get; set; }


        #region Relations

        public virtual List<UserRole> UserRoles { get; set; }


        public ICollection<PurchaseOrder> CreatePo { get; set; }
        public ICollection<PurchaseOrder> CreateAsn { get; set; }
        public ICollection<PurchaseOrder> CreateGrn { get; set; }
        public ICollection<SaleOrder> SaleOrders { get; set; }
        public ICollection<Customer> Customers { get; set; }

        #endregion

    }
}
