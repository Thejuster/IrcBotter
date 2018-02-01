using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IRCBotter
{
    public class IrcEngine
    {
        //Public Field
        public List<String> Channels = new List<string>();
        public List<String> Nicks = new List<string>() { "Thejuster","Pierotofy","Lumo","jacopos" };
        public List<int> Levels = new List<int>() { 100, 0, 0, 20 };
        public List<Query> Queryes = new List<Query>();



        //Structure
        public struct Query
        {
            public string username;
            public string message;
        }

      

        #region Delegate

        public delegate void JoinHandler(Object sender, OnJoinEventArgs e);
        public delegate void MessageHandler(Object sender, OnMessageEventArgs e);
        public delegate void QuitHandler(Object sender, OnQuitEventArgs e);
        public delegate void ServerMessageHandler(Object sender, OnServerMessageArgs e);
        public delegate void CommandHandler(Object sender, OnCommandArgs e);
        public delegate void PrivateMessageHandler(Object sender, OnPrivateMessageArgs e);

        #endregion




        #region Event

        public event JoinHandler OnJoin;
        public event MessageHandler OnMessage;
        public event QuitHandler OnQuit;
        public event ServerMessageHandler OnServerMessage;
        public event CommandHandler OnCommand;
        public event PrivateMessageHandler OnPrivateMessage;

        #endregion



        #region Args

        public class OnJoinEventArgs : System.EventArgs
        {
            //funzioni a cui accedono
            private string message;
            private string p;
            private int p_2;

            public OnJoinEventArgs(string p)
            {
                message = p;
            }

      
            /// <summary>
            /// Get current username Written message
            /// </summary>
            /// <returns>Username</returns>
            public string GetUser()
            {
                string[] s1 = message.Split('!');
                string nick = s1[0].Replace(":", "");
                return nick;
            }

            /// <summary>
            /// Get Current username Channel
            /// </summary>
            /// <returns></returns>
            public string GetCurrentChannel()
            {
                string[] t = Regex.Split(message, "JOIN");
                string[] s1 = t[1].Split(':');
                return s1[1];
            }


            /// <summary>
            /// Get text written of username
            /// </summary>
            /// <returns></returns>
            public string GetText()
            {
                string[] t = Regex.Split(message, "JOIN");
                string[] s1 = t[1].Split(':');
                return s1[1];
            }

            /// <summary>
            /// Get Raw Data Text
            /// </summary>
            /// <returns>Raw Data</returns>
            public string GetData()
            {
                string nick = GetUser();
                string channel = GetCurrentChannel();

                string[] t = Regex.Split(message, "JOIN");
                return t[1];
            }
        }

        public class OnMessageEventArgs : System.EventArgs
        {
            private string message;

            public OnMessageEventArgs(string p)
            {
                message = p;
            }


            /// <summary>
            /// Get current username Written message
            /// </summary>
            /// <returns>Username</returns>
            public string GetUser()
            {
                string[] s1 = message.Split('!');
                string nick = s1[0].Replace(":", "");
                return nick;
            }

            /// <summary>
            /// Get Current username Channel
            /// </summary>
            /// <returns></returns>
            public string GetCurrentChannel()
            {
                string[] t = Regex.Split(message,"PRIVMSG");
                string[] s1 = t[1].Split(':');
                return s1[0];
            }


            /// <summary>
            /// Get text written of username
            /// </summary>
            /// <returns></returns>
            public string GetText()
            {
                string[] t = Regex.Split(message, "PRIVMSG");
                string[] s1 = t[1].Split(':');
                return s1[1];
            }

            /// <summary>
            /// Get Raw Data Text
            /// </summary>
            /// <returns>Raw Data</returns>
            public string GetData()
            {
                return message;
            }
        }

        public class OnQuitEventArgs : System.EventArgs
        {
            private string msg;

            public OnQuitEventArgs(string p)
            {
                msg = p;
            }

            /// <summary>
            /// Get current username Written message
            /// </summary>
            /// <returns>Username</returns>
            public string GetUser()
            {
                string[] s1 = msg.Split('!');
                string nick = s1[0].Replace(":", "");
                return nick;
            }

            /// <summary>
            /// Get Current username Channel
            /// </summary>
            /// <returns></returns>
            public string GetCurrentChannel()
            {
                string[] t = Regex.Split(msg, "PART");
                string[] s1 = t[1].Split(':');
                return s1[0];
            }


            /// <summary>
            /// Get text written of username
            /// </summary>
            /// <returns></returns>
            public string GetText()
            {
                string[] t = Regex.Split(msg, "PART");
                string[] s1 = t[1].Split(':');
                return s1[1];
            }

            /// <summary>
            /// Get Raw Data Text
            /// </summary>
            /// <returns>Raw Data</returns>
            public string GetData()
            {
                string nick = GetUser();
                string channel = GetCurrentChannel();

                string[] t = Regex.Split(msg, "PART");
                return t[1];
            }
        }

        public class OnServerMessageArgs : System.EventArgs
        {
            private string msg;

            public OnServerMessageArgs(string m)
            {
                msg = m;
            }


            public string GetData()
            {
                return msg;
            }
        }

        public class OnCommandArgs : System.EventArgs
        {
            private string msg;

            public OnCommandArgs(string m)
            {
                msg = m;
            }


            public string GetData()
            {
                return msg;
            }

            /// <summary>
            /// Get current username Written message
            /// </summary>
            /// <returns>Username</returns>
            public string GetUser()
            {
                string[] s1 = msg.Split('!');
                string nick = s1[0].Replace(":", "");
                return nick;
            }


            /// <summary>
            /// Get Current username Channel
            /// </summary>
            /// <returns></returns>
            public string GetCurrentChannel()
            {
                string[] t = Regex.Split(msg, "PRIVMSG");
                string[] s1 = t[1].Split(':');
                return s1[0];
            }


            /// <summary>
            /// Get text written of username
            /// </summary>
            /// <returns></returns>
            public string GetText()
            {
                string[] t = Regex.Split(msg, "PRIVMSG");
                string[] s1 = t[1].Split(':');
                return s1[1];
            }
        }

        public class OnPrivateMessageArgs : System.EventArgs
        {
            private string msg;

            public OnPrivateMessageArgs(string m)
            {
                msg = m;
            }

            public string GetData()
            {
                return msg;
            }

            /// <summary>
            /// Get current username Written message
            /// </summary>
            /// <returns>Username</returns>
            public string GetUser()
            {
                string[] s1 = msg.Split('!');
                string nick = s1[0].Replace(":", "");
                return nick;
            }


            /// <summary>
            /// Get Current username Channel
            /// </summary>
            /// <returns></returns>
            public string GetCurrentChannel()
            {
                string[] t = Regex.Split(msg, "PRIVMSG");
                string[] s1 = t[1].Split(':');
                return s1[0];
            }


            /// <summary>
            /// Get text written of username
            /// </summary>
            /// <returns></returns>
            public string GetText()
            {
                string[] t = Regex.Split(msg, "PRIVMSG");
                string[] s1 = t[1].Split(':');
                return s1[1];
            }
        }

        #endregion




        #region Void

        public void On_Join(string m)
        {
            OnJoinEventArgs j = new OnJoinEventArgs(m);
            this.OnJoin(this, j);
        }

        public void OnMsg(string m)
        {
            OnMessageEventArgs e = new OnMessageEventArgs(m);
            OnMessage(this, e);
        }

        public void OnQuits(string m)
        {
            OnQuitEventArgs e = new OnQuitEventArgs(m);
            OnQuit(this, e);
        }

        public void OnServerMessages(string m)
        {
            OnServerMessageArgs e = new OnServerMessageArgs(m);
            OnServerMessage(this, e);
        }

        public void OnCommands(string m)
        {
            OnCommandArgs e = new OnCommandArgs(m);
            OnCommand(this, e);
        }

        public void OnMessagePrivate(string m)
        {
            OnPrivateMessageArgs e = new OnPrivateMessageArgs(m);
            OnPrivateMessage(this, e);
        }
        #endregion


        #region Public Function

        /// <summary>
        /// Get Text of message based on RawData
        /// </summary>
        /// <param name="Data">RawData</param>
        /// <returns>Text</returns>
        public string GetText(string Data)
        {
            string[] t = Regex.Split(Data, "PRIVMSG");
            string[] s1 = t[1].Split(':');
            return s1[1];
        }

        /// <summary>
        /// Get username based on RawData
        /// </summary>
        /// <param name="Data">RawData</param>
        /// <returns>Get Username</returns>
        public string GetUser(string Data)
        {
            string[] s1 = Data.Split('!');
            string nick = s1[0].Replace(":", "");
            return nick;
        }


          /// <summary>
          /// Get Curret user channel
          /// </summary>
          /// <param name="Data">RawData</param>
          /// <returns>channel</returns>
        public string GetCurrentChannel(string Data)
        {
            string[] t = Regex.Split(Data, "PRIVMSG");
            string[] s1 = t[1].Split(':');
            return s1[0];
        }
        #endregion
    }
}
