using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace timeconsole
{
    class Query : Connz
    {

        public static void SqlUpdate(params string[] update)
        {
            var con = sqlConnz;

            string sql_up = "update anime_charac set anime_desc = '" + update[1] + "', anime_persona = '" + update[2] + "', anime_abilities = '" + update[3] + "' where anime_char = '" + update[0] + "'";
            SqlCommand cmd_up = new SqlCommand(sql_up, con);
            con.Open();
            cmd_up.ExecuteNonQuery();
            Console.WriteLine("updated");
            Console.Read();
            con.Close();
        }
        public static void SqlInsertCharacter(params string[] inserts)
           {
            var con = sqlConnz;

            string sql_mc = "insert into anime_charac(anime_fnum,anime_char,anime_desc,anime_persona,anime_abilities) values('{0}','{1}','{2}','{3}','{4}')";
            string _format = String.Format(sql_mc, inserts[0], inserts[1], inserts[2], inserts[3], inserts[4]);
            SqlCommand cmd_mc = new SqlCommand(_format, con);
            con.Open();
            cmd_mc.ExecuteNonQuery();
            Console.WriteLine("Added mc");
            Console.Read();
            con.Close();
        }

        public static void SqlInsertAnime(params string[] insertAnime)
        {
            var con = sqlConnz;
            string sql_add = "insert into anime_(anime_title,anime_description) values ('{0}', '{1}')";
            string format = String.Format(sql_add, insertAnime[0], insertAnime[1]);
            SqlCommand cmd_add = new SqlCommand(format, con);
            con.Open();
            cmd_add.ExecuteNonQuery();
            Console.WriteLine("Added");
            con.Close();
        }

        public static void SqlDelete(params string[] delete)
        {
            var con = sqlConnz;
            string sql_delchar = delete[0];
            SqlCommand cmd_del = new SqlCommand(sql_delchar, con);
            con.Open();
            cmd_del.ExecuteNonQuery();
            con.Close();
        }

        public static void view(params string[] views)
        {
            var con = sqlConnz;
            string sql = views[0];
            SqlCommand command = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int t = 0; t < reader.FieldCount; t++)
                {
                    Console.Write(views[1] + reader.GetValue(t));
                }
            }
            Console.WriteLine("\n");
        }
        public static void UnloopView(params string[] unloop)
        {
            var con = sqlConnz;
            string sql = unloop[0];
            SqlCommand command = new SqlCommand(sql, con);
            con.Open();
            string desc = "";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                desc = reader[0].ToString();
            }
            Console.WriteLine(desc + "\n");
            con.Close();
        }
    }
}
