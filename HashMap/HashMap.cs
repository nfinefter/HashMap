using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

        private bool isReadOnly = false;

        public bool IsReadOnly => isReadOnly;

        private IEqualityComparer<TKey> equalityComparer;

        public HashMap(List<KeyValuePair<TKey, TValue>> collection, bool isReadOnly, int size, IEqualityComparer<TKey> equalityComparer)
        {
            if (size == 0) size = 10;

            items = new LinkedList<KeyValuePair<TKey, TValue>>[size];
            Count = 0;
            this.equalityComparer = equalityComparer;
            for (int i = 0; i < collection.Count; i++)
            {
                Add(collection[i].Key, collection[i].Value);
            }
            this.isReadOnly = isReadOnly;
            
        }

        public HashMap(int size, IEqualityComparer<TKey> equalityComparer)
        {
            if (size == 0) size = 10;

            items = new LinkedList<KeyValuePair<TKey, TValue>>[size];
            this.equalityComparer = equalityComparer;
            Count = 0;
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
                        var index = (Math.Abs(item.GetHashCode()) % newItems.Length);

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
            if (IsReadOnly == true) return;

            var index = (Math.Abs(item.Key.GetHashCode())) % items.Length;

            if (items[index] == null)
            {
                items[index] = new LinkedList<KeyValuePair<TKey, TValue>>();

                items[index].AddLast(item);
                Count++;
                if (Count >= items.Length)
                {
                    items = ReHash();
                }
            }
            else
            {
                LinkedListNode<KeyValuePair<TKey, TValue>> node = GetNode(item.Key);

                if (node != null && item.Key.Equals(node.Value.Key))
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

        //looping through the pairs in this, copy at array[arrayIndex] each pair and increment index
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            int count = arrayIndex;

            if (count + Count > items.Length)
            {
                throw new ArgumentOutOfRangeException();
            }

            foreach (var item in this)
            {
               
                array[count] = item;
                count++;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null) continue;

                foreach (var item in items[i])
                {
                    yield return item;
                }
            }

        }

        public bool Remove(TKey key)
        {
            if (IsReadOnly == true) return false;
            LinkedListNode<KeyValuePair<TKey, TValue>> node = GetNode(key);

            if (node == null) return false;

            node.List.Remove(node);
            Count--;
            return true;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (IsReadOnly == true) return false;
            LinkedListNode<KeyValuePair<TKey, TValue>> node = GetNode(item.Key);

            if (node == null) return false;

            node.List.Remove(node);
            Count--;
            return true;
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            //given the key, if the key exists with a value set value to the value and return true other return false

            value = default;

            var item = GetNode(key);

            if (item == null) return false;

            value = item.Value.Value;
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public LinkedListNode<KeyValuePair<TKey, TValue>> GetNode(TKey key)
        {
            var index = (Math.Abs(key.GetHashCode()) % items.Length);

            if (items[index] == null) return null;

            for (var temp = items[index].First; temp != null; temp = temp.Next)
            {
                if (key.Equals(temp.Value.Key)) return temp;
            }
            return null;
        }
        public LinkedListNode<KeyValuePair<TKey, TValue>> GetNode(KeyValuePair<TKey, TValue> item)
        {
            var index = (Math.Abs(item.Key.GetHashCode())) % items.Length;

            if (items[index] == null) return null;

            for (var temp = items[index].First; temp != null; temp = temp.Next)
            {
                if (item.Key.Equals(temp.Value.Key)) return temp;
            }
            return null;
        }


    }
}
