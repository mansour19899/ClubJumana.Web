using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ClubJumana.DataLayer.Entities.Users;

namespace ClubJumana.DataLayer.Context
{
   public class JumanaContext:DbContext
    {

        public JumanaContext(DbContextOptions<JumanaContext> options):base(options)
        {
            
        }

        #region User

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        #endregion

    }
}
