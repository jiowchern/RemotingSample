using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class ConnectTest
    {
        [TestMethod,Timeout(10000)]
        public void TestConnect()
        {
            var command = new Regulus.Utility.Command();
            var view = new AssertChecker();
            var server = new Regulus.Remoting.Soul.Native.Server(new Custom.Appliction(view), 12345);

            // 啟動伺服器
            server.Launch();

            // 建立代理者
            var agent = Regulus.Remoting.Ghost.Native.Agent.Create();

            // 自定義的客戶端邏輯
            var client = new Client.SampleClient(agent, view, command);

            // 啟動代理器
            agent.Launch();

            while (view.IsDone() == false)
            {
                agent.Update();
            }


            // 關閉代理器
            agent.Shutdown();            

            // 關閉伺服器
            server.Shutdown();
        }
    }
}
