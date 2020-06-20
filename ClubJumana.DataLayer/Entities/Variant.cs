using System;
using System.Collections.Generic;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
  public  class Variant
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public int? ProductFK { get; set; }
        public int? ColourFK { get; set; }
        public int? BarcodeFK { get; set; }
        public int? ProductTypeFK { get; set; }
        public decimal? FobPrice { get; set; }
        public decimal? WholesaleA { get; set; }
        public decimal? WholesaleB { get; set; }
        public decimal? RetailPrice { get; set; }
        public decimal? Width { get; set; }
        public decimal? length { get; set; }
        public string Size { get; set; }
        public string Note { get; set; }
        public string Data1 { get; set; }
        public string Data2 { get; set; }
        public string Data3 { get; set; }
        public string Data4 { get; set; }
        public string Data5 { get; set; }
        public string Data6 { get; set; }
        public DateTime LastDateEdited { get; set; }
        public byte[] RowVersion { get; set; }
        public Product Product { get; set; }
        public  Colour Colour { get; set; }
        public Barcode Barcode { get; set; }
        public ProductType ProductType { get; set; }
        public virtual ICollection<Image> Images { get; set; }

    }
}
