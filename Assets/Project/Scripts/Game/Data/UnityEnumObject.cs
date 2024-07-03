using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data
{
    [Serializable]
    public sealed class UnityEnumObject<TKey, TValue> : IEquatable<TKey> where TKey : struct, Enum where TValue : class
    {
        [SerializeField]private TKey key;
        [SerializeField]private TValue value;

        public TKey Key => key;

        public TValue Value => value;


        public bool Equals(TKey other)
        {
            return EqualityComparer<TKey>.Default.Equals(key, other);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is UnityEnumObject<TKey, TValue> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(value, key);
        }
    }
}