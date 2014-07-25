using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample
{
    using Regulus.Extension;
    class Verify : Regulus.Game.IStage
    {
        private Regulus.Remoting.IAgent _Agent;
        private Regulus.Utility.Console.IViewer _View;        
        private Regulus.Utility.Command _Command;
        Sample.IVerify _Verify;


        public delegate void OnDone();
        public event OnDone DoneEvent;

        public Verify(Regulus.Remoting.IAgent agent, Regulus.Utility.Console.IViewer view, Regulus.Utility.Command command)
        {            
            this._Agent = agent;
            this._View = view;            
            this._Command = command;
        }
        
        void Regulus.Game.IStage.Enter()
        {
            var provider = _Agent.QueryProvider<Sample.IVerify>();
            provider.Supply += _Obtain;
        }

        private void _Obtain(Sample.IVerify verify)
        {
            _Verify = verify;
            _ShowCommand();
        }

        private void _ShowCommand()
        {
            _Command.RemotingRegister<string, string , bool >("Login", _Verify.Login , _LoginResult );
            
        }

        private void _LoginResult(bool success)
        {
            _View.WriteLine("登入" + (success ? "成功" : "失敗"));

            if (success)
            {
                DoneEvent();
            }            
        }
        

        private void _HideCommand()
        {
            _Command.Unregister("Login");
        }

        void Regulus.Game.IStage.Leave()
        {
            var provider = _Agent.QueryProvider<Sample.IVerify>();
            provider.Supply -= _Obtain;
            _HideCommand();
        }

        void Regulus.Game.IStage.Update()
        {
            
        }
    }
}
