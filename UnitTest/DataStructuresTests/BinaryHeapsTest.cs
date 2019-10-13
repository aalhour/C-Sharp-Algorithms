using DataStructures.Heaps;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTest.DataStructuresTests
{
    public static class MinHeapTest
    {
        [Fact]
        public static void CheckOrderInHeap_RandomOrder_ReturnsTrue()
        {
            BinaryMinHeap<long> minHeap = new BinaryMinHeap<long>(Comparer<long>.Default);

            minHeap.Add(23);
            minHeap.Add(42);
            minHeap.Add(4);
            minHeap.Add(16);
            minHeap.Add(8);
            minHeap.Add(1);
            minHeap.Add(3);
            minHeap.Add(100);
            minHeap.Add(5);
            minHeap.Add(7);

            var isRightOrder = IsRightOrderInHeap<long>(minHeap);
            Assert.True(isRightOrder);    
        }

        [Fact]
        public static void CheckOrderInHeap_AscendingOrder_ReturnsTrue()
        {
            BinaryMinHeap<long> minHeap = new BinaryMinHeap<long>(Comparer<long>.Default);

            minHeap.Add(1);
            minHeap.Add(2);
            minHeap.Add(3);
            minHeap.Add(4);
            minHeap.Add(5);
            minHeap.Add(6);
            minHeap.Add(7);
            minHeap.Add(8);
            minHeap.Add(9);
            minHeap.Add(10);

            var isRightOrder = IsRightOrderInHeap<long>(minHeap);
            Assert.True(isRightOrder);
        }

        [Fact]
        public static void CheckOrderInHeap_DecreasingOrder_ReturnsTrue()
        {
            BinaryMinHeap<long> minHeap = new BinaryMinHeap<long>(Comparer<long>.Default);

            minHeap.Add(10);
            minHeap.Add(9);
            minHeap.Add(8);
            minHeap.Add(7);
            minHeap.Add(6);
            minHeap.Add(5);
            minHeap.Add(4);
            minHeap.Add(3);
            minHeap.Add(2);
            minHeap.Add(1);

            var isRightOrder = IsRightOrderInHeap<long>(minHeap);
            Assert.True(isRightOrder);
        }

        public static bool IsRightOrderInHeap<T>(BinaryMinHeap<T> binaryMinHeap) where T : IComparable<T>
        {
            var array = binaryMinHeap.ToArray();
            
            for(int i=0; i * 2 + 1 < array.Length; ++i)
            {
                int leftChildIndex = i * 2 + 1;
                int rightChildIndex = leftChildIndex + 1;

                if (array[i].CompareTo(array[leftChildIndex]) > 0)
                {
                    return false;
                }

                if (rightChildIndex < array.Length && array[i].CompareTo(array[rightChildIndex]) > 0)
                {
                    return true;
                }
            }

            return true;
        }
    }

    public static class MaxHeapTest
    {
        [Fact]
        public static void CheckOrderInHeap_RandomOrder_ReturnsTrue()
        {
            BinaryMaxHeap<long> maxHeap = new BinaryMaxHeap<long>(Comparer<long>.Default);

            maxHeap.Add(23);
            maxHeap.Add(42);
            maxHeap.Add(4);
            maxHeap.Add(16);
            maxHeap.Add(8);
            maxHeap.Add(1);
            maxHeap.Add(3);
            maxHeap.Add(100);
            maxHeap.Add(5);
            maxHeap.Add(7);

            var isRightOrder = IsRightOrderInHeap<long>(maxHeap);
            Assert.True(isRightOrder);
        }

        [Fact]
        public static void CheckOrderInHeap_AscendingOrder_ReturnsTrue()
        {
            BinaryMaxHeap<long> maxHeap = new BinaryMaxHeap<long>(Comparer<long>.Default);

            maxHeap.Add(1);
            maxHeap.Add(2);
            maxHeap.Add(3);
            maxHeap.Add(4);
            maxHeap.Add(5);
            maxHeap.Add(6);
            maxHeap.Add(7);
            maxHeap.Add(8);
            maxHeap.Add(9);
            maxHeap.Add(10);

            var isRightOrder = IsRightOrderInHeap<long>(maxHeap);
            Assert.True(isRightOrder);
        }

        [Fact]
        public static void CheckOrderInHeap_DecreasingOrder_ReturnsTrue()
        {
            BinaryMaxHeap<long> maxHeap = new BinaryMaxHeap<long>(Comparer<long>.Default);

            maxHeap.Add(10);
            maxHeap.Add(9);
            maxHeap.Add(8);
            maxHeap.Add(7);
            maxHeap.Add(6);
            maxHeap.Add(5);
            maxHeap.Add(4);
            maxHeap.Add(3);
            maxHeap.Add(2);
            maxHeap.Add(1);

            var isRightOrder = IsRightOrderInHeap<long>(maxHeap);
            Assert.True(isRightOrder);
        }

        public static bool IsRightOrderInHeap<T>(BinaryMaxHeap<T> binaryMaxHeap) where T : IComparable<T>
        {
            var array = binaryMaxHeap.ToArray();

            for (int i = 0; i * 2 + 1 < array.Length; ++i)
            {
                int leftChildIndex = i * 2 + 1;
                int rightChildIndex = leftChildIndex + 1;

                if (array[i].CompareTo(array[leftChildIndex]) < 0)
                {
                    return false;
                }

                if (rightChildIndex < array.Length && array[i].CompareTo(array[rightChildIndex]) > 0)
                {
                    return true;
                }
            }

            return true;
        }
    }
}

