using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public interface ISubtractor
    {
        Regulus.Remoting.Value<int> Sub(int num1,int num2);

    }
}
