using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample
{
    using Regulus.Remoting;
    public interface IPlayer
    {
        void Back();
        Value<int> Add(int num1,int num2);
    }
}
