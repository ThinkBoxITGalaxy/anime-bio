using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace timeconsole
{
    class Query
    {
        public static void SqlUpdate(string mc, string desc, string persona, string abi)
        {
            var con = Connz.sqlConnz;

            string sql_up = "update anime_charac set anime_desc = '" + desc + "', anime_persona = '" + persona + "', anime_abilities = '" + abi + "' where anime_char = '" + mc + "'";
            SqlCommand cmd_up = new SqlCommand(sql_up, con);

            con.Open();
            cmd_up.ExecuteNonQuery();
            Console.WriteLine("update");
            Console.Read();
            con.Close();
        }
        public static void SqlInsert(string aniTitle, string mainChar, string mainDesc, string mainPersona, string mainSkills)
        {
            var con = Connz.sqlConnz;

            string sql_mc = "insert into anime_charac(anime_fnum,anime_char,anime_desc,anime_persona,anime_abilities) values('{0}','{1}','{2}','{3}','{4}')";
            string _format = String.Format(sql_mc, aniTitle, mainChar, mainDesc, mainPersona, mainSkills);
            SqlCommand cmd_mc = new SqlCommand(_format, con);
            con.Open();
            cmd_mc.ExecuteNonQuery();
            Console.WriteLine("Added mc");
            Console.Read();
            con.Close();
        }
    }
}
