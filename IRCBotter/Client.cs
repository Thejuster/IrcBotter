using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleDraw.Windows.Base;
using ConsoleDraw;
using ConsoleDraw.Inputs;
using ConsoleDraw.Windows;
using System.IO;
using Form = System.Windows.Forms;

namespace IRCBotter
{
    public class Client : FullWindow
    {
        public IrcBot bots;
        public Client(string ip,string port,IrcBot bot) : base(0, 0, Console.WindowWidth, Console.WindowHeight, null)
        {
            bots = bot;
            var displayAlertBtn = new Button(2, 20, "Display Alert", "displayAlertBtn", this) { Action = delegate() { new Alert("This is an Alert!", this, ConsoleColor.White); } };

            List<string> opts = new List<string>() { "hello", "world","yawww","Yammmm" };
            var cb = new Dropdown(0, 0, opts, "1", this);
            cb.DropdownItems = new List<DropdownItem>(opts.Select(_ => new DropdownItem(_, 10, "2", this)).ToArray());
            cb.Selectable = true;

            var fileSelect = new FileBrowser(26, 2, 40, 10, Directory.GetCurrentDirectory(), "fileSelect", this, true);

            var displayConfirmBtn = new Button(1, 1, "Display Confirm", "displayConfirmBtn", this)
            {
                Action = delegate()
                {

                    //var color = ClosestConsoleColor(211, 211, 211);
                    //this.BackgroundColour = color;

                    IrcDraw(false);

                    var cf = new Confirm("Open System Menu?", this, ConsoleColor.White);
                    
                    if (cf.ShowDialog() == ConsoleDraw.DialogResult.OK)
                    {
                       

                        CurrentlySelected = cb;
                        Inputs.Add(cb);
                        Inputs.Add(fileSelect);
                        Draw();
                    }
                    else
                    {
                        IrcDraw(true);
                    }
                }
            };




           

            

           // Inputs.Add(displayAlertBtn);
            //Inputs.Add(displayConfirmBtn);
         
    
  
            CurrentlySelected = displayConfirmBtn;
            var cc = FromColor(System.Drawing.Color.Gray);
            this.BackgroundColour = cc;

            Draw();
            MainLoop();


        }


        public void IrcDraw(bool enable)
        {
            if (!enable)
            {
                Action action = () => { bots.Menu = true; };
                action.Invoke();
            }
            else
            {
                Action action = () => { bots.Menu = false; };
                action.Invoke();
                Console.CursorLeft = 0;
            }
        }



        /// <summary>
        /// Return Color for Console in RGB Value
        /// </summary>
        /// <param name="r">R</param>
        /// <param name="g">G</param>
        /// <param name="b">B</param>
        /// <returns>Console Color Code</returns>
        public static System.ConsoleColor FromColor(System.Drawing.Color c) 
        {
                int index = (c.R > 128 | c.G > 128 | c.B > 128) ? 8 : 0; // Bright bit
                index |= (c.R > 64) ? 4 : 0; // Red bit
                index |= (c.G > 64) ? 2 : 0; // Green bit
                index |= (c.B > 64) ? 1 : 0; // Blue bit
                return (System.ConsoleColor)index;
        }
    }
}
