using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    class Program
    {

        static void Main(string[] args)
        {
            var view = new Regulus.Utility.ConsoleViewer();
            var input = new Regulus.Utility.ConsoleInput(view);
            var client = new Sample.Client(view, input);

            client.Launch();
            while(client.Process())
            {
                input.Update();
            }
            client.Shutdown();
        }

        
    }
}
