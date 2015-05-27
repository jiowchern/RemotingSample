using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {

            // 展示用簡易控制台
            var view = new Regulus.Utility.ConsoleViewer();
            var input = new Regulus.Utility.ConsoleInput(view);
            var console = new Regulus.Utility.Console(input,view);

            // 建立伺服器
            var server = new Regulus.Remoting.Soul.Native.Server(new Common.Main(view), 12345);

            bool enable = true;
            console.Command.Register("quit", () => { enable = false; });

            server.Launch();

            while(enable)
            {
                input.Update();
            }
            server.Shutdown();
        }
    }
}
