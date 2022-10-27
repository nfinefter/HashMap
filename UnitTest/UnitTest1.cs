using HashMap;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Security;
using Microsoft.VisualBasic;

namespace UnitTest
{
    public class UnitTest1
    {
        HashMap<string, int> hashMap = new HashMap<string, int>(5, new StringEqualityComparer());

        [Theory]
        [InlineData("a", 47)]
        [InlineData("b", 89)]
        [InlineData("c", 61)]
        [InlineData("d", 34)]
        [InlineData("e", 23)]
        public void Add(string key, int val)
        {
            hashMap.Add(key, val);
        }

        [Theory]
        [InlineData("a", 47)]
        public void Remove(string key, int value)
        {
            KeyValuePair<string, int> keyValuePair = new KeyValuePair<string, int>(key, value);

            hashMap.Remove(keyValuePair);
        }

        [Theory]
        [InlineData("a", 47)]
        public void Contains(string key, int value)
        {
            KeyValuePair<string, int> keyValuePair = new KeyValuePair<string, int>(key, value);

            hashMap.Contains(keyValuePair);
        }
        /*(string, int)[] temp = new (string, int)[]
        {
            ("a", 27)
        };*/
        [Theory]
        [InlineData(new[] { "a" }, new[] { 5 }, 0)]
        public void CopyTo(string[] strings, int[] ints, int arrayIndex)
        {
            //make array
            hashMap.CopyTo(KeyValuePair.Create(strings[0], ints[0]), arrayIndex);
        }

    }
}