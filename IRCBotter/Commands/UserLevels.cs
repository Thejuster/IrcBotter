using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRCBotter.Commands
{
    [AttributeUsage(AttributeTargets.All,AllowMultiple=true)]
    class UserLevels : Attribute
    {
        public int LevelRequired { get; set; }

    }

}
