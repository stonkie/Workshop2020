using System;
using System.Threading;
using System.Threading.Tasks;

namespace Step2_framework
{
    public class Engine
    {
        public event EventHandler? FuelLevelUpdated;

        private int _fuelLevel;
        public int FuelLevel => _fuelLevel;

        private Task? _engineThread;

        public void Refuel()
        {
            _fuelLevel = 100;

            FuelLevelUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void Start()
        {
            _engineThread ??= Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(500);

                    int fuelLevel = _fuelLevel;

                    if (fuelLevel > 0)
                    {
                        if (Interlocked.CompareExchange(ref _fuelLevel, fuelLevel - 5, fuelLevel) == fuelLevel)
                        {
                            FuelLevelUpdated?.Invoke(this, EventArgs.Empty);
                        }
                    }

                    GC.Collect();
                }
            });
        }

        public async Task CarefulShutdownAsync()
        {
            TaskCompletionSource<bool> completionSource = new TaskCompletionSource<bool>();

            try
            {
                StartShutdown(completionSource);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            await completionSource.Task;
        }

        public async Task ForceShutdownAsync()
        {
            TaskCompletionSource<bool> completionSource = new TaskCompletionSource<bool>();
            StartShutdown(completionSource);
            await completionSource.Task;
        }

        private void StartShutdown(TaskCompletionSource<bool> completionSource)
        {
            if (_fuelLevel > 0)
            {
                throw new InvalidOperationException("Cannot shutdown when fuel remains. Could cause an explosion.");
            }

            completionSource.SetResult(true);
        }
    }
}