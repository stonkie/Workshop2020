using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Step3
{
    public class Block<T>
    {
        public static async Task<Block<T>> CreateBlockAsync(Block<T>? previous, T value, Func<T, byte[]> binaryConverter)
        {
            await Task.Yield();

            DateTime time = DateTime.UtcNow;

            await using MemoryStream stream = new MemoryStream();

            await stream.WriteAsync(previous?.Hash);
            await stream.WriteAsync(BitConverter.GetBytes(time.Ticks));
            await stream.WriteAsync(binaryConverter(value));

            byte[] hash = SHA256.Create().ComputeHash(stream.ToArray());

            return new Block<T>(previous, time, value, hash);
        }

        public Block<T>? Previous { get; }
        public DateTime Time { get; }
        public T Value { get; }
        public byte[] Hash { get; }

        private Block(Block<T>? previous, DateTime time, T value, byte[] hash)
        {
            Previous = previous;
            Time = time;
            Value = value;
            Hash = hash;
        }
    }
}