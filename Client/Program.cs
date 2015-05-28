using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // 展示用簡易控制台
            var view = new Regulus.Utility.ConsoleViewer();
            var input = new Regulus.Utility.ConsoleInput(view);
            var console = new Regulus.Utility.Console(input, view);

            // 建立代理者
            var agent = Regulus.Remoting.Ghost.Native.Agent.Create();

            bool enable = true;
            console.Command.Register("quit", () => { enable = false; });

            // 啟動代理器
            agent.Launch();

            // 自定義的客戶端邏輯
            var client = new SampleClient(agent, view, console.Command);
            
            while (enable)
            {                
                // 代理器更新
                // 重要 : 需要呼叫他來刷新封包接收
                agent.Update();

                input.Update();
            }

            // 關閉代理器
            agent.Shutdown();            
        }
    }
}
