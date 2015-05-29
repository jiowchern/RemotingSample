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

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            // 展示用簡易控制台
            var view = new Regulus.Utility.ConsoleViewer();
            var input = new Regulus.Utility.ConsoleInput(view);
            var console = new Regulus.Utility.Console(input,view);

            // 建立伺服器
            // 參數1 : 您的自定應用程式邏輯
            //         該物件需要繼承Regulus.Remoting.ICore 
            //
            // 參數2 : 監聽連線的Port
            var server = new Regulus.Remoting.Soul.Native.Server(new Custom.Appliction(view), 12345);

            bool enable = true;
            // 註冊關閉命令給Command
            console.Command.Register("quit", () => { enable = false; });


            // 設定伺服器端口        
            // 與使用者自定義的物件互動(Custom.Appliction)
            server.Launch();

            while(enable)
            {
                // 按鍵訊息刷新
                input.Update();
            }

            //關閉伺服器
            server.Shutdown();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
