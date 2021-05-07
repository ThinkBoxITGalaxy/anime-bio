using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace timeconsole
{
    class Connz
    {
        public static SqlConnection sqlConnz
        {
            get
            {
                string constring = "Server=DESKTOP-UJBAK2U; user id=anime; password=123; database=anime_db";
                return new SqlConnection(constring);
            }
        }
    }
}
