using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample
{
    class Player : Regulus.Game.IStage , IPlayer
    {
        private Regulus.Remoting.ISoulBinder _Binder;


        public delegate void OnBack();
        public event OnBack BackEvent;
        public Player(Regulus.Remoting.ISoulBinder binder)
        {            
            this._Binder = binder; 
        }

        void Regulus.Game.IStage.Enter()
        {
            _Binder.Bind<IPlayer>(this);
        }

        void Regulus.Game.IStage.Leave()
        {
            _Binder.Unbind<IPlayer>(this);
        }

        void Regulus.Game.IStage.Update()
        {
            
        }

        void IPlayer.Back()
        {
            BackEvent();
        }

        Regulus.Remoting.Value<int> IPlayer.Add(int num1, int num2)
        {
            return num1 + num2;
        }
    }
}
