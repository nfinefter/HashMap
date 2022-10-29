using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

namespace HashMap
{
    public class StringEqualityComparer : IEqualityComparer<string>
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
            HashMap<string, int> items = new HashMap<string, int>(0, new StringEqualityComparer());

            KeyValuePair<string, int> object1 = new KeyValuePair<string, int>("a", 47);
            KeyValuePair<string, int> object2 = new KeyValuePair<string, int>("fasdf", 47);

            items.Add(object1);
            items.Add("b", 89);
            items.Add("c", 61);
            items.Add("d", 34);
            items.Remove(object1);
            items.Add(object1);
            items.Add("e", 23);
            bool contain = items.Contains(object1);
            var temp = items["a"];

            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();

            foreach (var item in items)
            {
                list.Add(item);
            }

            KeyValuePair<string, int>[] array = new KeyValuePair<string, int>[10];

            items.CopyTo(array, 1);

        }
    }
}