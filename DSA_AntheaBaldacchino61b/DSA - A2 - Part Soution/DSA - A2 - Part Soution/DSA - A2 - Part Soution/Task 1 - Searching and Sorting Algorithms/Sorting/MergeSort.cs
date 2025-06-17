using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1___Searching_and_Sorting_Algorithms
{
    /// <summary>
    /// Class allowing the sorting of Order objects based on Date Placed (most recent first) using Merge Sort
    /// 
    /// TODO: You are to implement the Sort() method for this class; additional methods may be added but:
    /// 1. The final result i.e. the sorted array of orders must be returned by the Sort() method provided
    /// 2. Any methods added to this class must be declared as private and called within the Sort() method
    /// </summary>
    internal class MergeSort : Sorter
    {
        public override List<Order> Sort(List<Order> unsortedOrderList)
        {
            if (unsortedOrderList == null || unsortedOrderList.Count <= 1)
                return new List<Order>(unsortedOrderList);

            List<Order> sortedList = new List<Order>(unsortedOrderList);
            MergeSortRecursive(sortedList, 0, sortedList.Count - 1);
            sortedList.Reverse(); 
            return sortedList;
        }

        private void MergeSortRecursive(List<Order> list, int left, int right)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;
                MergeSortRecursive(list, left, mid);
                MergeSortRecursive(list, mid + 1, right);
                Merge(list, left, mid, right);
            }
        }

        private void Merge(List<Order> list, int left, int mid, int right)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;

            List<Order> leftList = new List<Order>();
            List<Order> rightList = new List<Order>();

            for (int i = 0; i < n1; i++)
                leftList.Add(list[left + i]);
            for (int j = 0; j < n2; j++)
                rightList.Add(list[mid + 1 + j]);

            int x = 0, y = 0;
            int k = left;

            while (x < n1 && y < n2)
            {
                if (leftList[x].placedOn.CompareTo(rightList[y].placedOn) <= 0)
                {
                    list[k] = leftList[x];
                    x++;
                }
                else
                {
                    list[k] = rightList[y];
                    y++;
                }
                k++;
            }

            while (x < n1)
            {
                list[k] = leftList[x];
                x++;
                k++;
            }

            while (y < n2)
            {
                list[k] = rightList[y];
                y++;
                k++;
            }
        }
    }
}
