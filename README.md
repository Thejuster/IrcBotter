# IrcBotter
An incredible Irc Bot Engine for make your awesome bot.

### Feature Developed and in Develop

- [X] Plugin Host
- [X] Managed Events
- [X] Automatic Reconnection
- [X] Spam / Flood Protection
- [X] Manage Custom Commands
- [X] Interactive DOS GUI
- [X] Visual Configuration
- [X] User Registration
- [X] User Level
- [X] Privilege
- [ ] Shell
- [ ] Web Api
- [ ] JSON Parser
- [ ] Serialization / Deserialization
- [X] Simple Example Bot



### How to Work?

![Diagram](http://pichoster.net/images/2018/02/02/bc5f8b27a3bae08bfcd890a88b5d263b.png)



### Compiled Plugin

![plugin](http://pichoster.net/images/2018/02/02/ff96d221305eeaf4d58e5d7ed609f726.png)



### Welcome Screen

Autostart if config file called **data.txt** is stored in bin\Debug

![First Screeb](http://pichoster.net/images/2018/02/02/787f224cff9b55fe6a7275ed30a50cb4.png)



### Terminal Screen

![Terminal](http://pichoster.net/images/2018/02/02/c507d42531b7fad09365e0652dba47f7.png)

If press **Enter** key, Enter a System GUI Menu.



![gui](http://pichoster.net/images/2018/02/02/14ce96081639c1da556f830b27e5ba5b.png)



### Customize your Bot

IrcBotter is based on IRC Command and interpreter.
All recursive operation are configured, you can just explain IrcEngine Events.
For example:

```csharp

       engine.OnJoin += new IrcEngine.JoinHandler(engine_OnJoin);
       engine.OnMessage += new IrcEngine.MessageHandler(engine_OnMessage);
       engine.OnQuit += new IrcEngine.QuitHandler(engine_OnQuit);
       engine.OnServerMessage += new IrcEngine.ServerMessageHandler(engine_OnServerMessage);
       engine.OnCommand += new IrcEngine.CommandHandler(engine_OnCommand);
       engine.OnPrivateMessage += new IrcEngine.PrivateMessageHandler(engine_OnPrivateMessage);
       
       
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
                engine.Queryes.Add(new IrcEngine.Query() { username = e.GetUser(), message = "Hello " + e.GetUser() + " how are               you?" });               
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
            
            //Custom command whit ! identificator
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
            
```

### Special access to the void

Only registred user and whit him level is > specified in LevelRequired 
have access to this void and execute contents.

```csharp


[UserLevel(LevelRequired = 50)]
public void MyVoid(string user,string RawData)
{
  //ToDo
}


```
