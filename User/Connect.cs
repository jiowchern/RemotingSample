using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample
{
    class Connect : Regulus.Game.IStage
    {
        private Regulus.Utility.Console.IViewer _View;        
        private Regulus.Utility.Command _Command;
        Regulus.Remoting.Ghost.Native.Agent _Agent;
        

        
        public delegate void OnDone();
        public event OnDone DoneEvent;
        public Connect(Regulus.Remoting.Ghost.Native.Agent agent,Regulus.Utility.Console.IViewer view, Regulus.Utility.Command command)
        {            
            this._View = view;            
            this._Command = command;
            _Agent = agent;        
        }

        void Regulus.Game.IStage.Enter()
        {            
            _ShowCommand();
        }

        private void _ShowCommand()
        {
            _Command.Register<string, int>("Connect", _Connect);
        }

        private void _Connect(string ip,int port)
        {
            var result = _Agent.Connect(ip, port);
            result.OnValue += _Result;            
        }

        private void _HideCommand()
        {
            _Command.Unregister("Connect");
        }

        private void _Result(bool success)
        {
            _View.WriteLine("連線" + (success ? "成功" : "失敗"));
            if(success)
            {
                DoneEvent();
            }            
        }

        void Regulus.Game.IStage.Leave()
        {
            _HideCommand();            
        }

        void Regulus.Game.IStage.Update()
        {            
        }
    }
}
