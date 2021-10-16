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

        public static double ToRadian(double degree)
        {
            return degree * System.Math.PI / 180.0;
        }
    }
}
