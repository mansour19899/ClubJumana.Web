using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubJumana.DataLayer.Entities
{
    public class ProductInventoryWarehouse
    {
        public int Id { get; set; }
        public int? ProductMaster_fk { get; set; }
        public int? Warehouse_fk { get; set; }
        public int? Inventory { get; set; }
        public int? Income { get; set; }
        public int? OutCome { get; set; }
        public int? OnTheWayInventory { get; set; }
        public string Aile { get; set; }
        public string Bin { get; set; }

        public ProductMaster ProductMaster { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
