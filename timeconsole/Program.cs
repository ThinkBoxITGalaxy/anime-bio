﻿using System;
using timeconsole;
using System.Data.SqlClient;
using System.Threading;
using System.Text;
using System.IO;
// anime database - username: anime passwor: 123


/* 
  For github links:
  stackoverflow.com/questions/23401652/fatal-the-current-branch-master-has-no-upstream-branch
  docs.github.com/en/github/importing-your-projects-to-github/adding-an-existing-project-to-github-using-the-command-line

 */
namespace ConsoleAnimations
{
    class MainClass
    {

        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Anime Wikipedia\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Search: ");
            string search = Console.ReadLine().ToLower();
            if (string.IsNullOrEmpty(search))
            {
                Console.WriteLine("Please input your favorite anime");
                return;
            }
            else if (search == "-insert")
            {
                Console.Clear();
                _add();
            }
            else
            {
                datas(search);
            }

            //  choosef();
        }
        static void datas(string cc)
        {
            var con = Connz.sqlConnz;

            string sql = @"SELECT anime_charac.anime_char 
                           FROM anime_ INNER JOIN anime_charac 
                           ON anime_.anime_title = anime_charac.anime_fnum  
                           Where anime_charac.anime_fnum = '" + cc + "'";

            string sql_desc = @"SELECT anime_description FROM anime_ WHERE anime_title = '" + cc + "'";

            SqlCommand command = new SqlCommand(sql, con);
            SqlCommand command_desc = new SqlCommand(sql_desc, con);

            con.Open();
            string desc = "";
            SqlDataReader reader_desc = command_desc.ExecuteReader();
            while (reader_desc.Read())
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                desc = reader_desc[0].ToString();
            }

            Console.WriteLine(desc + "\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Characters:");
            con.Close();

