using System;
using System.Collections.Generic;
using System.Text;

namespace ClubJumana.DataLayer.Entities
{
   public class Refund
    {
        
        public int Id { get; set; }
        public DateTime? RefundDate { get; set; }
        public int? RefundNumber { get; set; }
        public int SaleOrder_fk { get; set; }
        public int? WarehouseId { get; set; }

        private decimal _refundTotalPrice;

        public decimal RefundTotalPrice
        {
            get { return _refundTotalPrice; }
            set { _refundTotalPrice = value; }
        }

        private decimal _subtotalPrice;

        public decimal SubtotalPrice
        {
            get { return _subtotalPrice; }
            set
            {
                _subtotalPrice = value;
                _tax = Math.Round(_subtotalPrice * 0.13m, 2, MidpointRounding.ToEven);
                _refundTotalPrice = _subtotalPrice + _tax;
            }

        }

        private decimal _tax;

        public decimal Tax
        {
            get { return _tax; }
            set { _tax = value; }
        }

        public SaleOrder SaleOrder { get; set; }
        public ICollection<RefundItem> RefundItems { get; set; }
        public byte[] RowVersion { get; set; }

    }
}
