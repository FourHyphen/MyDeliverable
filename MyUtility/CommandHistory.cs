using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUtility
{
    public class CommandHistory<T> where T : class
    {
        private List<T> CommandList { get; set; } = new List<T>();

        private int Index { get; set; } = -1;

        private int MaxIndex { get { return CommandList.Count - 1; } }

        public void Execute(T commandInstance, string methodName, object[] args = null)
        {
            Type[] overload = GetOverloadArgs(args);
            System.Reflection.MethodInfo method = commandInstance.GetType().GetMethod(methodName, overload);
            method.Invoke(commandInstance, args);
            AddHistory(commandInstance);
        }

        private Type[] GetOverloadArgs(object[] args)
        {
            if (args == null)
            {
                return new Type[0];
            }

            Type[] overload = new Type[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                var tmp = args[i];
                overload[i] = tmp.GetType();
            }

            return overload;
        }

        private void AddHistory(T commandInstance)
        {
            //// 操作後のパラメーターに変化がない場合、その操作を保持しない
            //if (commandInstance.After.Equals(commandInstance.Before))
            //{
            //    return;
            //}

            if (Index < MaxIndex)
            {
                // 履歴の途中に操作を差し込んだ場合、旧最新操作履歴を削除する
                DeleteNewerHistory();
            }

            // パラメーター変更された操作を履歴の最新操作とする
            AddHistoryCore(commandInstance);
        }

        private void DeleteNewerHistory()
        {
            // 例
            // Index:                        ↓(= 1)
            // 履歴 :  [0] Init  [1] Key.Down  [2] Key.Down  [3] Key.Up
            //                                ↑ここに操作を入れる場合、2つ削除する
            //  -> RemoveRange(2, deleteNum = 2)
            int deleteNum = MaxIndex - Index;    // = 現時点のIndexの次から全てを削除
            int deleteStart = Index + 1;
            CommandList.RemoveRange(deleteStart, deleteNum);
        }

        private void AddHistoryCore(T commandInstance)
        {
            CommandList.Add(commandInstance);
            Index++;
        }

        /// <summary>
        /// Undo し、Undo後の現在履歴の Execute したインスタンスを返す
        /// </summary>
        /// <param name="undoNum"></param>
        /// <returns></returns>
        public object Undo(int undoNum)
        {
            return UndoCore(undoNum);
        }

        /// <summary>
        /// Undo し、Undo後の現在履歴の Execute したインスタンスのプロパティ値を返す
        /// </summary>
        /// <param name="undoNum"></param>
        /// <param name="returnPropertyName"></param>
        /// <returns></returns>
        public object Undo(int undoNum, string returnPropertyName)
        {
            return UndoCore(undoNum, returnPropertyName);
        }

        private object UndoCore(int undoNum, string returnPropertyName = "")
        {
            Index = System.Math.Max(-1, Index - undoNum);
            return Index < 0 ? null : Now(returnPropertyName);
        }

        private object Now(string propertyName)
        {
            if (propertyName == "")
            {
                return CommandList[Index];
            }
            else
            {
                return GetPropertyOfReturn(propertyName);
            }
        }

        private object GetPropertyOfReturn(string propertyName)
        {
            //return CommandList[Index].After;
            return CommandList[Index].GetType().GetProperty(propertyName).GetValue(CommandList[Index]);
        }

        /// <summary>
        /// Redo し、Redo後の現在履歴の Execute したインスタンスを返す
        /// </summary>
        /// <param name="redoNum"></param>
        /// <returns></returns>
        public object Redo(int redoNum)
        {
            return RedoCore(redoNum);
        }

        /// <summary>
        /// Redo し、Redo後の現在履歴の Execute したインスタンスのプロパティ値を返す
        /// </summary>
        /// <param name="redoNum"></param>
        /// <param name="returnPropertyName"></param>
        /// <returns></returns>
        public object Redo(int redoNum, string returnPropertyName)
        {
            return RedoCore(redoNum, returnPropertyName);
        }

        private object RedoCore(int redoNum, string returnPropertyName = "")
        {
            Index = System.Math.Min(Index + redoNum, MaxIndex);
            return Now(returnPropertyName);
        }
    }
}
