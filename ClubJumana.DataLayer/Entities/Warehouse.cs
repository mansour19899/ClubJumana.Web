using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubJumana.DataLayer.Entities
{
    public class Warehouse
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string Note { get; set; }
        public ICollection<PurchaseOrder> PoToWareHose { get; set; }
        public ICollection<PurchaseOrder> POFromWarehouse { get; set; }
        public ICollection<ProductInventoryWarehouse> ProductInventoryWarehouses { get; set; }
        public ICollection<SaleOrder> SaleOrders { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
