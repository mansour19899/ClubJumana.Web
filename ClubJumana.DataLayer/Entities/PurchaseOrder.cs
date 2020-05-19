using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClubJumana.DataLayer.Entities.Users;

namespace ClubJumana.DataLayer.Entities
{
    public class PurchaseOrder
    {

        public int Id { get; set; }
        public int PoNumber { get; set; }
        public int Asnumber { get; set; }
        public int Grnumber { get; set; }
        public int? Vendor_fk { get; set; }
        public string PoType { get; set; }
        public string Associate { get; set; }
        public string PoTerms { get; set; }
        public string Account { get; set; }
        public string FormSO { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? AsnDate { get; set; }
        public DateTime? GrnDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public DateTime? CreateOrder { get; set; }
        public DateTime? LastEditDate { get; set; }
        public decimal Freight { get; set; }
        public decimal DiscountPercent { get; set; }
        public string Percent { get; set; }
        public decimal DiscountDollers { get; set; }
        public decimal Insurance { get; set; }
        public decimal CustomsDuty { get; set; }
        public decimal Handling { get; set; }
        public decimal Forwarding { get; set; }
        public decimal LandTransport { get; set; }
        public decimal Others { get; set; }
        public decimal TotalCharges { get; set; }
        public decimal? PoSubtotal { get; set; }
        public decimal? AsnSubtotal { get; set; }
        public decimal? GrnSubtotal { get; set; }
        public decimal? PoTotal { get; set; }
        public decimal? AsnTotal { get; set; }
        public decimal? GrnTotal { get; set; }
        public  bool CreatedPO { get; set; }
        public  bool CreatedAsn { get; set; }
        public  bool CreatedGrn { get; set; }
        public int? ItemsPoCount { get; set; }
        public int? ItemsAsnCount { get; set; }
        public int? ItemsGrnCount { get; set; }
        public int? ToWarehouse_fk { get; set; }
        public int? FromWarehouse_fk { get; set; }
        public int? ApprovePoUser_fk{ get; set; }
        public int? ApproveAsnUser_fk { get; set; }
        public int? ApproveGrnUser_fk { get; set; }
        public byte[] RowVersion { get; set; }


        public ICollection<Item> Items { get; set; }
        public Vendor Vendor { get; set; }
        public Users.User UserCreatePo { get; set; }
        public Users.User UserCreateAsn { get; set; }
        public Users.User UserCreateGrn { get; set; }
        public Warehouse ToWarehouse { get; set; }
        public Warehouse FromWarehouse { get; set; }
    }
}
