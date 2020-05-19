using System;
using System.Collections.Generic;
using System.Text;
using ClubJumana.DataLayer.Entities;

namespace ClubJumana.Core.Services.Interfaces
{
    interface IProductService
    {
        public int AddOrUpdateProduct(ProductMaster product);

        public List<string> GetAllInformationInventoryProduct(int Id);
    }
}
