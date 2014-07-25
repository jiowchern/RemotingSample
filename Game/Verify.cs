using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Sample
{
    public class Verify : IVerify , Regulus.Game.IStage
    {
        public delegate void OnDone();
        public event OnDone DoneEvent;
        private Regulus.Remoting.ISoulBinder _Binder;

        public Verify(Regulus.Remoting.ISoulBinder _Binder)
        {         
            this._Binder = _Binder;
        }
        Regulus.Remoting.Value<bool> IVerify.Login(string account, string password)
        {
            if (account == "1")
            {
                DoneEvent();
                return true;
            }
            return false;
        }

        void Regulus.Game.IStage.Enter()
        {
            _Binder.Bind<IVerify>(this);
        }

        void Regulus.Game.IStage.Leave()
        {
            _Binder.Unbind<IVerify>(this);
        }

        void Regulus.Game.IStage.Update()
        {
            
        }
    }
}
