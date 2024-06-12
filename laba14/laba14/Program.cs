using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using ClassLibrary1;

namespace laba14
{
    public static class StringExtension
    {
        public static int WordCount(this string s)
        {
            return s.Split(new char[] { ' ', ',', '.', '!' }, StringSplitOptions.RemoveEmptyEntries).Count();
        }
    }
    public static class ObjExtension
    {
        public static void DisplayType(this )
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string str = "D nhfdt vclbkt rxpytxbr!";
            Console.WriteLine(str.WordCount());
        }
    }
}