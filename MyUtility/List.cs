using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUtility
{
    public static class List
    {
        /// <summary>
        /// 同参照なら true, 同要素数＆同要素順＆全要素 Equals が true なら true / この 2 条件以外は全て false
        /// </summary>
        /// <typeparam name="T">Equals() メソッドを実装していること</typeparam>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        /// <returns></returns>
        public static bool AreEqual<T>(List<T> list1, List<T> list2)
        {
            if (ReferenceEquals(list1, list2))
            {
                return true;
            }

            if (list1.Count != list2.Count)
            {
                return false;
            }

            for(int i = 0; i < list1.Count; i++)
            {
                if (!list1[i].Equals(list2[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// シャッフルした List (新規インスタンス)を返す
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> Shuffle<T>(List<T> list)
        {
            // シャッフル参考: https://dobon.net/vb/dotnet/programing/arrayshuffle.html
            return list.OrderBy(i => MyUtility.Guid.Get()).ToList();
        }
    }
}
