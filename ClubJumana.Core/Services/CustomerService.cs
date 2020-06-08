using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.Core.Services.Interfaces;
using ClubJumana.DataLayer.Context;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Services
{
  public  class CustomerService : ICustomerService
    {
        private JummanaContext _context;
        public int AddAndUpdateCustomer(Customer customer)
        {
            if (customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                _context.Update(customer);
            }

            _context.SaveChanges();
            return 1;
        }
    }
}
