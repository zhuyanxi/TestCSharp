﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace ConsistentHash
{
    /// <summary>
    /// https://code.google.com/archive/p/consistent-hash/
    /// </summary>
    public class MurmurHash2
    {
        private const uint m = 0x5bd1e995;
        private const int r = 24;

        [StructLayout(LayoutKind.Explicit)]
        private struct ByteToUInt32Converter
        {
            [FieldOffset(0)]
            public byte[] Bytes;

            [FieldOffset(0)]
            public uint[] UInts;
        }

        public static uint Hash(byte[] data)
        {
            return Hash(data, 0xc58f1a7b);
        }

        public static uint Hash(byte[] data, uint seed)
        {
            int length = data.Length;
            if (length == 0)
            {
                return 0;
            }
            uint h = seed ^ (uint)length;
            int currentIndex = 0;

            // array will be length of Bytes but contains Uints
            // therefore the currentIndex will jump with +1 while length will jump with +4
            uint[] hackArray = new ByteToUInt32Converter { Bytes = data }.UInts;
            while (length >= 4)
            {
                uint k = hackArray[currentIndex++];
                k *= m;
                k ^= k >> r;
                k *= m;

                h *= m;
                h ^= k;
                length -= 4;
            }
            currentIndex *= 4;
            switch (length)
            {
                case 3:
                    h ^= (ushort)(data[currentIndex++] | data[currentIndex++] << 8);
                    h ^= (uint)data[currentIndex] << 16;
                    h *= m;
                    break;

                case 2:
                    h ^= (ushort)(data[currentIndex++] | data[currentIndex] << 8);
                    h *= m;
                    break;

                case 1:
                    h ^= data[currentIndex];
                    h *= m;
                    break;

                default:
                    break;
            }

            // Do a few final mixes of the hash to ensure the last few
            // bytes are well-incorporated.
            h ^= h >> 13;
            h *= m;
            h ^= h >> 15;

            return h;
        }
    }

    public class ConsistentHash<T>
    {
        private SortedDictionary<int, T> circle = new SortedDictionary<int, T>();
        private int _replicate = 100; //default _replicate count
        private int[] ayKeys = null;  //cache the ordered keys for better performance

        //it's better you override the GetHashCode() of T.
        //we will use GetHashCode() to identify different node.
        public void Init(IEnumerable<T> nodes)
        {
            Init(nodes, _replicate);
        }

        public void Init(IEnumerable<T> nodes, int replicate)
        {
            _replicate = replicate;
            foreach (var node in nodes)
            {
                Add(node, false);
            }
            ayKeys = circle.Keys.ToArray();
        }

        public void Add(T node)
        {
            Add(node, true);
        }

        public void Add(T node, bool updateKeyArray)
        {
            for (int i = 0; i < _replicate; i++)
            {
                int hash = BetterHash(node.GetHashCode().ToString() + i);
                circle[hash] = node;
            }
            if (updateKeyArray)
            {
                ayKeys = circle.Keys.ToArray();
            }
        }

        public void Remove(T node)
        {
            for (int i = 0; i < _replicate; i++)
            {
                int hash = BetterHash(node.GetHashCode().ToString() + i);
                if (!circle.Remove(hash))
                {
                    throw new Exception("can not remove a node that not added");
                }
            }
            ayKeys = circle.Keys.ToArray();
        }

        //we keep this function just for performance compare
        private T GetNode_slow(string key)
        {
            int hash = BetterHash(key);
            if (circle.ContainsKey(hash))
            {
                return circle[hash];
            }

            int first = circle.Keys.FirstOrDefault(h => h >= hash);
            if (first == new int())
            {
                first = ayKeys[0];
            }
            T node = circle[first];
            return node;
        }

        //return the index of first item that >= val.
        //if not exist, return 0;
        //ay should be ordered array.
        private int First_ge(int[] ay, int val)
        {
            int begin = 0;
            int end = ay.Length - 1;
            if (ay[end] < val || ay[0] > val)
            {
                return 0;
            }

            int mid = begin;
            while (end - begin > 1)
            {
                mid = (end + begin) / 2;
                if (ay[mid] >= val)
                {
                    end = mid;
                }
                else
                {
                    begin = mid;
                }
            }
            if (ay[begin] > val || ay[end] < val)
            {
                throw new Exception("should not happen");
            }

            return end;
        }

        public T GetNode(string key)
        {
            //return GetNode_slow(key);

            int hash = BetterHash(key);
            int first = First_ge(ayKeys, hash);

            //int diff = circle.Keys[first] - hash;
            return circle[ayKeys[first]];
        }

        //default String.GetHashCode() can't well spread strings like "1", "2", "3"
        public static int BetterHash(string key)
        {
            uint hash = MurmurHash2.Hash(Encoding.ASCII.GetBytes(key));
            return (int)hash;
        }
    }
}