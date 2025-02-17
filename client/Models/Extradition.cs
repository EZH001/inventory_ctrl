using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Models
{
    public class Extradition
    {
        public int Id { get; set; }
        public int product_id { get; set; }
        public decimal quantity { get; set; }
        public DateTime shipment_date { get; set; }
        public int staff_id { get; set; }
        public virtual Product Product { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
