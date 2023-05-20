using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcLibrary.AdapterDll
{
    /// <summary>
    /// Интерфейс адаптера бибилиотеки BinOp.dll
    /// </summary>
    internal interface ITargetBinOperaton
    {
        /// <summary>
        /// Производит вычисления or и xor двух числе
        /// </summary>
        /// <param name="par1"></param>
        /// <param name="par2"></param>
        /// <param name="operation">Операция xor или or</param>
        /// <returns>Значение выражения</returns>
        double Calc(double par1, double par2, string operation);
    }
}
