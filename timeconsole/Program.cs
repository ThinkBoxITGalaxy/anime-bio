using System;
using timeconsole;
using System.Data.SqlClient;
using System.Threading;
// anime database - username: anime passwor: 123
namespace ConsoleAnimations
{

    class MainClass
    {
       
        public static void Main(string[] args)
        {
       
            // Console.Write("Command Prompt...");
            // char charyn;
            //Console.ReadKey().Key == ConsoleKey.Enter
            //charyn = Console.ReadKey().KeyChar;
            string search = Console.ReadLine().ToLower();

            datas(search);
            choosef();

        }
        static void datas(string cc)
        {
            var con = Connz.sqlConnz;

            string sql = @"SELECT anime_.anime_title, anime_.anime_description
                           FROM anime_ INNER JOIN anime_charac ON
                           anime_.anime_id = anime_charac.anime_fnum";

            SqlCommand command = new SqlCommand(sql, con);
            con.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.WriteLine(reader.GetValue(i));
                    }   
                    Console.WriteLine();
                }
            }
            con.Close();
        }
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
                case "Login":
                    Console.WriteLine("Login Form...");
                    Console.Write("Username: ");
                    string uname = Console.ReadLine();
                    Console.Write("Password: ");
                    string pword = Console.ReadLine();

                    if (uname == "admin" && pword == "admin")
                    {
                        profile();
                        //int i;
                        //Console.WriteLine("Login Successfully");
                        //Console.WriteLine();
                        //Console.Write("Closed in");
                        //for (i = 5; i >= 0; i--)
                        //{
                        //    Thread.Sleep(1000);
                        //    Console.Write(" " + i);
                        //}
                        //if (i == 20)
                        //{
                        //    Console.Clear();
                        //}
                    }
                    else
                    {
                        Console.WriteLine("Login Failed");
                    }

                    break;

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
        /* static void hugenumbers(int _data)

        {
            int data_ = _data;
            string res = "";
            switch (data_)
            {
                case 0:
                    res = "\n▄▄▄▄▄▄\n█    █\n█    █\n█    █\n▀▀▀▀▀▀";
                    break;
                case 1:
                    res = "\n  ▄▄ \n   █ \n   █ \n   █ \n  ▀▀▀";
                    break;
                case 2:
                    res = "\n▄▄▄▄▄▄\n     █\n▄▄▄▄▄█\n█     \n▀▀▀▀▀▀";
                    break;
                case 3:
                    res = @"
▄▄▄▄▄▄
     █
  ▀▀▀█
     █
▀▀▀▀▀▀";
                    break;
                case 4:
                    res = @"
█    █
█    █
█▄▄▄▄█
     █
     █";
                    break;
                case 5:
                    res = @"
▄▄▄▄▄▄
█     
█▄▄▄▄▄
     █
▀▀▀▀▀▀";
                    break;
                case 6:
                    res = @"
▄▄▄▄▄▄
█     
█▄▄▄▄▄
█    █
▀▀▀▀▀▀";
                    break;
                case 7:
                    res = @"
▄▄▄▄▄▄
     █
     █
     █
     ▀";
                    break;
                case 8:
                    res = @"
▄▄▄▄▄▄
█    █
■■■■■■
█    █
▀▀▀▀▀▀";
                    break;
                case 9:
                    res = @"
▄▄▄▄▄▄
█    █
▀▀▀▀▀▀
     █
▀▀▀▀▀▀";
                    break;
                case 10:
                    res = "";
                    break;
            }
            final_result(res);
        }*/
        static void timer()
        {
            int j;
            for (j = 5; j >= 0; j--)
            {
                if (j == 5){string ww = "Refresh in: ";Console.Write(ww);}
                Loading();
                // Console.SetCursorPosition(Console.CursorLeft - Convert.ToInt32(ww.Length) + 13, Console.CursorTop);
                Console.Write(" " + j + "...");
                tt = 3;
                if (j == 0){Console.Clear();choosef();}
            }
        }
        static void profile()
        {
            Console.WriteLine("Profileee");
            string ctgry_p = Console.ReadLine().ToLower();
            switch (ctgry_p)
            {
                case "naruto":
                    string naruto_profile = Console.ReadLine().ToLower();
                    switch (naruto_profile)
                    {
                        case "naruto uzumaki":
                            Console.WriteLine(@"Naruto Uzumaki (Japanese: うずまき ナルト, Hepburn: Uzumaki Naruto) 
(/ˈnɑːrətoʊ/) is a fictional character in the manga and anime franchise Naruto, created by Masashi Kishimoto. 
Serving as the eponymous protagonist of the series, he is a young ninja from the fictional village of Konohagakure (Hidden Leaf Village). 
The villagers ridicule and ostracize Naruto on account of the Nine-Tailed Demon Fox—a malevolent creature that attacked Konohagakure—that 
was sealed away in Naruto's body. Despite this, he aspires to become his village's leader, the Hokage in order to receive their approval. 
His carefree, optimistic and boisterous personality enables him to befriend other Konohagakure ninja, as well as ninja from other villages.
Naruto appears in the series' films and in other media related to the franchise, including video games and original video animations (OVA), 
as well as the sequel Boruto: Naruto Next Generations by Ukyo Kodachi, where he is the Hokage and his son, Boruto, is the protagonist.");
                            break;

                        case "hinata hyuga":
                            Console.WriteLine(@"Hinata Hyuga (日向 ヒナタ, Hyūga Hinata) is a fictional character in the anime and manga Naruto,
created by Masashi Kishimoto. Hinata is a kunoichi and the former heiress of the Hyūga clan from the fictional village of Konohagakure.
She is also a member of Team 8, which consists of herself, Kiba Inuzuka with his ninja dog — Akamaru, Shino Aburame,
and team leader Kurenai Yuhi. At the start of the series, Hinata has strong admiration toward the main protagonist — Naruto Uzumaki, 
which eventually turns into love as the story progresses. Hinata has appeared several times in the series' feature films, 
most notably The Last: Naruto the Movie (2014), which revolves around her relationship with Naruto. She has also been present 
in other media related to the franchise, including video games, original video animations, and the manga and anime sequel Boruto: 
Naruto Next Generations (2016), in which she has become the mother of Boruto Uzumaki and Himawari Uzumaki, and is now named 
Hinata Uzumaki (うずまき ヒナタ, Uzumaki Hinata).");
                            break;
                    }
                    break;

                case "one piece":
                    string onepiece_profile = Console.ReadLine().ToLower();
                    switch (onepiece_profile)
                    {
                        case "monkey d luffy":
                            Console.WriteLine(@"Monkey D. Luffy (/ˈluːfi/ LOO-fee) (Japanese: モンキー・D・ルフィ,
Hepburn: Monkī Dī Rufi, [ɾɯɸiː]), also known as Straw Hat Luffy[n 1],
is a fictional character and the main protagonist of the One Piece manga series,
created by Eiichiro Oda. Luffy made his debut in One Piece Chapter #1 as a young boy who acquires the properties
of rubber after accidentally eating the supernatural Gum-Gum Fruit.");
                            break;

                        case "roronoa zoro":
                            Console.WriteLine(@"Roronoa Zoro (ロロノア・ゾロ, spelled as Roronoa Zolo or Roronoa Zollo
in some English adaptations), nicknamed Pirate Hunter Zoro (海賊狩りのゾロ, Kaizoku-Gari no Zoro), 
is a fictional character in the One Piece franchise created by Eiichiro Oda.");
                            break;
                    }
                    break;
                    
            }
            Console.ReadLine();
        }
    }       
}

