using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample
{
    class User : Regulus.Utility.IUpdatable
    {
        private Regulus.Remoting.ISoulBinder _Binder;
        Regulus.Game.StageMachine _Machine;

        public User(Regulus.Remoting.ISoulBinder binder)
        {
            _Machine = new Regulus.Game.StageMachine();
            _Binder = binder;
        }
        bool Regulus.Utility.IUpdatable.Update()
        {
            _Machine.Update();
            return true;
        }

        void Regulus.Framework.ILaunched.Launch()
        {
            _ToVerify();
        }

        private void _ToVerify()
        {
            var stage = new Verify(_Binder);
            stage.DoneEvent += _ToGame;
            _Machine.Push(stage);
        }

        private void _ToGame()
        {
            var stage = new Player(_Binder);
            stage.BackEvent += _ToVerify;
            _Machine.Push(stage);
        }

        void Regulus.Framework.ILaunched.Shutdown()
        {
            _Machine.Termination();
        }

        
    }
}
