using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server.Db.Tables
{
    public class User
    {
        public int Id { get; private set; }
        public string Login { get; private set; }
        public string Password_hash { get; private set; }
        public bool IsAdmin { get; private set; }
    }
}
