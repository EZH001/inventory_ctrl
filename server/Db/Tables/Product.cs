using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Db.Tables
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Category_Id{ get; set; }
        public decimal QuantityInStock{  get; set; }
        public int StorageUnitsId{  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int User_id { get; set; }
        public int storage_id{  get; set; }
        public int supplier_id{  get; set; }
        public virtual StorageUnit StorageUnit { get; set; }
        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
        public virtual Storage Storage { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
