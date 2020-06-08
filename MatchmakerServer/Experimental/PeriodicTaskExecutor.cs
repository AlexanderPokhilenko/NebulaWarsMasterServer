using System;
using System.Threading;
using System.Threading.Tasks;

namespace AmoebaGameMatcherServer.Services
{
    /// <summary>
    /// Отвечает за периодический вызов callback-a
    /// </summary>
    public class PeriodicTaskExecutor
    {
        private readonly Func<Task> callback;
        private readonly TimeSpan delay;

        public PeriodicTaskExecutor(Func<Task> callback, TimeSpan delay)
        {
            this.callback = callback;
            this.delay = delay;
        }
        
        public void StartThread()
        {
            Thread thread = new Thread(PeriodicMethodCall);
            thread.Start();
        }
        
        private async void PeriodicMethodCall()
        {
            while (true)
            {
                await Task.Delay(delay);
                await callback.Invoke();
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}