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
        Custom.ISample _Sample;
        public SampleClient(Regulus.Remoting.IAgent agent, Regulus.Utility.Console.IViewer view, Regulus.Utility.Command command)
        {
            _Counter = new Regulus.Utility.TimeCounter();
            this._Agent = agent;
            this._View = view;
            this._Command = command;

            _View.WriteLine("Start connection...");
            _Agent.QueryNotifier<Custom.ISample>().Supply += _GetSample;
            _Agent.Connect("127.0.0.1", 12345).OnValue += _ConnectResult;
            

        }        

        void _GetSample(Custom.ISample sample)
        {
            _View.WriteLine("Get Sample...");
            sample.Add(1, 2).OnValue += (result) => { _View.WriteLine(string.Format("Add(1 , 2) == {0}", result)); };
            sample.GetSubtractor().OnValue += _GetSubtractor;
            _Sample = sample;

            _View.WriteLine("Enter GetSeconds can obtain information on server-side object");
            _Command.Register("GetSeconds", () => { _View.WriteLine(string.Format("ElapsedSecond : {0}", _Sample.ElapsedSecond)); });

        }

        private void _GetSubtractor(Custom.ISubtractor subtractor)
        {
            _View.WriteLine("Get Subtractor...");
            subtractor.Sub(1, 2).OnValue += (result) => { _View.WriteLine(string.Format("Sub(1 , 2) == {0}", result)); };
        }

        private void _ConnectResult(bool success)
        {
            if (success)
            {
                _View.WriteLine("Connection Success.");                
            }                
            else
                _View.WriteLine("Connection Failed.");
        }
        
    }
}
