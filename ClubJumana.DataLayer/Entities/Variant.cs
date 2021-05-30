using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
  public  class Variant
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string Sku { get; set; }
        public int? ProductFK { get; set; }
        public int? ColourFK { get; set; }
        public int? BarcodeFK { get; set; }
        public int? UOMFK { get; set; }
        public int? ProductTypeFK { get; set; }

        public decimal? FobPrice { get; set; }
        public decimal? WholesaleA { get; set; }
        public decimal? WholesaleB { get; set; }
        public decimal? PublishRate { get; set; }
        public decimal? RetailPrice { get; set; }
        public decimal? Width { get; set; }
        public decimal? length { get; set; }
        [StringLength(20)]
        public string Size { get; set; }
        public string Note { get; set; }
        [StringLength(100)]
        public string Data1 { get; set; }
        [StringLength(100)]
        public string Data2 { get; set; }
        [StringLength(100)]
        public string Data3 { get; set; }
        [StringLength(100)]
        public string Data4 { get; set; }
        [StringLength(100)]
        public string Data5 { get; set; }
        [StringLength(100)]
        public string Data6 { get; set; }

        public bool IsStar { get; set; } = false;
        public DateTime LastDateEdited { get; set; }
        [Timestamp]
        public DateTime RowVersion { get; set; }
        public Product Product { get; set; }
        public  Colour Colour { get; set; }
        public Barcode Barcode { get; set; }
        public ProductMaster ProductMaster { get; set; }
        public ProductType ProductType { get; set; }
        public UOM Uom { get; set; }
        public virtual ICollection<Image> Images { get; set; }

    }
}
