using System.Threading.Tasks;

namespace Step5
{
    public class ImageGenerator
    {
        public static async Task<ImageObject> GenerateAsync()
        {
            await Task.Delay(10);
            return new ImageObject(10 * 1024 * 1024);
        }
    }
}