namespace MyUtility
{
    class Task
    {
        private void ExampleDelay()
        {
            DelayCore();
        }

        private async void DelayCore()
        {
            // 非同期待機: Sleep()だと同期待機なのでブロックしてしまう
            await System.Threading.Tasks.Task.Delay(1000);
        }
    }
}
