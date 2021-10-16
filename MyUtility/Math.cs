using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUtility
{
    public static class Math
    {
        public static double CalcDistance((double X, double Y) p1, (double X, double Y) p2)
        {
            double xDiff = p1.X - p2.X;
            double yDiff = p1.Y - p2.Y;
            return System.Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
        }

        /// <summary>
        /// 高速版: Min
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int Min(int a, int b)
        {
            return a < b ? a : b;
        }

        /// <summary>
        /// 高速版: 正数の小数点以下四捨五入
        /// </summary>
        /// <param name="overZero"></param>
        /// <returns></returns>
        public static int Round(double overZero)
        {
            double tmp = overZero + 0.5;
            return (int)tmp;
        }

        public static double ToRadian(double degree)
        {
            return degree * System.Math.PI / 180.0;
        }
    }
}
