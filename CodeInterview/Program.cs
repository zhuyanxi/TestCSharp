using System;

namespace CodeInterview
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TestDuplicationArray();
        }

        static void TestDuplicationArray()
        {
            int[] numbers = { 2, 3, 3, 4, 5, 1 };
            int dup = DuplicationInArray.FindDuplication(numbers, numbers.Length);
            Console.WriteLine($"One of the duplicate numbers is : {dup}.");
        }
    }
}
