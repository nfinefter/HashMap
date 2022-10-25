using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

namespace HashMap
{
    class StringEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string? x, string? y)
        {
            return x == y;
        }

        public int GetHashCode([DisallowNull] string obj)
        {
            return obj.GetHashCode();
        }
    }
    

    internal class Program
    {
        static void Main(string[] args)
        {
            HashMap<string, int> items = new HashMap<string, int>(5, new StringEqualityComparer());

            items.Add("a", 47);
            items.Add("b", 89);
            items.Add("c", 61);
            items.Add("d", 34);
            items.Add("e", 23);

            Dictionary<int, int> ints = new Dictionary<int, int>();

        }
    }
}