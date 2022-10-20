using System;

namespace HashMap
{ 
    internal class Program
    {
        static void Main(string[] args)
        {
            HashMap<string, int> items = new HashMap<string, int>();

            items.Add("a", 47);
            items.Add("b", 89);
            items.Add("c", 61);
            items.Add("d", 34);
            items.Add("e", 23);

        }
    }
}