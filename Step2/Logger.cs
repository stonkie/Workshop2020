using System;

namespace Step2
{
    public static class Logger
    {
        public static void Error(Exception exception)
        {
            Console.WriteLine(exception.ToString());
        }
    }
}