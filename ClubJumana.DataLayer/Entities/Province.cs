using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubJumana.DataLayer.Entities
{
    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? HST { get; set; }
        public decimal? GST { get; set; }
        public decimal? QST { get; set; }
        public bool Active { get; set; }

        public ICollection<SaleOrder> SaleOrders { get; set; }
    }
}
