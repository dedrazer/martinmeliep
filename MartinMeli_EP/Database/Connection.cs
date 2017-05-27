using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Connection
    {
        public martinmeli_epEntities db { get; set; }

        public Connection()
        {
            db = new martinmeli_epEntities();
        }
    }
}
