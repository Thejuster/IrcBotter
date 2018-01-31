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

            var displayConfirmBtn = new Button(1, 1, "Display Confirm", "displayConfirmBtn", this)
            {
                Action = delegate()
                {

                    IrcDraw(false);
                    var cf = new Confirm("Open System Menu?", this, ConsoleColor.White);                   
                    if (cf.ShowDialog() == ConsoleDraw.DialogResult.OK)
                    {
                        //Add Label
                        var lb = new Label("Channel List", 1, 1, "chlist", this);
                        var lb2 = new Label("Registred User", 1, 25, "rgnicklabel", this);
                        var lb3 = new Label("Boot Controls", 10, 1, "botctrllbl", this);
                        var lb4 = new Label("Allow Public Commands", 13, 5, "cb1lb", this);
                        var lb5 = new Label("Enable Opers Commands", 15, 5, "cb2lb", this);
                        var lb6 = new Label("Allow User Registration", 17, 5, "cb3lb", this);
                        var lb7 = new Label("Auto Reconnection", 19, 5, "cb3lb", this);

                        //Channel List
                        opts = bots.engine.Channels;
                        cb = new Dropdown(3, 1, opts, "1", this);

                        //Registred Nick
                        List<string> regnick = bots.engine.Nicks;
                        var rgnick = new Dropdown(3, 25, regnick, "regnick", this);
                        rgnick.DropdownItems = new List<DropdownItem>(regnick.Select(x => new DropdownItem(x, 10, "2", this)).ToArray());

               
                        
                        //CheckBox
                        var cb1 = new CheckBox(13, 1, "cb1", this) { Checked = true };
                        var cb2 = new CheckBox(15, 1, "cb2", this) { Checked = true };
                        var cb3 = new CheckBox(17, 1, "cb3", this) { Checked = false };
                        var cb4 = new CheckBox(19,1,"cb4",this) {Checked = true};



                        CurrentlySelected = cb;
                        
                        //Add Controls
                        Inputs.Add(cb);
                        Inputs.Add(lb);
                        Inputs.Add(rgnick);
                        Inputs.Add(lb2);
                        Inputs.Add(lb3);
                        Inputs.Add(cb1);
                        Inputs.Add(lb4);
                        Inputs.Add(cb2);
                        Inputs.Add(lb5);
                        Inputs.Add(cb3);
                        Inputs.Add(lb6);
                        Inputs.Add(cb4);
                        Inputs.Add(lb7);

                        Draw();
                    }
                    else
                    {
                        IrcDraw(true);
                    }
                }
            };

         
    
  
            CurrentlySelected = displayConfirmBtn;
            var cc = FromColor(System.Drawing.Color.Gray);
            this.BackgroundColour = cc;

            Draw();
            MainLoop();


        }



        /// <summary>
        /// Enable System Menu Drawing
        /// </summary>
        /// <param name="enable">True for enable rendering</param>
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
        /// Get Color for Console in RGB Value
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
