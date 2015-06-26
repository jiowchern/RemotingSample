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
           
            server.Launch();

            var agent = Regulus.Remoting.Ghost.Native.Agent.Create();

            var client = new Client.SampleClient(agent, view, command);

            agent.Launch();

            while (view.IsDone() == false)
            {
                agent.Update();
            }

            agent.Shutdown();            

            server.Shutdown();
        }
    }
}
