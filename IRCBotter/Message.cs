using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRCBotter
{
    /// <summary>
    /// Message Class
    /// </summary>
    public static class Message
    {
        /// <summary>
        /// Debug Message
        /// </summary>
        /// <param name="message">Text</param>
        public static void Debug(string message)
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[DEBUG]: ");
            Console.ForegroundColor = old;
            Console.WriteLine(message);
        }

        /// <summary>
        /// Error Message
        /// </summary>
        /// <param name="message">Text</param>
        public static void Error(string message)
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[ERROR]: ");
            Console.ForegroundColor = old;
            Console.WriteLine(message);
        }

        /// <summary>
        /// Info Message
        /// </summary>
        /// <param name="message">Text</param>
        public static void Info(string message)
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[INFO]: ");
            Console.ForegroundColor = old;
            Console.WriteLine(message);
        }

        /// <summary>
        /// Notice Message
        /// </summary>
        /// <param name="message">Text</param>
        public static void Notice(string message)
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[NOTICE]: ");
            Console.ForegroundColor = old;
            Console.WriteLine(message);
        }

        /// <summary>
        /// Draw Logo Splash Screen
        /// </summary>
        public static void Logo()
        {
            Console.WriteLine("");
            Console.WriteLine("  ██╗██████╗  ██████╗██████╗  ██████╗ ████████╗████████╗███████╗██████╗ ");
            Console.WriteLine("  ██║██╔══██╗██╔════╝██╔══██╗██╔═══██╗╚══██╔══╝╚══██╔══╝██╔════╝██╔══██╗");
            Console.WriteLine("  ██║██████╔╝██║     ██████╔╝██║   ██║   ██║      ██║   █████╗  ██████╔╝");
            Console.WriteLine("  ██║██╔══██╗██║     ██╔══██╗██║   ██║   ██║      ██║   ██╔══╝  ██╔══██╗");
            Console.WriteLine("  ██║██║  ██║╚██████╗██████╔╝╚██████╔╝   ██║      ██║   ███████╗██║  ██║");
            Console.WriteLine("  ╚═╝╚═╝  ╚═╝ ╚═════╝╚═════╝  ╚═════╝    ╚═╝      ╚═╝   ╚══════╝╚═╝  ╚═╝");
            Console.WriteLine("");
        }


        /// <summary>
        /// Message sent from the user
        /// </summary>
        /// <param name="message"><Text/param>
        public static void Text(string message)
        {
            ConsoleColor old = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("[Message] ");
            Console.ForegroundColor = old;
            Console.WriteLine(message);
        }


        /// <summary>
        /// Message from the server
        /// </summary>
        /// <param name="message">TEXT</param>
        public static void MOTD(string message)
        {
            ConsoleColor c = Console.BackgroundColor;
            ConsoleColor c2 = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("[MOTD] {0}", message);

            Console.BackgroundColor = c;
            Console.ForegroundColor = c2;
        }

    }
}
