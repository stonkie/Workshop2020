using System;

namespace Step2_framework
{
    public static class Logger
    {
        public static void Error(Exception exception)
        {
            Console.WriteLine(exception.ToString());
        }
    }
}