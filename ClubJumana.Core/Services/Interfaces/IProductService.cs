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

        public ProductMaster GetProductMasterById(int Id);
        public ProductMaster GetProductMasterByUPC(string upc);
        public int AddInner(Inner inner);
        public int AddMaster(MasterCarton masterCarton);
        public bool CheckInnerITF(string itf14);
        public bool CheckMasterCartonITF(string itf14);
        public Inner GetInnerByITF(string itf14);
        public MasterCarton GetMasterByITF(string itf14);
    }
}
