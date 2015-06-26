using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    class AssertChecker : Regulus.Utility.Console.IViewer
    {

        List<string> _Messages;
        public AssertChecker()
        {
            _Messages = new List<string>(new [] 
            {
                "There is a user to join.",
                "Start connection...",
                "Get Sample...",
                "Enter GetSeconds can obtain information on server-side object",
                "Get Subtractor...",
                "Connection Success."
            });
        }


        void Regulus.Utility.Console.IViewer.Write(string message)
        {
            _CheckMessage(message);
        }

        void Regulus.Utility.Console.IViewer.WriteLine(string message)
        {
            _CheckMessage(message);
        }

        private void _CheckMessage(string message)
        {
            if (_Messages.Exists(m => m == message))
                _Messages.Remove(message);
        }

        internal bool IsDone()
        {
            return _Messages.Count == 0;
        }
    }
}
