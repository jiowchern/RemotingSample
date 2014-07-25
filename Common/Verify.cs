using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Sample
{
    using Regulus.Remoting;
    public interface IVerify
    {
        Value<bool> Login(string account , string password);
    }
}
