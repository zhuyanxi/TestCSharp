using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeInterview
{
    //[TestFixture]
    public class FindInPartiallySortedMatrix
    {
        //[SetUp]
        //public void Setup()
        //{
        //}

        public static bool Find(int[,] matrix, int number)
        {
            var rows = matrix.GetLength(0);
            var columns = matrix.GetLength(1);

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