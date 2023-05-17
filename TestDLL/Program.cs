using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestDLL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Assembly SampleAssembly = Assembly.LoadFrom("BinCalc.dll");
            Type[] types = SampleAssembly.GetTypes();
            foreach (Type type in types)
            {
                Console.WriteLine(type.Name + "\t" + type.FullName);
                Console.WriteLine(type.GetFields().ToString());
                Console.WriteLine(type.GetMethods().ToString());
            }
            Console.ReadKey();
        }
    }
}
