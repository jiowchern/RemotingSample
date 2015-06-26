using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var view = new Regulus.Utility.ConsoleViewer();
            var input = new Regulus.Utility.ConsoleInput(view);
            var console = new Regulus.Utility.Console(input,view);

            // 建立伺服器
            // Create Server
            // Argument1 : 您的自定應用程式邏輯
            //             your custom application logic
            //             該物件需要繼承Regulus.Remoting.ICore 
            //             The object needs to inherit Regulus.Remoting.ICore
            //
            // Argument2 : 端口
            //             Port
            var server = new Regulus.Remoting.Soul.Native.Server(new Custom.Appliction(view), 12345);

            bool enable = true;

            
            console.Command.Register("quit", () => { enable = false; });
            
            server.Launch();

            while(enable)
            {
                // 按鍵訊息刷新
                // Key message Refresh
                input.Update();
            }
            
            server.Shutdown();
        }
    }
}
