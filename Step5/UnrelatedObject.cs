using System.Threading;

namespace Step5
{
    public class UnrelatedObject
    {
        private static readonly SemaphoreSlim _nativeResourceLock = new SemaphoreSlim(0, 1); // Protects some native resource

        private void ReleaseUnmanagedResources()
        {
            // Deadlocks when trying to cleanup some native resource
            _nativeResourceLock.Wait(); 
        }

        ~UnrelatedObject()
        {
            ReleaseUnmanagedResources();
        }
    }
}