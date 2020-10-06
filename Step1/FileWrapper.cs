using System;
using System.Threading;
using System.Threading.Tasks;

namespace Step1
{
    internal class FileWrapper
    {
        public static void ReadFile(string testTxt)
        {
            Thread.Sleep(TimeSpan.FromDays(1));
        }

        public static async Task ReadFileAsync(string testTxt)
        {
            await Task.Delay(TimeSpan.FromDays(1));
        }
    }
}