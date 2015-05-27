using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Custom
{


    public class Appliction : Regulus.Remoting.ICore, Custom.ISample, Custom.ISubtractor
    {
        Regulus.Utility.Console.IViewer _View;
        Regulus.Utility.TimeCounter _TimeCounter;

        public Appliction(Regulus.Utility.Console.IViewer view)
        {
            _View = view;
            _TimeCounter = new Regulus.Utility.TimeCounter();
        }
        


        //系統初始化會呼叫此方法
        void Regulus.Framework.IBootable.Launch()
        {
            _TimeCounter.Reset();
        }

        //系統關閉時會呼叫此方法
        void Regulus.Framework.IBootable.Shutdown()
        {
            
        }

        Regulus.Remoting.Value<int> ISample.Add(int num1, int num2)
        {
            return num1 + num2;
        }

        void Regulus.Remoting.ICore.ObtainBinder(Regulus.Remoting.ISoulBinder binder)
        {


            binder.Bind<Custom.ISample>(this);
            binder.BreakEvent += () => { _View.WriteLine("有一位使用者離開"); };
            _View.WriteLine("有一位使用者加入");
        }


        /*
         * 系統每次刷新皆會呼叫此方法
         * 每秒刷新次數不固定。
         */
        bool Regulus.Utility.IUpdatable.Update()
        {
            //傳回false系統將會停止
            return true;
        }

        float ISample.ElapsedSecond
        {
            get { return _TimeCounter.Second; }
        }

        Regulus.Remoting.Value<ISubtractor> ISample.GetSubtractor()
        {
            return this;
        }

        Regulus.Remoting.Value<int> ISubtractor.Sub(int num1, int num2)
        {
            return num1 - num2;

        }
    }
}
