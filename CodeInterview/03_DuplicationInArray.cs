using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeInterview
{
    /// <summary>
    /// 题目：题目：在一个长度为n的数组里的所有数字都在0到n-1的范围内。数组中某些数字是重复的，但不知道有几个数字重复了，
    /// 也不知道每个数字重复了几次。请找出数组中任意一个重复的数字。例如，如果输入长度为7的数组{2, 3, 1, 0, 2, 5, 3}，
    /// 那么对应的输出是重复的数字2或者3。
    /// </summary>
    public class DuplicationInArray
    {
        /// <summary>
        /// 寻找重复的某个数
        /// </summary>
        /// <param name="numbers">输入数组</param>
        /// <param name="length">数组长度</param>
        /// <returns>
        /// -1：输入错误或没有重复数
        /// 自然数：重复的某个数
        /// </returns>
        public static int FindDuplication(int[] numbers, int length)
        {
            if (numbers == null || length <= 0)
            {
                return -1;
            }

            for (int i = 0; i < length; i++)
            {
                if (numbers[i] < 0 || numbers[i] > length - 1)
                    return -1;
            }

            for (int i = 0; i < length; i++)
            {
                while (numbers[i] != i)
                {
                    if (numbers[i] == numbers[numbers[i]])
                    {
                        return numbers[i];
                    }

                    int temp = numbers[i];
                    numbers[i] = numbers[temp];
                    numbers[temp] = temp;
                }
            }

            return -1;
        }
    }
}
