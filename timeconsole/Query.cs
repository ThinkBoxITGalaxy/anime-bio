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
        public static void SqlUpdate(params string[] update)
        {                                                      
            var con = Connz.sqlConnz;

            string sql_up = "update anime_charac set anime_desc = '" + update[1] + "', anime_persona = '" + update[2] + "', anime_abilities = '" + update[3] + "' where anime_char = '" + update[0] + "'";
            SqlCommand cmd_up = new SqlCommand(sql_up, con);

            con.Open();
            cmd_up.ExecuteNonQuery();
            Console.WriteLine("update");
            Console.Read();
            con.Close();
        }
        public static void SqlInsert(params string[] inserts)
           {
            var con = Connz.sqlConnz;

            string sql_mc = "insert into anime_charac(anime_fnum,anime_char,anime_desc,anime_persona,anime_abilities) values('{0}','{1}','{2}','{3}','{4}')";
            string _format = String.Format(sql_mc, inserts[0], inserts[1], inserts[2], inserts[3], inserts[4]);
            SqlCommand cmd_mc = new SqlCommand(_format, con);
            con.Open();
            cmd_mc.ExecuteNonQuery();
            Console.WriteLine("Added mc");
            Console.Read();
            con.Close();
        }
        public static void view()
        {
            var con = Connz.sqlConnz;

            string sql = "select anime_title from anime_";
            SqlCommand command = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int t = 0; t < reader.FieldCount; t++)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(reader.GetValue(t) + " || ");
                }
            }
            Console.WriteLine("\n");
        }
    }
}
