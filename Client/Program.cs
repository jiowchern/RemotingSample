using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // 範例用簡易控制台   
            // example with a simple console
            var view = new Regulus.Utility.ConsoleViewer();
            var input = new Regulus.Utility.ConsoleInput(view);
            var console = new Regulus.Utility.Console(input, view);
                
            var agent = Regulus.Remoting.Ghost.Native.Agent.Create();

            bool enable = true;
            console.Command.Register("quit", () => { enable = false; });

            agent.Launch();

            // 自定義的客戶端邏輯
            // Custom client logic
            var client = new SampleClient(agent, view, console.Command);
            
            while (enable)
            {                                
                // Important: call it to refresh the packet receive
                agent.Update();

                input.Update();
            }
            
            agent.Shutdown();            
        }
    }
}
