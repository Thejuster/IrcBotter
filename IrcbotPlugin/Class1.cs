using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IrcbotPlugin
{
    public interface IPluginInfo
    {
        /// <summary>
        /// Plugin Name
        /// </summary>
        string name { get; }

        /// <summary>
        /// Public Author
        /// </summary>
        string author { get; }

        /// <summary>
        /// Plugin Version
        /// </summary>
        string version { get; }
        
        /// <summary>
        /// RawData
        /// </summary>
        string RawData { get; set; }

        /// <summary>
        /// Result of Processing
        /// </summary>
        string Process { get; set; }

        /// <summary>
        /// On Message Interception
        /// </summary>
        /// <param name="RawData">RawData</param>
        void OnMessage(string RawData);

        /// <summary>
        /// On Join Interception
        /// </summary>
        /// <param name="RawData">RawData</param>
        void OnJoin(string RawData);

        /// <summary>
        /// On Quit Interception
        /// </summary>
        /// <param name="RawData">RawData</param>
        void OnQuit(string RawData);

        /// <summary>
        /// On Server Message Interception
        /// </summary>
        /// <param name="RawData">RawData</param>
        void OnServerMessage(string RawData);

        /// <summary>
        /// On Command Interception
        /// </summary>
        /// <param name="RawData">RawData</param>
        void OnCommand(string RawData);

        /// <summary>
        /// On Private Message Interception
        /// </summary>
        /// <param name="RawData">RawData</param>
        void OnPrivateMessage(string RawData);
    }
}
