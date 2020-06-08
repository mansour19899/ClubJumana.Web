using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Services.Interfaces
{
    interface ICustomerService
    {
        public int AddAndUpdateCustomer(Customer customer);
    }
}
