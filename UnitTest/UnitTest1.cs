using HashMap;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Security;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;

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
            int count = hashMap.Count;
            hashMap.Add(key, val);
            Assert.True(count != hashMap.Count);
        }

        [Theory]
        [InlineData("a", 47)]
        public void Remove(string key, int value)
        {
            hashMap.Add(key, value);

            int count = hashMap.Count;

            KeyValuePair<string, int> item = new KeyValuePair<string, int>();

            item = KeyValuePair.Create(key, value);

            hashMap.Remove(item);
            Assert.True(count != hashMap.Count);
        }

        [Theory]
        [InlineData("a", 47)]
        public void RemoveKey(string key, int value)
        {
            hashMap.Add(key, value);

            int count = hashMap.Count;

            hashMap.Remove(key);
            Assert.True(count != hashMap.Count);
        }

        [Theory]
        [InlineData("a", 47)]
        public void Contains(string key, int value)
        {
            hashMap.Add(key, value);
            KeyValuePair<string, int> keyValuePair = new KeyValuePair<string, int>(key, value);

            Assert.True(hashMap.Contains(keyValuePair));
        }

        [Theory]
        [InlineData(0)]
        public void CopyTo(int arrayIndex)
        {
            hashMap.Add("z", 523);

            //make array
            KeyValuePair<string, int>[] array = new KeyValuePair<string, int>[5];

            KeyValuePair<string, int>[] temp = new KeyValuePair<string, int>[5];
            array.CopyTo(temp, 0);
            hashMap.CopyTo(array, arrayIndex);

            Assert.False(Enumerable.SequenceEqual(array, temp));
        }

        [Theory]
        [InlineData("a", 47)]
        public void ContainsKey(string key, int value)
        {
            hashMap.Add(key, value);
            Assert.True(hashMap.ContainsKey(key));
        }

        [Theory]
        [InlineData("a")]
        public void TryGetValue(string key)
        {
            hashMap.Add("a", 53);
            hashMap.TryGetValue(key, out int value);
            Assert.True(value != default);
        }

        [Theory]
        [InlineData(new string[] {"a", "c"}, new int[] { 5, 1 }, "b", 7)]
        public void ReadOnlyCheck(string[] keys, int[] values, string key, int value)
        {
            List<KeyValuePair<string, int>> collection = new List<KeyValuePair<string, int>>();

            for (int i = 0; i < keys.Length; i++)
            {
                collection.Add(KeyValuePair.Create(keys[i], values[i]));
            }
            hashMap = new HashMap<string, int>(collection, true, 5, new StringEqualityComparer());

            int count = hashMap.Count;

            hashMap.Add(key, value);
            Assert.True(hashMap.Count == count);
        }
    }
}