using System;
using System.Collections.Generic;

namespace Game.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="index"></param>
        /// <param name="swappedValue"></param>
        /// <typeparam name="T"></typeparam>
        public static void RemoveAtSwapBack<T>(this IList<T> list, int index, out T swappedValue)
        {
            int lastIndex = list.Count - 1;
            if (lastIndex > 0 && lastIndex != index)
            {
                swappedValue = list[index] = list[lastIndex];
            }
            else
            {
                swappedValue = default;
            }

            list.RemoveAt(lastIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="sourceIndex"></param>
        /// <param name="destinationIndex"></param>
        /// <param name="newDestinationValue"></param>
        /// <typeparam name="T"></typeparam>
        public static void Swap<T>(this IList<T> list, int sourceIndex, int destinationIndex, out T newDestinationValue)
        {
            newDestinationValue = list[sourceIndex];
            list[sourceIndex] = list[destinationIndex];
            list[destinationIndex] = newDestinationValue;
        }


        public static T Find<T>(this T[] array, Predicate<T> predicate)
        {
            if (array == null) return default;
            for (int i = 0; i < array.Length; i++)
            {
                if (predicate.Invoke(array[i]))
                {
                    return array[i];
                }
            }

            return default;
        }

        public static bool Exists<T>(this T[] array, Predicate<T> predicate)
        {
            if (array == null) return false;
            foreach (var item in array)
            {
                if (predicate(item))
                {
                    return true;
                }
            }

            return false;
        }

        public static Queue<T> ToQueue<T>(this List<T> list, bool forwardOrder = true)
        {
            var queue = new Queue<T>();

            if (forwardOrder)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    queue.Enqueue(list[i]);
                }
            }
            else
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    queue.Enqueue(list[i]);
                }
            }

            foreach (var item in list)
            {
                queue.Enqueue(item);
            }

            return queue;
        }

        /// <summary>
        /// Use Unity Random.Range
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static T Random<T>(this IList<T> list)
        {
            if (list.Count == 0)
            {
                return default(T);
            }

            var index = UnityEngine.Random.Range(0, list.Count);
            return list[index];
        }

        /// <summary>
        /// Stable Sort
        /// http://www.csharp411.com/c-stable-sort/
        /// </summary>
        /// <param name="list"></param>
        /// <param name="comparison"></param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        public static void InsertionSort<T>(this IList<T> list, Comparison<T> comparison)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (comparison == null)
                throw new ArgumentNullException(nameof(comparison));

            int count = list.Count;
            for (int j = 1; j < count; j++)
            {
                T key = list[j];

                int i = j - 1;
                for (; i >= 0 && comparison(list[i], key) > 0; i--)
                {
                    list[i + 1] = list[i];
                }

                list[i + 1] = key;
            }
        }
    }
}