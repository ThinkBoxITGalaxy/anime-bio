using System;
using timeconsole;
using System.Data.SqlClient;
using System.Threading;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
// anime database - username: anime passwor: 123

// remove char and anime ttle --- 
// update anime title

/* 
  For github links:
  stackoverflow.com/questions/23401652/fatal-the-current-branch-master-has-no-upstream-branch
  docs.github.com/en/github/importing-your-projects-to-github/adding-an-existing-project-to-github-using-the-command-line

 */
namespace ConsoleAnimations
{
    class MainClass
    {
        // not mine - stackoverflow
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;

        public static void Main(string[] args)
        {
            // not mine - stackoverflow
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE);
            controls();
            //  choosef();
        }
        static void controls()
        {
            Logo.AnimeWikipedia();

            string viewchar_controls = "select anime_title from anime_";
            string mainpage_view = " || ";

            Query.view(viewchar_controls, mainpage_view);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Search: ");
            string search = Console.ReadLine().ToLower();

            if (string.IsNullOrEmpty(search))
            {
                Console.WriteLine("Please input your favorite anime");
            }
            else if (search == "-insert")
            {
                Console.Clear();
                _add();
            }
            else if (search == "help")
            {
                Console.Clear();
                Console.WriteLine("exit - Terminate the window\nclear - Back to main page");
                controls();
            }
            else
            {
                datas(search);
            }
        }
        static void datas(string cc)
        {
            var con = Connz.sqlConnz;
            Console.Write("Characters");
            string mainchar_list = @"SELECT anime_charac.anime_char 
                           FROM anime_ INNER JOIN anime_charac 
                           ON anime_.anime_title = anime_charac.anime_fnum  
                           Where anime_charac.anime_fnum = '" + cc + "'";
            string anime_desc = @"SELECT anime_description FROM anime_ WHERE anime_title = '" + cc + "'";
            string barrier = " : ";

            Query.view(mainchar_list, barrier);
            Query.UnloopView(anime_desc);

            // search char
            bool w = true;
            while (w)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Search characters: ");
                string char_ = Console.ReadLine();
                string sss = "";
                string search_char = @"SELECT anime_charac.anime_desc,anime_charac.anime_persona,anime_abilities
                           FROM anime_ INNER JOIN anime_charac 
                           ON anime_.anime_title = anime_charac.anime_fnum  
                           Where anime_charac.anime_char = '" + char_ + "'";
                Console.ForegroundColor = ConsoleColor.Cyan;
                Query.view(search_char, sss);

                if (char_ == "exit")
                {
                    w = false;
                }
                if (char_ == "clear")
                {
                    Console.Clear();
                    controls();
                }
                if (char_ == "help")
                {
                    Console.Clear();
                    Console.WriteLine("exit - Terminate the window\nclear - Back to main page");
                    controls();
                }
            }
        }
        // 2nd --------------------------------------------------------------------------------------
        static void _add()
        {
            var con = Connz.sqlConnz;
            string viewchar = "select anime_title from anime_";
            string database_view = " || ";
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Logo.AnimeDatabase();
            Query.view(viewchar, database_view);
            Console.WriteLine("\n");
            con.Close();

            string _command = Console.ReadLine();
            switch (_command)
            {
                case "-addtitle":
                    Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));
                    Console.Write("Title: "); string ani_title = Console.ReadLine();
                    if (ani_title.ToLower() == "clear")
                    {
                        _add();
                    }
                    else if (ani_title.ToLower() == "home")
                    {
                        goto case "home";
                    }
                    Console.Write("Description: ");
                    string ani_desc = Console.ReadLine().Replace("*", "\n\r").Replace("\'", "' + char(39) + '");
                    if (ani_desc.ToLower() == "clear")
                    {
                        _add();
                    }
                    else if (ani_desc.ToLower() == "home")
                    {
                        goto case "home";
                    }
                    else
                    {
                        Query.SqlInsertAnime(ani_title, ani_desc);
                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                            goto case "-addtitle";
                        }
                    }
                    break;

                case "-addchar":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));
                    Console.Write("Anime: "); string mcworld = BigLetters.CapitalizeWords(Console.ReadLine());
                    Console.Write("Character: "); string mc = BigLetters.CapitalizeWords(Console.ReadLine());
                    Console.Write("Bio: "); string mcdesc = Console.ReadLine().Replace("*", "\n\r").Replace("\'", "' + char(39) + '");
                    Console.Write("Personality: "); string mcpersona = Console.ReadLine().Replace("*", "\n\r").Replace("\'", "' + char(39) + '");
                    Console.Write("Abilities: "); string mcskills = Console.ReadLine().Replace("*", "\n\r").Replace("\'", "' + char(39) + '");

                    Query.SqlInsertCharacter(mcworld, mc, mcdesc, mcpersona, mcskills);
                    break;

                case "-update":
                    Console.Clear();
                    string viewchar_update = "select anime_title from anime_";
                    string update_view = " || ";
                    Logo.Anime();
                    Query.view(viewchar_update, update_view);
                    Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));
                    Console.Write("Character: "); string up_mc = BigLetters.CapitalizeWords(Console.ReadLine());
                    Console.Write("Description: "); string up_desc = Console.ReadLine().Replace("*", "\n\r").Replace("\'", "'+char(39)+'");
                    Console.Write("Personality: "); string up_persona = Console.ReadLine().Replace("*", "\n\r").Replace("\'", "'+char(39)+'");
                    Console.Write("Ability: "); string up_abi = Console.ReadLine().Replace("*", "\n\r").Replace("\'", "'+char(39)+'");

                    Query.SqlUpdate(up_mc, up_desc, up_persona, up_abi);
                    break;

                case "-remove":
                    Console.Clear();
                    string viewchar_remove = "select anime_title from anime_";
                    string remove_view = "\n";
                    Query.view(viewchar_remove, remove_view);

                    string deletion = Console.ReadLine();
                    switch (deletion)
                    {
                        case "#terminate character":
                            Console.WriteLine("This page is for deleting character from database...");
                            Console.Write("Character: ");
                            string del_char = BigLetters.CapitalizeWords(Console.ReadLine());
                            string view_chars = "select anime_char from anime_charac where anime_fnum = '" + del_char.Replace("List ", "") + "'";
                            string removelist_view = "\n";
                            if (del_char.Contains("List"))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Query.view(view_chars, removelist_view);
                            }

                            string view_del = "select anime_fnum from anime_charac where anime_char = '" + del_char + "'";
                            string view_post = "Successfully deleted " + del_char + " from anime ";
                            Query.view(view_del, view_post);

                            string chardel = "delete from anime_charac where anime_char = '" + del_char + "'";

                            Query.SqlDelete(chardel);

                            if (Console.ReadKey().Key == ConsoleKey.Enter)
                            {
                                Console.Clear();
                                goto case "#terminate character";
                            }
                            break;

                        case "#terminate anime":
                            Console.WriteLine("This page is for deleting anime from database...");
                            Console.Write("Anime: "); string del_ani = BigLetters.CapitalizeWords(Console.ReadLine());

                            string anime_delete = "delete from anime_ where anime_title = '" + del_ani + "'";

                            Console.Write("Are you sure your want to remove {0}? Y/N", del_ani);
                            if (Console.ReadKey().Key == ConsoleKey.Y)
                            {
                                Query.SqlDelete(anime_delete);
                                Console.WriteLine("\nDeleted {0}", del_ani);
                            }
                            else if (Console.ReadKey().Key == ConsoleKey.N)
                            {
                                Console.WriteLine("Removing terminated");
                                Console.Clear();
                                goto case "#terminate anime";
                            }
                            break;
                    }
                    if (Console.ReadKey().Key == ConsoleKey.Enter)
                    {
                        goto case "-remove";
                    }
                    break;

                case "help":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("-addtitle - add new anime\n-addchar - add new character\n-update - update existing character\n-remove - delete something from database\nhome - back to main page\n\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    _add();
                    break;
                case "home":
                    Console.Clear();
                    controls();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid keyword\n\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    _add();
                    break;
            }
        }
        // Not mine ----------------------------------------------------------------

        // Still working from top of this comment--------------------------------------------------------------------------------------------
        //▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄
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

