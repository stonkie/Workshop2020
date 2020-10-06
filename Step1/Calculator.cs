using System.Threading.Tasks;

namespace Step1
{
    internal class Calculator
    {
        public static void Calculate()
        {
            while (true) { }
        }

        public static async Task CalculateAsync()
        {
            await Task.Run(() =>
            {
                while (true) { }
            }).ConfigureAwait(false);
        }
    }
}