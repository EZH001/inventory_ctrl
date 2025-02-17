using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Db.Tables
{
    public class Staff
    {
        public int Id { get; set; }
        public string last_name {  get; set; }
        public string first_name { get; set; }
        public string post {  get; set; }
    }
}
