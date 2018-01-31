using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleDraw;
using System.Net.NetworkInformation;
using System.IO;


namespace IRCBotter
{
    class Program
    {
        static ConsoleColor defaults;
        static string host;
        static string port;

        static void Main(string[] args)
        {

           
            #region Credit

            defaults = Console.ForegroundColor;

            Message.Logo();

            Console.WriteLine("");
            Console.WriteLine("Press any key to start");
            Console.ReadKey();

            #endregion 


            Console.WriteLine("Checking Connection....");
            Ping p = new Ping();
            PingReply ps = p.Send("google.it");

            if (ps.Status == IPStatus.Success)
            {
                Console.WriteLine("Connection Success.");
            }

            //Try to read last configration
            try
            {
                StreamReader sr = new StreamReader("data.txt");
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("ip"))
                    {
                        string[] t = line.Split(':');
                        host = t[1];
                    }

                    if (line.StartsWith("port"))
                    {
                        string[] t = line.Split(':');
                        port = t[1];
                    }
                }

                goto end;
            }
            catch
            { }



            hostname:
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("");
            Console.WriteLine("Insert hostname or ip address");
            Console.ForegroundColor = defaults;
            Console.WriteLine("");
            Console.Write("Host/IP> ");
            host = Console.ReadLine();

            Console.WriteLine(host);
           
            try
            {


                p = new Ping();
                ps = p.Send(host);

                if (ps.Status == IPStatus.Success)
                {
                    Message.Notice("Connection Success...");
                    Console.WriteLine("Plase insert Port of connection");
                    Console.Write("PORT:>");
                    port = Console.ReadLine();
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Message.Notice("Want to save configuration to file?");
                    Message.Notice("Y = Yes, N = No, Start client");
                    Console.Write("<Y/N> ");

                    
                        StreamWriter sw = new StreamWriter("data.txt");
                        sw.WriteLine("ip:" + host);
                        sw.WriteLine("port:" + port);
                        sw.Close();

                        goto end;
                   
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Message.Error("Hostname wrong or Offline");
                    Console.WriteLine("");
                }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Message.Error("Hostname wrong or Offline");
                Console.WriteLine("");
                goto hostname;
            }


            end:




            


            WindowManager.UpdateWindow(100, 48);
            WindowManager.SetWindowTitle("IRCBotter");
            IrcBot bot = new IrcBot(host, port, "#test", "IrcBOT");
            new Client(host, port,bot);
        }
    }
}
