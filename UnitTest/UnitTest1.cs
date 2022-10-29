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
        [InlineData("a")]
        public void RemoveKey(string key)
        {
            hashMap.Remove(key);
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
        [InlineData(0)]
        public void CopyTo(int arrayIndex)
        {
            //make array
            KeyValuePair<string, int>[] array = new KeyValuePair<string, int>[5];
            hashMap.CopyTo(array, arrayIndex);
        }

        [Theory]
        [InlineData("a")]
        public void ContainsKey(string key)
        {
            hashMap.ContainsKey(key);
        }

    }
}