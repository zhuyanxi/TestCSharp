﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeInterview
{
    // 题目：在一个二维数组中，每一行都按照从左到右递增的顺序排序，每一列都按
    // 照从上到下递增的顺序排序。请完成一个函数，输入这样的一个二维数组和一个
    // 整数，判断数组中是否含有该整数。
    //[TestFixture]
    public class FindInPartiallySortedMatrix
    {
        //[SetUp]
        //public void Setup()
        //{
        //}

        public static bool Find(int[,] matrix, int number)
        {
            var mRow = matrix.GetLength(0);
            var mColumn = matrix.GetLength(1);

            int currentRow = 0;
            int currentColumn = mColumn - 1;

            //while(currentRow)

            return false;
        }

        [Test]
        public void Test1()
        {
            int[,] matrix = new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };
            var rows = matrix.GetLength(0);
            var columns = matrix.GetLength(1);

            int a = 1;
            int b = 2;
            int sum = a + b;
            Assert.AreEqual(sum, 3);
        }
    }
}