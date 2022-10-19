using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HashMap
{
    public class HashMap<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public LinkedList<KeyValuePair<TKey, TValue>>[] items;

        public TValue this[TKey key]
        {
            get
            {

            }
            set
            {

            }
        }
        public ICollection<TKey> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        

        public void Add(TKey key, TValue value)
        {
            var index = (key.GetHashCode()) % items.Length;

            if (items[index] == null)
            {
                LinkedList<KeyValuePair<TKey, TValue>> keyValuePairs = new LinkedList<KeyValuePair<TKey, TValue>>();
                keyValuePairs.AddLast();
                //Add the KeyValuePair
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {

        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }
}
