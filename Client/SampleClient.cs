using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    class SampleClient
    {
        private Regulus.Remoting.IAgent _Agent;
        private Regulus.Utility.Console.IViewer _View;
        private Regulus.Utility.Command _Command;

        Regulus.Utility.TimeCounter _Counter;
        Common.ISample _Sample;
        public SampleClient(Regulus.Remoting.IAgent agent, Regulus.Utility.Console.IViewer view, Regulus.Utility.Command command)
        {
            _Counter = new Regulus.Utility.TimeCounter();
            this._Agent = agent;
            this._View = view;
            this._Command = command;
        }
        internal void Shutdown()
        {
            _Agent.Connect("127.0.0.1", 12345).OnValue -= _ConnectResult;
            _Agent.QueryNotifier<Common.ISample>().Supply -= _GetSample;
        }
        internal void Launch()
        {
            _View.WriteLine("開始連線...");
            _Agent.Connect("127.0.0.1", 12345).OnValue += _ConnectResult;
            _Agent.QueryNotifier<Common.ISample>().Supply += _GetSample;
        }

        void _GetSample(Common.ISample sample)
        {
            _View.WriteLine("取得Sample...");
            sample.Add(1, 2).OnValue += (result) => { _View.WriteLine(string.Format("Add(1 , 2) == {0}", result)); };
            sample.GetSubtractor().OnValue += _GetSubtractor;
            _Sample = sample;

            _View.WriteLine("輸入GetSeconds可以取得伺服器端物件的資料");
            _Command.Register("GetSeconds", () => { _View.WriteLine(string.Format("ElapsedSecond : {0}", _Sample.ElapsedSecond)); });

        }

        private void _GetSubtractor(Common.ISubtractor subtractor)
        {
            _View.WriteLine("取得Subtractor...");
            subtractor.Sub(1, 2).OnValue += (result) => { _View.WriteLine(string.Format("Sub(1 , 2) == {0}", result)); };
        }

        private void _ConnectResult(bool success)
        {
            if (success)
            {
                _View.WriteLine("連線成功.");                
            }                
            else
                _View.WriteLine("連線失敗.");
        }

        

        
    }
}
