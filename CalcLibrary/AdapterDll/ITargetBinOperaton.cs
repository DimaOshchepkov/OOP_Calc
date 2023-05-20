using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcLibrary.AdapterDll
{
    internal interface ITargetBinOperaton
    {
        double Calc(double par1, double par2, string operation);
    }
}
