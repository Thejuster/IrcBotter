using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleDraw;
using System.Net.NetworkInformation;
using System.IO;
using System.Runtime.Remoting;
using IrcbotPlugin;

/*************************************************
 * Irc Botter - By Thejuster
 * Under GPL GNU v3 Licence
 * Plase report bug, contribution and new feature
 * Enjoy :)
 * **********************************************/


namespace IRCBotter
{
    class Program
    {
        static ConsoleColor defaults;
        static string host;
        static string port;
        public static List<IPluginInfo> plugs = new List<IPluginInfo>();


        static void Main(string[] args)
        {

            //Load all Plugin
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.dll");
            
            //Remove Non plugin library
            foreach (string s in files)
            {
                try
                {
                    ObjectHandle hwnd = Activator.CreateInstanceFrom(s, "IrcbotPlugin.PluginInfo");
                    plugs.Add(hwnd.Unwrap() as IPluginInfo);
                }
                catch { }
            }


 
            //Starting....

            #region Credit

            defaults = Console.ForegroundColor;

            Message.Logo();
            Message.Notice("Plugin Detected: " + plugs.Count);
            Console.WriteLine("");
            Console.WriteLine("");

            ConsoleColor old = Console.ForegroundColor;
            for (int i = 0; i < plugs.Count; i++)
            {
                Console.Write("Loaded: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(plugs[i].name);
                Console.ForegroundColor = old;
                Console.Write("   Written by: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(plugs[i].author);
                Console.ForegroundColor = old;
                Console.Write("   Version: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(plugs[i].version);
                Console.ForegroundColor = old;
            }


            Console.WriteLine("");
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
            IrcBot bot = new IrcBot(host, port, "#test", "IrcBotter");
            bot.plugs = plugs; //Assign plugin to IrcEngine
            new Client(host, port,bot);


           
        }
    }
}
