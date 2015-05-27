using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    class SampleClient
    {
        private Regulus.Remoting.IAgent agent;
        private Regulus.Utility.Console.IViewer view;
        private Regulus.Utility.Command command;

        Regulus.Utility.TimeCounter _Counter;
        Common.ISample _Sample;
        public SampleClient(Regulus.Remoting.IAgent agent, Regulus.Utility.Console.IViewer view, Regulus.Utility.Command command)
        {
            _Counter = new Regulus.Utility.TimeCounter();
            this.agent = agent;
            this.view = view;
            this.command = command;
        }
        internal void Shutdown()
        {
            agent.Connect("127.0.0.1", 12345).OnValue -= _ConnectResult;
            agent.QueryNotifier<Common.ISample>().Supply -= _GetSample;
        }
        internal void Launch()
        {
            view.WriteLine("開始連線...");
            agent.Connect("127.0.0.1", 12345).OnValue += _ConnectResult;
            agent.QueryNotifier<Common.ISample>().Supply += _GetSample;
        }

        void _GetSample(Common.ISample sample)
        {
            view.WriteLine("取得Sample...");
            sample.Add(1, 2).OnValue += (result) => { view.WriteLine(string.Format("Add(1 , 2) == {0}", result)); };
            sample.GetSubtractor().OnValue += _GetSubtractor;
            _Sample = sample;

            view.WriteLine("輸入GetSeconds可以取得伺服器端物件的資料");
            command.Register("GetSeconds", () => { view.WriteLine(string.Format("ElapsedSecond : {0}", _Sample.ElapsedSecond)); });

        }

        private void _GetSubtractor(Common.ISubtractor subtractor)
        {
            view.WriteLine("取得Subtractor...");
            subtractor.Sub(1, 2).OnValue += (result) => { view.WriteLine(string.Format("Sub(1 , 2) == {0}", result)); };
        }

        private void _ConnectResult(bool success)
        {
            if (success)
            {
                view.WriteLine("連線成功.");                
            }                
            else
                view.WriteLine("連線失敗.");
        }

        

        
    }
}
