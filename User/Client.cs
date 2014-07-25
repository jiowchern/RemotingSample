using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample
{
    public class Client
    {
        Regulus.Remoting.Ghost.Native.Agent _Agent;
        Regulus.Utility.Updater _Updater; 

        Regulus.Utility.Console _Console;
        Regulus.Utility.Console.IViewer _View;
        Regulus.Utility.Console.IInput _Input;
                

        Regulus.Game.StageMachine _Machine;

        bool _Run;


        public Client(Regulus.Utility.Console.IViewer view , Regulus.Utility.Console.IInput input)
        {            
            _View = view;
            _Input = input;
            _Agent = new Regulus.Remoting.Ghost.Native.Agent();
            _Updater = new Regulus.Utility.Updater();
        }

        private void _Release()
        {
            _Console.Command.Unregister("quit");
            _Machine.Termination();
            _Updater.Remove(_Agent);
            _Updater.Shutdown();

            _View.WriteLine("系統關閉.");
        }

        private bool _Process()
        {
            _Updater.Update();
            _Machine.Update();
            return _Run;
        }

        private void _Initial()
        {
            _View.WriteLine("系統啟動.");
            _Updater.Add(_Agent);

            _Run = true;
            _Machine = new Regulus.Game.StageMachine();            
            
            _Console = new Regulus.Utility.Console(_Input, _View);

            _Console.Command.Register("quit", () => { _Run = false; });

            _ToConnect();
            
            
        }

        private void _ToConnect()
        {
            var stage = new Connect(_Agent , _View , _Console.Command );
            stage.DoneEvent += _ToVerify;
            _Machine.Push(stage);
        }

        private void _ToVerify()
        {
            var stage = new Verify(_Agent , _View, _Console.Command);
            stage.DoneEvent += _ToPlayer;
            _Machine.Push(stage);
        }

        private void _ToPlayer()
        {
            var stage = new Player(_Agent, _View, _Console.Command);
            stage.DoneEvent += _ToVerify;
            _Machine.Push(stage);
        }

        public void Launch()
        {
            _Initial();
        }

        public bool Process()
        {
            
            return _Process();
        }

        public void Shutdown()
        {
            _Release();
        }
    }
}
