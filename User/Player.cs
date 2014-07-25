using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample
{
    using Regulus.Extension;

    class Player : Regulus.Game.IStage
    {
        private Regulus.Remoting.IAgent _Agent;
        private Regulus.Utility.Console.IViewer _View;
        private Regulus.Utility.Command _Command;
        Sample.IPlayer _Player;

        public delegate void OnDone();
        public event OnDone DoneEvent;

        public Player(Regulus.Remoting.IAgent agent, Regulus.Utility.Console.IViewer view, Regulus.Utility.Command command)
        {            
            this._Agent = agent;
            this._View = view;
            this._Command = command;
        }


        void Regulus.Game.IStage.Enter()
        {
            _Agent.QueryProvider<Sample.IPlayer>().Supply += _Obtain;
            _Agent.QueryProvider<Sample.IPlayer>().Unsupply += _Remove;
        }

        private void _Remove(Sample.IPlayer player)
        {
            DoneEvent();
        }

        private void _Obtain(Sample.IPlayer player)
        {
            _Player = player;
            _ShowCommand();
        }

        private void _ShowCommand()
        {
            _Command.RemotingRegister<int, int, int>("Add", _Player.Add , _AddResult);
            _Command.Register("Logout", _Player.Back );
        }

        private void _AddResult(int result)
        {
            _View.WriteLine("回傳結果 : " + result);
        }

        

        void Regulus.Game.IStage.Leave()
        {
            _HideCommand();
            _Agent.QueryProvider<Sample.IPlayer>().Supply -= _Obtain;
            _Agent.QueryProvider<Sample.IPlayer>().Unsupply -= _Remove;
        }

        private void _HideCommand()
        {
            _Command.Unregister("Add");
            _Command.Unregister("Logout");
        }

        void Regulus.Game.IStage.Update()
        {
            
        }
    }
}
