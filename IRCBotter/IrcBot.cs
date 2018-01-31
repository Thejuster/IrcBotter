using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;
using IRCBotter.Commands;

namespace IRCBotter
{
    public class IrcBot
    {
        private readonly string _host;
        private readonly string _port;
        private readonly string _channel;
        private readonly string USER = "USER IRCbot 0 * :IRCbot";
        private readonly string _nick;
        private readonly int _maxRetries = 5;
        public IrcEngine engine = new IrcEngine();
        private UserLevels levels = new UserLevels();

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
                                            engine.OnMsg(inputLine);
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






        void engine_OnCommand(object sender, IrcEngine.OnCommandArgs e)
        {
            Message.Debug(e.GetData());
        }

        

        /// <summary>
        /// When server say message from the client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void engine_OnServerMessage(object sender, IrcEngine.OnServerMessageArgs e)
        {
            Message.MOTD(e.GetData());
        }


        /// <summary>
        /// On Quit Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void engine_OnQuit(object sender, IrcEngine.OnQuitEventArgs e)
        {
            Message.Notice(e.GetUser() + " left from " + e.GetCurrentChannel());
        }
     

        /// <summary>
        /// On user Join
        /// </summary>
        void engine_OnJoin(object sender, IrcEngine.OnJoinEventArgs e)
        {
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
            //Check if user send a command
            if (e.GetText().StartsWith("!"))
            {
                engine.OnCommands(e.GetData());
            }

            Message.Text(e.GetCurrentChannel() + " " + e.GetUser() + ":>" + e.GetText());
        }


        [UserLevels(LevelRequired=0)]
        void HelpCommand()
        {

        }
    }
}
