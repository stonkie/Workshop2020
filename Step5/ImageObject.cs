using System;

namespace Step5
{
    public class ImageObject
    {
        private readonly byte[] _nativeInternalBuffer;

        public ImageObject(int size)
        {
            _nativeInternalBuffer = new byte[size];
            Array.Fill(_nativeInternalBuffer, (byte)12);
        }

        private void ReleaseUnmanagedResources()
        {
            // Our buffer isn't really native, but if it was on a graphics card for example, we'd clean it up here
        }

        ~ImageObject()
        {
            ReleaseUnmanagedResources();
        }
    }
}