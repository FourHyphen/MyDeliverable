using System;
using System.Reflection;
using System.Threading;

namespace MyUtility
{
    /// <summary>
    /// 別タスクで処理を絶え間なく実行させる(実行 -> 再実行の間の Sleep なし)
    /// </summary>
    public class TaskServiceNoWait : IDisposable
    {
        private CancellationTokenSource ToCallStop { get; set; }

        private object Instance { get; set; }

        private MethodInfo Method { get; set; }

        private object[] Args { get; set; }

        public TaskServiceNoWait(string className, string methodName, object[] args = null)
        {
            MethodInfo method = MyUtility.Reflection.GetMethod(className, methodName, args);
            Init(null, method, args);
        }

        public TaskServiceNoWait(object instance, string methodName, object[] args = null)
        {
            MethodInfo method = MyUtility.Reflection.GetMethod(instance, methodName, args);
            Init(instance, method, args);
        }

        private void Init(object instance, MethodInfo method, object[] args)
        {
            ToCallStop = new CancellationTokenSource();
            Instance = instance;
            Method = method;
            Args = args;
        }

        public void Dispose()
        {
            ToCallStop?.Dispose();
            Instance = null;
            Args = null;
            System.GC.SuppressFinalize(this);
        }

        public void Start()
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                Doing();
            });
        }

        private void Doing()
        {
            CancellationToken ct = ToCallStop.Token;
            while (true)
            {
                Method.Invoke(Instance, Args);

                if (ct.IsCancellationRequested)
                {
                    return;
                }
            }
        }

        public void Stop()
        {
            ToCallStop.Cancel();
        }

        public void Restart()
        {
            ToCallStop?.Dispose();
            ToCallStop = new CancellationTokenSource();
            Start();
        }
    }
}
