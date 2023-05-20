using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CalcLibrary.AdapterDll
{
    internal class TargetBinOperation : ITargetBinOperaton
    {
        Type[] types;
        MethodInfo method;
        public TargetBinOperation()
        {
            Assembly SampleAssembly = Assembly.LoadFrom("BinCalc.dll");
            types = SampleAssembly.GetTypes();
            method = types[0].GetMethods()[0];

            
        }
        public double Calc(double par1, double par2, string operation)
        {
            if (operation == "xor")
                operation = "^";
            else if (operation == "or")
                operation = "|";

            object obj = Activator.CreateInstance(types[0]);
            string s = (string)method.Invoke(obj, new object[] { par1.ToString() + operation + par2.ToString() });
            return double.Parse(s);
        }
    }
}
