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

            var client = new SampleClient(agent, view, console.Command);

            agent.Launch();
            client.Launch();
            while (enable)
            {                
                agent.Update();
                input.Update();
            }
            agent.Shutdown();
            client.Shutdown();
        }
    }
}
