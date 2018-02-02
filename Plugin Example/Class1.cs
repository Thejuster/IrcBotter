using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IrcbotPlugin
{
    public class PluginInfo : IPluginInfo
    {

        public string name
        {
            get { return "Prova"; }
        }

        public string author
        {
            get { return "Thejuster"; }
        }

        public string version
        {
            get { return "1.0"; }
        }

        public string RawData
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Process
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public void OnMessage(string RawData)
        {
            Console.WriteLine("Message From Plugin yea");
        }


        public void OnJoin(string RawData)
        {
            
        }

        public void OnQuit(string RawData)
        {
            
        }

        public void OnServerMessage(string RawData)
        {
            
        }

        public void OnCommand(string RawData)
        {
            
        }


        public void OnPrivateMessage(string RawData)
        {
            
        }
    }
}
