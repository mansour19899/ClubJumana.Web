using System;
using System.Collections.Generic;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
  public abstract class Variant
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public int ProductFK { get; set; }
        public int ColourFK { get; set; }
        public int BarcodeFK { get; set; }
        public int ProductTypeFK { get; set; }
        public Nullable<float> Price { get; set; }
        public Nullable<float> Width { get; set; }
        public Nullable<float> length { get; set; }
        public string Size { get; set; }
        public string Note { get; set; }
        public DateTime LastDateEdited { get; set; }
        public byte[] RowVersion { get; set; }

        public Product Product { get; set; }
        public  Colour Colour { get; set; }
        public Barcode Barcode { get; set; }
        public ProductType ProductType { get; set; }
        public virtual ICollection<Image> Images { get; set; }

    }
}
