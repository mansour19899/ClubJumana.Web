using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubJumana.DataLayer.Entities
{
    public class Item
    {

        public int Id { get; set; }
        public int Po_fk { get; set; }
        public int ProductMaster_fk { get; set; }
        public int PreviousQuantity { get; set; }
        public int CurrentQuantity { get; set; }
        public int PoQuantity { get; set; }
        public int AsnQuantity { get; set; }
        public int GrnQuantity { get; set; }
        public decimal PoPrice { get; set; }
        public decimal AsnPrice { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public decimal TotalItemPrice { get; set; }
        public decimal PoItemsPrice { get; set; }
        public decimal AsnItemsPrice { get; set; }
        public int? Diffrent { get; set; }
        public bool? Alert { get; set; }
        public string Note { get; set; }
        public bool? Checked { get; set; }
        public byte[] RowVersion { get; set; }

        public ProductMaster ProductMaster { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }

    }
}
