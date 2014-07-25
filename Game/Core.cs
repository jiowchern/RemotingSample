using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Sample
{
    public class Core : Regulus.Game.ICore
    {
        Regulus.Utility.Updater _Users;

        void Regulus.Game.ICore.ObtainController(Regulus.Remoting.ISoulBinder binder)
        {
            var user = new User(binder);
            binder.BreakEvent += () =>
            {
                _Users.Remove(user);
            };

            _Users.Add(user);
        }

        bool Regulus.Utility.IUpdatable.Update()
        {
            _Users.Update();
            return true;
        }

        void Regulus.Framework.ILaunched.Launch()
        {
            _Users = new Regulus.Utility.Updater();
        }

        void Regulus.Framework.ILaunched.Shutdown()
        {
            _Users.Shutdown();
        }
    }
}
