using ClassLibrary1;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class SortByCost : IComparer
    {
        public int Compare(object x, object y)
        {
            Auto auto1 = x as Auto;
            Auto auto2 = y as Auto;
            if (auto1.Cost < auto2.Cost) return -1;
            else if (auto1.Cost == auto2.Cost) return 0;
            else return 1;
        }
    }
}
