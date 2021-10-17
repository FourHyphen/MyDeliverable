using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyUtility
{
    /// <summary>
    /// 一定時間毎に指定した処理を実行するクラス
    /// </summary>
    public class TaskService : IDisposable
    {
        private System.Timers.Timer Timer { get; set; }

        public TaskService(string className, string methodName, object[] args, int tillDoTimeMilliseconds)
        {
            MethodInfo method = Reflection.GetMethod(className, methodName, args);
            Init(method, null, args, tillDoTimeMilliseconds);
        }

        public TaskService(object instance, string methodName, object[] args, int tillDoTimeMilliseconds)
        {
            MethodInfo method = Reflection.GetMethod(instance, methodName, args);
            Init(method, instance, args, tillDoTimeMilliseconds);
        }

        private void Init(MethodInfo method, object instance, object[] args, int tillDoTimeMilliseconds)
        {
            Timer = new System.Timers.Timer();
            Timer.AutoReset = true;
            Timer.Interval = tillDoTimeMilliseconds;
            Timer.Elapsed += (s, e) =>
            {
                method.Invoke(instance, args);
            };
        }

        public void Dispose()
        {
            if (Timer != null)
            {
                Timer.Stop();
                Timer.Dispose();
                Timer = null;
            }
        }

        /// <summary>
        /// 開始
        /// </summary>
        /// <remarks>すでに開始済みなら何もしない</remarks>
        public void Start()
        {
            if (!Timer.Enabled)
            {
                Timer.Start();
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <remarks>経過時間はリセットされるため、再 Start() 時のカウントは最初からになる</remarks>
        public void Stop()
        {
            // TimerはStopするだけで経過時刻がリセットされる
            Timer.Stop();
        }
    }
}
