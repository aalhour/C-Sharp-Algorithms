namespace Algorithms.Sorting
{
    public static class QuickSortDemo
    {
        public static void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }

        private static void QuickSort(int[] array, int left, int right)
        {
            // stop it if left >= right
            if (left>= right)
            {
                return;
            }

            var index = Partition(array, left, right);
            
            //recursive method left & right 
            QuickSort(array, left, index -1);
            QuickSort(array, index+ 1, right);
        }

        private static int Partition(int[] array, int left, int right)
        {
            var result = left;
            var pivotValue = array[right];

            for (var i = left; i <= right - 1; i++)
            {
                if (array[i] > pivotValue) continue;
                Swap(array, i, result);
                result++;
            }
            
            Swap(array, result, right);

            return result;
        }

        private static void Swap(int[] array, int left, int right)
        {
            if (array.Length < 2 || left == right)
            {
                return;
            }

            (array[left], array[right]) = (array[right], array[left]);
        }
    }
}