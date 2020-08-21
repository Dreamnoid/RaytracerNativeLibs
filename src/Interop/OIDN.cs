using System;
using System.Runtime.InteropServices;
using System.Security;

namespace RaytracerNativeLibs.Interop
{
    public enum OIDNDeviceType
    {
        OIDN_DEVICE_TYPE_DEFAULT = 0, // select device automatically
        OIDN_DEVICE_TYPE_CPU = 1, // CPU device
    }

    public enum OIDNFormat
    {
        OIDN_FORMAT_UNDEFINED = 0,

        // 32-bit single-precision floating point scalar and vector formats
        OIDN_FORMAT_FLOAT = 1,
        OIDN_FORMAT_FLOAT2 = 2,
        OIDN_FORMAT_FLOAT3 = 3,
        OIDN_FORMAT_FLOAT4 = 4,
    }

    public static class OIDN
    {
        public const string DLLName = "OpenImageDenoise.dll";

        [SuppressUnmanagedCodeSecurity, DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr oidnNewDevice(OIDNDeviceType type);

        [SuppressUnmanagedCodeSecurity, DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void oidnReleaseDevice(IntPtr device);

        [SuppressUnmanagedCodeSecurity, DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void oidnCommitDevice(IntPtr device);

        [SuppressUnmanagedCodeSecurity, DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void oidnSetDevice1b(IntPtr device, string name, bool value);

        [SuppressUnmanagedCodeSecurity, DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr oidnNewFilter(IntPtr device, string name);

        [SuppressUnmanagedCodeSecurity, DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void oidnReleaseFilter(IntPtr filter);

        [SuppressUnmanagedCodeSecurity, DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void oidnExecuteFilter(IntPtr filter);

        [SuppressUnmanagedCodeSecurity, DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void oidnCommitFilter(IntPtr filter);

        [SuppressUnmanagedCodeSecurity, DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void oidnSetFilter1b(IntPtr filter, string name, bool value);

        [SuppressUnmanagedCodeSecurity, DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void oidnSetSharedFilterImage(IntPtr filter, string name, IntPtr ptr, OIDNFormat format, uint width, uint height, uint byteOffset, uint bytePixelStride, uint byteRowStride);
    }
}
