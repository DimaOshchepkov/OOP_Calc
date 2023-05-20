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

            Console.WriteLine("Все типы в сборке:");
            foreach (Type type in types)
            {
                Console.WriteLine(type.Name + "\t" + type.FullName);
            }

            Console.WriteLine("\nВсе поля сборки:");
            FieldInfo[] ts;
            for (int i = 0; i < types.Length; i++)
            {
                ts = types[i].GetFields();
                foreach (var t in ts) Console.WriteLine($"{types[i].Name}:\t{t.Name}");
            }

            Console.WriteLine("\nМетоды сборки:");
            MethodInfo[] ms;
            for (int i = 0; i < types.Length; i++)
            {
                ms = types[i].GetMethods();
                foreach (var m in ms) Console.WriteLine($"{types[i].Name}:\t{m.Name}\t{ms[i].DeclaringType}");
            }

            MethodInfo method = types[0].GetMethods()[0];
            foreach (var x in types)
            {
                Console.WriteLine($"{x.Name}");
                MethodInfo[] methods = x.GetMethods();

                foreach (var m in methods)
                {
                    Console.WriteLine($"\t{m.Name}");
                    foreach (var l in m.GetParameters()) Console.WriteLine($"\t\t{l}");

                    Console.WriteLine($"\t\t\t{m.ReturnType}");
                }
            }

            method = types[0].GetMethods()[0];

            object obj = Activator.CreateInstance(types[0]);
            string s = (string)method.Invoke(obj, new object[] { "23|4" });

            Console.WriteLine($"\n\n{s}");


            Console.ReadKey();
        }
    }
}
