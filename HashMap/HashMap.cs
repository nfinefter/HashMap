using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HashMap
{
    public class HashMap<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public LinkedList<KeyValuePair<TKey, TValue>>[] items;

#nullable disable
        public TValue this[TKey key]
        {
            get
            {
                LinkedListNode<KeyValuePair<TKey, TValue>> node = GetNode(key);

                if (node == null) throw new KeyNotFoundException();

                return node.Value.Value;
            }
            set
            {

                LinkedListNode<KeyValuePair<TKey, TValue>> node = GetNode(key);

                if (node == null) throw new KeyNotFoundException();

                node.Value = KeyValuePair.Create(key, node.Value.Value);
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                ICollection<TKey> keys = null;
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] != null)
                    {
                        foreach (var item in items[i])
                        {
                            keys.Add(item.Key);
                        }
                    }
                }
                return keys;
            }
            
        }

        public ICollection<TValue> Values
        {
            get
            {
                ICollection<TValue> values = null;
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] != null)
                    {
                        foreach (var item in items[i])
                        {
                            values.Add(item.Value);
                        }
                    }
                }
                return values;
            }
        }
#nullable enable
        public int Count { get; private set; }

        public bool IsReadOnly => throw new NotImplementedException();

        private IEqualityComparer<TKey> equalityComparer;

        public HashMap(int count, IEqualityComparer<TKey> equalityComparer)
        {
            this.equalityComparer = equalityComparer;
            Count = count;
        }

        public LinkedList<KeyValuePair<TKey, TValue>>[] ReHash()
        {
            LinkedList<KeyValuePair<TKey, TValue>>[] newItems = new LinkedList<KeyValuePair<TKey, TValue>>[items.Length * 2];
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null)
                {
                    foreach (var item in items[i])
                    {
                        var index = (item.GetHashCode() % newItems.Length);

                        if (newItems[index] == null)
                        {
                            newItems[index] = new LinkedList<KeyValuePair<TKey, TValue>>();
                        }

                        newItems[index].AddLast(item);
                    }
                }
            }
            return newItems;
        }

        public void Add(TKey key, TValue value)
        {
            Add(KeyValuePair.Create(key, value));
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            var index = (item.Key.GetHashCode()) % items.Length;

            if (items[index] == null)
            {
                items[index] = new LinkedList<KeyValuePair<TKey, TValue>>();

                items[index].AddLast(item);
                Count++;
                if (items.Length >= Count)
                {
                    items = ReHash();
                }
            }
            else
            {
                LinkedListNode<KeyValuePair<TKey, TValue>> node = GetNode(item.Key);

                if (item.Key.Equals(node.Value.Key))
                {
                    throw new Exception("Duplicate key");
                }

                items[index].AddLast(item);
            }
        }

        public void Clear()
        {
            items = null;
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (GetNode(item) == null) return false;

            return true;
        }

        public bool ContainsKey(TKey key)
        {
            if (GetNode(key) == null) return false;

            return true;
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
            LinkedListNode<KeyValuePair<TKey, TValue>> node = GetNode(key);

            if (node == null) return false;

            node.List.Remove(node);
            return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {

            LinkedListNode<KeyValuePair<TKey, TValue>> node = GetNode(item.Key);

            if (node == null) return false;

            node.List.Remove(node);
            return true;
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public LinkedListNode<KeyValuePair<TKey, TValue>> GetNode(TKey key)
        {
            var index = (key.GetHashCode()) % items.Length;

            if (items[index] == null) return null;

            for (var temp = items[index].First; temp != null; temp = temp.Next)
            {
                if (key.Equals(temp.Value.Key)) return temp;
            }
            return null;
        }
        public LinkedListNode<KeyValuePair<TKey, TValue>> GetNode(KeyValuePair<TKey, TValue> item)
        {
            var index = (item.Key.GetHashCode()) % items.Length;

            if (items[index] == null) return null;

            for (var temp = items[index].First; temp != null; temp = temp.Next)
            {
                if (item.Key.Equals(temp.Value.Key)) return temp;
            }
            return null;
        }

     
    }
}
