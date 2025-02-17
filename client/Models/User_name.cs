using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Models
{
    public class User_name
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public string Last_name { get; set;}
        public string First_name { get; set;}
        public virtual User User { get; set; }
    }
}
