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
                "有一位使用者加入",
                "開始連線...",
                "取得Sample...",
                "輸入GetSeconds可以取得伺服器端物件的資料",
                "取得Subtractor...",
                "連線成功."
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
