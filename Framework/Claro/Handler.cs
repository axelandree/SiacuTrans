using System;

namespace Claro
{
    #region CommandEventArgs

    public class CommandEventArgs :
        EventArgs
    {
        #region [ Constructors ]

        public CommandEventArgs() { }

        public CommandEventArgs(string commandName, object commandArgument)
        {
            CommnadName = commandName;
            CommandArgument = commandArgument;
        }

        #endregion

        #region [ Properties ]

        public object CommandArgument { get; set; }

        public string CommnadName { get; set; }

        #endregion

    }
    #endregion

    #region CommandEventHandler

    public delegate void CommandEventHandler(object sender, CommandEventArgs e);
    
    #endregion
}
