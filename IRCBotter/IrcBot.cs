using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;
using IRCBotter.Commands;
using System.Reflection;
using IrcbotPlugin;

/*************************************************
 * Irc Botter - By Thejuster
 * Under GPL GNU v3 Licence
 * Plase report bug, contribution and new feature
 * Enjoy :)
 * **********************************************/


namespace IRCBotter
{
    public class IrcBot
    {
        private readonly string _host;
        private readonly string _port;
        private readonly string _channel;
        private readonly string USER = "USER IrcBotter 0 * :IrcBotter";
        private readonly string _nick;
        private readonly int _maxRetries = 5;
        public IrcEngine engine = new IrcEngine();
        public List<IPluginInfo> plugs = new List<IPluginInfo>();

        //private UserLevels levels = new UserLevels();

        public bool Menu = false;

        private Client clis;
        public IrcBot(string host, string port, string channel, string nick)
        {
            this._host = host;
            this._port = port;
            this._channel = channel;
            this._nick = nick;
            Thread t = new Thread(Start);
            t.Start();


            //Action del = () => { port = "aa"; };
            //del.Invoke();

        }


        public void Start()
        {
            var retry = false;
            var retryCount = 0;
            

            engine.OnJoin += new IrcEngine.JoinHandler(engine_OnJoin);
            engine.OnMessage += new IrcEngine.MessageHandler(engine_OnMessage);
            engine.OnQuit += new IrcEngine.QuitHandler(engine_OnQuit);
            engine.OnServerMessage += new IrcEngine.ServerMessageHandler(engine_OnServerMessage);
            engine.OnCommand += new IrcEngine.CommandHandler(engine_OnCommand);
            engine.OnPrivateMessage += new IrcEngine.PrivateMessageHandler(engine_OnPrivateMessage);
            
            do
            {
                try
                {
                    using (var irc = new TcpClient(_host, Convert.ToInt16(_port)))
                    using (var stream = irc.GetStream())
                    using (var reader = new StreamReader(stream))
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.WriteLine("NICK " + _nick);
                        writer.Flush();
                        writer.WriteLine(USER);
                        engine.Channels.Add(_channel);
                        writer.Flush();

                        while (true)
                        {
                            string inputLine;
                            while ((inputLine = reader.ReadLine()) != null && Menu == false)
                            {
                                //Unparsed server message
                                //Console.WriteLine("<- " + inputLine);

                               
                                // split the lines sent from the server by spaces (seems to be the easiest way to parse them)
                                string[] splitInput = inputLine.Split(new Char[] { ' ' });

                                if (splitInput[0] == "PING")
                                {
                                    string PongReply = splitInput[1];
                                    Message.Info("-> PONG " + PongReply);
                                    writer.WriteLine("PONG " + PongReply);
                                    writer.Flush();
                                    //continue;
                                }

                              
                                        writer.WriteLine("JOIN " + _channel);
                                        writer.Flush();
                             

                                        if (inputLine.Contains("PRIVMSG"))
                                        {
                                            if (inputLine.Contains("#"))
                                                engine.OnMsg(inputLine);
                                            else
                                                engine.OnMessagePrivate(inputLine);
                                        }

                                        if (inputLine.Contains("PART"))
                                        {
                                            engine.OnQuits(inputLine);
                                        }
                                        if (inputLine.Contains("JOIN"))
                                        {
                                            engine.On_Join(inputLine);
                                        }

                                        if (splitInput.Length > 1 && splitInput[1].Contains("372") || splitInput.Length > 1 && splitInput[1].Contains("375"))
                                        {
                                            engine.OnServerMessages(inputLine);
                                            
                                        }

                                        //Execute Queryes for all channels and user
                                        for (int i = 0; i < engine.Queryes.Count; i++)
                                        {
                                            writer.WriteLine("PRIVMSG {0} {1}", engine.Queryes[i].username, engine.Queryes[i].message);
                                            engine.Queryes.RemoveAt(i);
                                        }
                                }
                            }
                        }
                    }
                
                catch (Exception e)
                {
                    // shows the exception, sleeps for a little while and then tries to establish a new connection to the IRC server
                    Console.WriteLine(e.ToString());
                    Thread.Sleep(5000);
                    retry = ++retryCount <= _maxRetries;
                }
            } while (retry);
        }






