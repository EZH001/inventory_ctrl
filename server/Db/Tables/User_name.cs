using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Db.Tables
{
    public class User_name
    {
        public int Id { get; private set; }
        public int User_id { get; private set; }
        public string Last_name { get; private set;}
        public string First_name { get; private set;}
        public virtual User User { get; private set; }
    }
}
