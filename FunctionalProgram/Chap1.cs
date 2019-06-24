using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgram
{
    public static partial class MyExtentsion
    {
        public static void OutputList<T>(this IEnumerable<T> ll)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            foreach (var item in ll)
            {
                sb.Append($"{item},");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("}");
            Console.WriteLine(sb.ToString());
        }
    }

    public class Chap1
    {
        public Chap1() { }

        /// <summary>
        /// 函数作为第一类值
        /// </summary>
        public void Test1()
        {
            Func<int, int> tripple = x => x * 3;
            var range = Enumerable.Range(1, 3);
            var tripples = range.Select(tripple);
            tripples.OutputList();
        }

        /// <summary>
        /// 避免状态冲突：来自并发进程的状态突变会产生不可预知的结果
        /// </summary>
        public void Test2()
        {
            var nums = Enumerable.Range(-50000, 100001).Reverse().ToList();
            //Console.WriteLine(nums.Sum());

            Action task1 = () => { Console.WriteLine(nums.Sum()); };
            Action task2 = () => { nums.Sort(); Console.WriteLine(nums.Sum()); };
            Action task3 = () => Console.WriteLine(nums.OrderBy(x => x).Sum());

            Parallel.Invoke(task1, task2);
        }
        /*
         * LINQ常见操作；
         * 映射:给定一个序列和一个函数，映射生成一个新序列。
         *     在LINQ中是用Select实现的（eg：Enumerable.Range(1, 3).Select(x => x * 3) => [3,6,9]）
         * 过滤:给定一个序列和一个谓词，过滤生成一个新序列。
         *     在LINQ中是用Where实现的（eg：Enumerable.Range(1, 10).Where(x => x % 3 == 0) => [3,6,9]）
         * 排序:给定一个序列和一个键选择器函数，排序生成一个排序后的新序列。
         *     在LINQ中是用OrderBy和OrderByDescending实现的（eg：Enumerable.Range(1, 5).OrderBy(x => -x) => [5，4，3，2，1]）
         */


        #region 1.2.2 C#6和C#7中的函数式特性
        public Chap1(double radius) => Radius = radius;//一个只读的自动属性只能在构造函数中设置
        public double Radius { get; }
        public double Circumference => Math.PI * 2 * Radius;//一个具有表达式体式的属性
        public double Area
        {
            get
            {
                double Square(double d) => Math.Pow(d, 2);//局部函数Square：另一个方法中所声明的方法
                return Math.PI * Square(Radius);
            }
        }
        public (double Circumference, double Area) Stats => (Circumference, Area);//具有命名元素的元组语法（C#7），返回一个类型为(double，double)的元组

        /*
         * 在Funcional Programming中，倾向于编写大量简单的函数，其中许多是单行的。
         */
        #endregion


        #region 1.3 函数思维
        /*
         * C#中有几种可用于表示函数的语言结构：方法、委托、lambda、字典
         */

        ///委托是类型安全的函数指针，这意味着委托是强类型：函数的输入和输出值的类型在编译时是已知的，统一由编译器强制执行。
        ///C#语言内置有Func、Action、Predicate等委托
        ///public delegate int Comparison<in T>(T x, T y);//该委托可被赋予两个T类型的值，返回一个指示哪一个更大的int
        public void TestDelegate1()
        {
            var list = Enumerable.Range(1, 10).Select(i => i * 3).ToList();
            list.OutputList();
            Comparison<int> alphabetically = (l, r) => l.ToString().CompareTo(r.ToString());//定义Comparison的实现
            list.Sort(alphabetically);
            list.OutputList();
        }

        ///字典Dictionary
        ///字典也被称为映射Map或哈希表HashTable
        #endregion


        #region 1.4 高阶函数
        /*
         * 函数作为第一类值的最重要有点：可以定义高阶函数（HOF-High Order Function）
         */

        ///接收其它函数作为参数的函数
        public IEnumerable<T> MYWhere<T>(this IEnumerable<T> ts, Func<T, bool> predicate)
        {
            foreach (T t in ts)
            {
                if (predicate(t))
                {
                    yield return t;
                }
            }
        }
        #endregion
    }
}
