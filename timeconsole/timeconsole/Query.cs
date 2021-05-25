using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace timeconsole
{
    class Query
    {
        public static void sqlUpdate()
        {

            var con = Connz.sqlConnz;

            string sql_up = "update anime_charac set anime_desc = '" + up_desc + "', anime_persona = '" + up_persona + "', anime_abilities = '" + up_abi + "' where anime_char = '" + up_mc + "'";
            SqlCommand cmd_up = new SqlCommand(sql_up, con);

            con.Open();
            cmd_up.ExecuteNonQuery();
            Console.WriteLine("update");
            Console.Read();
            con.Close();
        }
        string update_char { get; set; }
    }
}
