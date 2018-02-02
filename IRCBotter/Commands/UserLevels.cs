using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*************************************************
 * Irc Botter - By Thejuster
 * Under GPL GNU v3 Licence
 * Plase report bug, contribution and new feature
 * Enjoy :)
 * **********************************************/


namespace IRCBotter.Commands
{
 
    [AttributeUsage(AttributeTargets.All ,AllowMultiple=true)]
    class UserLevel : Attribute
    {
        public int LevelRequired { get; set; }
    }



}
