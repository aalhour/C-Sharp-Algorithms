using System;
using DataStructures.Trees;

namespace DataStructures.Common
{
    public static class Comparers
    {
        /// <summary>
        /// Determines if a specific value is a number.
        /// </summary>
        /// <returns><c>true</c> if the value is a number; otherwise, <c>false</c>.</returns>
        /// <param name="value">Value.</param>
        /// <typeparam name="T">The Type of value.</typeparam>
        public static bool IsNumber<T>(this T value)
        {
            if (value is sbyte) return true;
            if (value is byte) return true;
            if (value is short) return true;
            if (value is ushort) return true;
            if (value is int) return true;
            if (value is uint) return true;
            if (value is long) return true;
            if (value is ulong) return true;
            if (value is float) return true;
            if (value is double) return true;
            if (value is decimal) return true;
            return false;
        }

        public static bool IsEqualTo<T>(this T firstValue, T secondValue) where T : IComparable<T>
        {
            return firstValue.Equals(secondValue);
        }

        public static bool IsGreaterThan<T>(this T firstValue, T secondValue) where T : IComparable<T>
        {
            return firstValue.CompareTo(secondValue) > 0;
        }

        public static bool IsLessThan<T>(this T firstValue, T secondValue) where T : IComparable<T>
        {
            return firstValue.CompareTo(secondValue) < 0;
        }

        public static bool IsGreaterThanOrEqualTo<T>(this T firstValue, T secondValue) where T : IComparable<T>
        {
            return (firstValue.IsEqualTo(secondValue) || firstValue.IsGreaterThan(secondValue));
        }

        public static bool IsLessThanOrEqualTo<T>(this T firstValue, T secondValue) where T : IComparable<T>
        {
            return (firstValue.IsEqualTo(secondValue) || firstValue.IsLessThan(secondValue));
        }


        //
        // METHODS FOR BINARY SEARCH TREE
        // COMAPRES THE VALUE OF TWO NODES TOGETHER
        private static bool HandleNullCases<T>(BSTNode<T> first, BSTNode<T> second) where T : IComparable<T>
        {
            if (first == null || second == null)
                return false;
            return true;
        }

        public static bool IsEqualTo<T>(this BSTNode<T> first, BSTNode<T> second) where T : IComparable<T>
        {
            return (HandleNullCases(first, second) && first.Value.CompareTo(second.Value) == 0);
        }

        public static bool IsGreaterThan<T>(this BSTNode<T> first, BSTNode<T> second) where T : IComparable<T>
        {
            return (HandleNullCases(first, second) && first.Value.CompareTo(second.Value) > 0);
        }

        public static bool IsLessThan<T>(this BSTNode<T> first, BSTNode<T> second) where T : IComparable<T>
        {
            return (HandleNullCases(first, second) && first.Value.CompareTo(second.Value) < 0);
        }

        public static bool IsLessThanOrEqualTo<T>(this BSTNode<T> first, BSTNode<T> second) where T : IComparable<T>
        {
            return (first.IsEqualTo(second) || first.IsLessThan(second));
        }

        public static bool IsGreaterThanOrEqualTo<T>(this BSTNode<T> first, BSTNode<T> second) where T : IComparable<T>
        {
            return (first.IsEqualTo(second) || first.IsGreaterThan(second));
        }
    }

}
