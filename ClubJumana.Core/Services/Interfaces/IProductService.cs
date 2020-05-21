using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Services.Interfaces
{
    interface IProductService
    {
        public int AddOrUpdateProduct(ProductMaster product);

        public IDictionary<string, string> GetAllInformationInventoryProduct(int Id);
    }
}
