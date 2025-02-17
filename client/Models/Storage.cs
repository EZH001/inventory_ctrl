using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int category_id { get; set; }
        public virtual Category Category { get; set; }
    }
}