        /// <summary>
        /// When user send to bot a private message
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        void engine_OnPrivateMessage(object sender, IrcEngine.OnPrivateMessageArgs e)
        {
            Message.Debug("Private Message [" + e.GetText() + "]");

            if (e.GetText().Contains("hello"))
            {
                engine.Queryes.Add(new IrcEngine.Query() { username = e.GetUser(), message = "Hello " + e.GetUser() + " how are you?" });               
            }
        }





        /// <summary>
        /// When user write a command
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        void engine_OnCommand(object sender, IrcEngine.OnCommandArgs e)
        {
            //Passing arguments to Plugin
            for (int i = 0; i < plugs.Count; i++)
            {
                plugs[i].OnCommand(e.GetData());
            }

            switch (e.GetText())
            {
                case "!help":
                    HelpCommand(e.GetUser(),e.GetData()); 
                break;
            }
        }

        

        /// <summary>
        /// When server say message from the client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void engine_OnServerMessage(object sender, IrcEngine.OnServerMessageArgs e)
        {
            //Passing arguments to Plugin
            for (int i = 0; i < plugs.Count; i++)
            {
                plugs[i].OnServerMessage(e.GetData());
            }

            Message.MOTD(e.GetData());
        }


        /// <summary>
        /// On Quit Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void engine_OnQuit(object sender, IrcEngine.OnQuitEventArgs e)
        {
            //Passing arguments to Plugin
            for (int i = 0; i < plugs.Count; i++)
            {
                plugs[i].OnQuit(e.GetData());
            }

            Message.Notice(e.GetUser() + " left from " + e.GetCurrentChannel());
        }
     

        /// <summary>
        /// On user Join
        /// </summary>
        void engine_OnJoin(object sender, IrcEngine.OnJoinEventArgs e)
        {
            //Passing arguments to Plugin
            for (int i = 0; i < plugs.Count; i++)
            {
                plugs[i].OnJoin(e.GetData());
            }

            Message.Notice(e.GetUser() + " has joined to " + e.GetCurrentChannel());
        }


        /// <summary>
        /// On user connected at same channel for the bot
        /// say a message
        /// </summary>
        /// <param name="sender">Client</param>
        /// <param name="e">Operation</param>
        void engine_OnMessage(object sender, IrcEngine.OnMessageEventArgs e)
        {      
            //Passing arguments to Plugin
            for (int i = 0; i < plugs.Count; i++)
            {
                plugs[i].OnMessage(e.GetData());
            }


                //Check if user send a command
                if (e.GetText().StartsWith("!"))
                {
                    engine.OnCommands(e.GetData());
                }

            Message.Text(e.GetCurrentChannel() + " " + e.GetUser() + ":>" + e.GetText());

        }




        [UserLevel(LevelRequired = 0)]
        public void HelpCommand(string user,string data)
        {
            bool check = CheckPermission(user);
           
            if (check)
            {
                string us = engine.GetUser(data);
                engine.Queryes.Add(new IrcEngine.Query() { username = user, message = "Ciao " + user + " hai bisogno di aiuto?"});
                engine.Queryes.Add(new IrcEngine.Query() { username = user, message = "Ecco una piccola lista di comandi a tua disposizione" });               
            }
            else
            {
                Message.Error(user + " perform non authorized command");
            }
                      
        }



        [UserLevel(LevelRequired=50)]
        public void VoiceCommand(string user, string data)
        {

        }




        /// <summary>
        /// Get Attribute from Method
        /// </summary>
        /// <param name="t">Type</param>
        /// <returns>Objects</returns>
       public static object[] GetAttribute(Type t)
       { 
           MethodInfo mInfo;
           mInfo = typeof(IrcBot).GetMethod("HelpCommand");
           object[] userLevel = mInfo.GetCustomAttributes(false);
           return userLevel;
       }



        /// <summary>
        /// Checking user Level to perform command
        /// Level is setted on Attribute
        /// </summary>
        /// <param name="user">Username</param>
        /// <returns>True if can perfor,</returns>
        public bool CheckPermission(string user)
        {
           object[] levels = GetAttribute(typeof(IrcBot));
           UserLevel lv = (UserLevel)levels[0];
           int ulevel = 0;

           for (int i = 0; i < engine.Nicks.Count; i++)
           {
               if (engine.Nicks[i] == user)
               {
                   ulevel = engine.Levels[i];
               }
           }

           if (ulevel < lv.LevelRequired)
               return false;
           else
             return true;
       }
     
    }
}
