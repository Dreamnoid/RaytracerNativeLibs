using System;
using System.Runtime.InteropServices;

namespace RaytracerNativeLibs.Interop
{
    internal class CBuffer : IDisposable
    {
        public IntPtr Pointer { get; private set; }
        public int Length { get; private set; }

        public CBuffer(float[] buffer)
        {
            Pointer = Marshal.AllocHGlobal(buffer.Length * 4);
            Length = buffer.Length;
            Marshal.Copy(buffer, 0, Pointer, buffer.Length);
        }

        public CBuffer(int[] buffer)
        {
            Pointer = Marshal.AllocHGlobal(buffer.Length * 4);
            Length = buffer.Length;
            Marshal.Copy(buffer, 0, Pointer, buffer.Length);
        }

        public void Download(float[] buffer)
        {
            Marshal.Copy(Pointer, buffer, 0, buffer.Length);
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal(Pointer);
        }
    }
}
