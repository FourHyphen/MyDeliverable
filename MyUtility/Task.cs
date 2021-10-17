namespace MyUtility
{
    public static class Task
    {
        /// <summary>
        /// 別タスクの処理を動かしつつ本処理側を待機したい場合に実行する
        /// </summary>
        /// <param name="second"></param>
        public static void Delay(double second)
        {
            System.Threading.Tasks.Task t = System.Threading.Tasks.Task.Delay((int)(second * 1000.0));
            t.Wait();

            // メモ: これと同じ効果
            //System.Threading.Tasks.Task task = System.Threading.Tasks.Task.Run(() =>
            //{
            //    System.Threading.Thread.Sleep((int)(second * 1000.0));
            //});
            //System.Threading.Tasks.Task.WaitAll(task);
        }
    }
}
