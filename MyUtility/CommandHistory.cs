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

        private int Offset { get; set; } = -1;

        private int MaxOffset { get { return CommandList.Count - 1; } }

        public void Execute(T commandInstance, string methodName, object[] args = null)
        {
            MyUtility.Reflection.DoMethod<T>(commandInstance, methodName, args);
            AddHistory(commandInstance);
        }

        private void AddHistory(T commandInstance)
        {
            //// 操作後のパラメーターに変化がない場合、その操作を保持しない
            //if (commandInstance.After.Equals(commandInstance.Before))
            //{
            //    return;
            //}

            if (Offset < MaxOffset)
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
            // Offset:           ↓(= 1)
            // 履歴  :  [0] Init  [1] Key.Down  [2] Key.Down  [3] Key.Up
            //                   ↑ここに操作を入れる場合、3つ削除する
            //  -> RemoveRange(1, deleteNum = 3)
            int deleteStart = Offset + 1;    // = 現時点のOffsetの次以降を全て削除
            int deleteNum = MaxOffset - Offset;
            CommandList.RemoveRange(deleteStart, deleteNum);
        }

        private void AddHistoryCore(T commandInstance)
        {
            CommandList.Add(commandInstance);
            Offset++;
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
            Offset = System.Math.Max(-1, Offset - undoNum);
            return Offset < 0 ? null : Now(returnPropertyName);
        }

        private object Now(string propertyName)
        {
            if (propertyName == "")
            {
                return CommandList[Offset];
            }
            else
            {
                return GetPropertyOfReturn(propertyName);
            }
        }

        private object GetPropertyOfReturn(string propertyName)
        {
            //return CommandList[Index].After;
            return CommandList[Offset].GetType().GetProperty(propertyName).GetValue(CommandList[Offset]);
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
            Offset = System.Math.Min(Offset + redoNum, MaxOffset);
            return Now(returnPropertyName);
        }
    }
}