            // View characters
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(reader.GetValue(i));
                }
            }
            Console.WriteLine();
            con.Close();

            // search char
            bool w = true;
            while (w)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Search characters: ");
                string char_ = Console.ReadLine();
                string sql_search = @"SELECT anime_charac.anime_desc,anime_charac.anime_persona,anime_abilities
                           FROM anime_ INNER JOIN anime_charac 
                           ON anime_.anime_title = anime_charac.anime_fnum  
                           Where anime_charac.anime_char = '" + char_ + "'";

                SqlCommand command_search = new SqlCommand(sql_search, con);
                con.Open();
                SqlDataReader reader_search = command_search.ExecuteReader();
                while (reader_search.Read())
                {
                    for (int t = 0; t < reader_search.FieldCount; t++)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(reader_search.GetValue(t));
                        Console.WriteLine();
                    }
                }

                con.Close();
                if (char_ == "exit")
                {
                    w = false;
                }
                if (char_ == "help")
                {
                    Console.WriteLine("exit - Terminate the window\nclear - Back to main page");
                }
                if (char_ == "clear")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Anime Wikipedia\n");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Search: ");
                }
            }
        }
        static void _add()
        {
            var con = Connz.sqlConnz;

            Console.WriteLine("Anime Database");

            string sql = "select anime_title from anime_";
            SqlCommand command = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                for (int t = 0; t < reader.FieldCount; t++)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(reader.GetValue(t));
                }
            }
            con.Close();

            string _command = Console.ReadLine();
            switch (_command)
            {
                case "-addtitle":
                    Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));
                    Console.Write("Title: "); string ani_title = Console.ReadLine();
                    Console.Write("Description: "); string ani_desc = Console.ReadLine();
                    string sql_add = "insert into anime_(anime_title,anime_description) values ('{0}', '{1}')";
                    string format = String.Format(sql_add, ani_title, ani_desc);
                    SqlCommand cmd_add = new SqlCommand(format, con);
                    con.Open();
                    cmd_add.ExecuteNonQuery();
                    Console.WriteLine("Added");
                    Console.Read();
                    con.Close();
                    break;

                case "-addchar":
                    Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));
                    Console.Write("Anime: "); string mcworld = CapitalizeWords(Console.ReadLine());
                    Console.Write("Character: "); string mc = CapitalizeWords(Console.ReadLine());
                    Console.Write("Bio: "); string mcdesc = Console.ReadLine().Replace("*", "\n\r").Replace('\'', ' ');
                    Console.Write("Personality: "); string mcpersona = Console.ReadLine().Replace("*", "\n\r").Replace('\'', ' ');
                    Console.Write("Abilities: "); string mcskills = Console.ReadLine().Replace("*", "\n\r").Replace('\'', ' ');

                    string sql_mc = "insert into anime_charac(anime_fnum,anime_char,anime_desc,anime_persona,anime_abilities) values('{0}','{1}','{2}','{3}','{4}')";
                    string _format = String.Format(sql_mc, mcworld, mc, mcdesc, mcpersona, mcskills);
                    SqlCommand cmd_mc = new SqlCommand(_format, con);
                    con.Open();
                    cmd_mc.ExecuteNonQuery();
                    Console.WriteLine("Added mc");
                    Console.Read();
                    con.Close();
                    break;
            }
        }
        public static string CapitalizeWords(string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (value.Length == 0)
                return value;
            StringBuilder result = new StringBuilder(value);
            result[0] = char.ToUpper(result[0]);
            for (int i = 1; i < result.Length; ++i)
            {
                if (char.IsWhiteSpace(result[i - 1]))
                    result[i] = char.ToUpper(result[i]);
            }

            return result.ToString();
        }
        // Still working from top of this comment--------------------------------------------------------------------------------------------
        // Pending from this comment and below-----------------------------------------------------------------------------------------------
        static void choosef()
        {
            Console.Write("...");
            Console.WriteLine();

            string _forms = Console.ReadLine();
            form(_forms);


        }
        static int time = 90;
        static int tt = 3;
        static void Loading()
        {
            while (tt > 0)
            {
                Console.CursorVisible = false;
                Console.Write("|");
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Thread.Sleep(time);
                Console.Write("-");
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Thread.Sleep(time);
                Console.Write("\\");
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Thread.Sleep(time);
                Console.Write("|");
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Thread.Sleep(time);
                tt--;

            }
        }
        static void form(string a)
        {
            string login_req = a;
            DateTime dt = DateTime.Now;
            switch (login_req)
            {
                case "checktime":
                    string timedate = dt.ToString("hh:mm:ss tt");
                    Console.Write(timedate);
                    Console.WriteLine();

                    /*---char _hourdate = hourdate[0];
                         int int_hourdate = Convert.ToInt32(_hourdate);
                         int int_hourdateconv = int_hourdate - '0';
                         hugenumbers(int_hourdateconv);

                         char hd = hourdate[1];
                         int hd_int = Convert.ToInt32(hd);
                         int hdconv_ = hd_int - '0';
                         hugenumbers(hdconv_); ---*/

                    timer();
                    break;
                case "checkdate":
                    string datetime = dt.ToString("MM/dd/yyyy");
                    Console.Write(datetime);
                    Console.WriteLine();

                    timer();
                    break;
                case "-help":
                    Console.WriteLine(@"checktime: Show current time
checkdate: Show current date
Login: Go to login form
-help: Show help");
                    Console.WriteLine("Press ESC to Home...");
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                    {
                        timer();

                    }
                    break;
            }
        }
        static void final_result(string f_res)
        {
            Console.Write(f_res);
            //string cleaned = f_res.Replace("\n", "").Replace("\r", "");
            //Console.SetCursorPosition(Console.CursorLeft,  Console.CursorTop - 1);
        }
        static void timer()
        {
            int j;
            for (j = 5; j >= 0; j--)
            {
                if (j == 5) { string ww = "Refresh in: "; Console.Write(ww); }
                Loading();
                // Console.SetCursorPosition(Console.CursorLeft - Convert.ToInt32(ww.Length) + 13, Console.CursorTop);
                Console.Write(" " + j + "...");
                tt = 3;
                if (j == 0) { Console.Clear(); choosef(); }
            }
        }
    }
}

