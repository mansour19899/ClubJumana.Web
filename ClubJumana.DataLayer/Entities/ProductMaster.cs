using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubJumana.DataLayer.Entities
{
    public class ProductMaster
    {

        public int Id { get; set; }

        public string Name { get; set; }
        public int VendorCode { get; set; }
        [StringLength(16)]
        public string StyleNumber { get; set; }
        public int? VariantFK { get; set; }
        public int? UOMFK { get; set; } 
        [StringLength(16)]
        public string SKU { get; set; }
        public string UPC { get; set; }

        [StringLength(15)]
        public string Size { get; set; }
        public string Color { get; set; }
        public string MadeIn { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<decimal> FobPrice { get; set; }
        public Nullable<decimal> RetailPrice { get; set; }
        public Nullable<decimal> WholesalePrice { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public DateTime? SaleStartDate{ get; set; }
        public DateTime? SaleEndDate { get; set; }
        public string Margin { get; set; }
        public int PackCount { get; set; }
        public int StockOnHand { get; set; }
        public int GoodsReserved { get; set; }
        public int Transit { get; set; }
        public int? RefundQuantity { get; set; } = 0;
        public int TempBalance => StockOnHand - GoodsReserved;
        public DateTime LastUpdateInventory { get; set; }
        public int Income { get; set; }
        public int Outcome { get; set; }

        private string _imageName;
        public string Image
        {
            get=>string.IsNullOrEmpty(_imageName)? "NoImage.jpeg" : _imageName;
            set=>_imageName=value;
        }
        public Nullable<bool> Active { get; set; }
        public bool IsWholesale { get; set; } = false;
        public bool IsRetail { get; set; } = false;
        public bool Taxable { get; set; } = false;
        public string Bundle { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Note { get; set; }
        [Timestamp]
        public DateTime RowVersion { get; set; }
        public ICollection<Item> Items { get; set; }
        public ICollection<SoItem> SoItems { get; set; }
        public ICollection<RefundItem> RefundItems { get; set; }
        public ICollection<ProductInventoryWarehouse> ProductInventoryWarehouses { get; set; }
        public ICollection<Inner> Inners { get; set; }
        public ICollection<InventoryReport> InventoryReports { get; set; }
        public Variant Variant { get; set; }
        public UOM Uom { get; set; }

    }
}
