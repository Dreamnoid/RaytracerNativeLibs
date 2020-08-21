using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security;

namespace RaytracerNativeLibs.Interop
{
    public enum RTCGeometryType
    {
        RTC_GEOMETRY_TYPE_TRIANGLE = 0, // triangle mesh
        RTC_GEOMETRY_TYPE_QUAD = 1, // quad (triangle pair) mesh
        RTC_GEOMETRY_TYPE_GRID = 2, // grid mesh

        RTC_GEOMETRY_TYPE_SUBDIVISION = 8, // Catmull-Clark subdivision surface

        RTC_GEOMETRY_TYPE_FLAT_LINEAR_CURVE = 17, // flat (ribbon-like) linear curves

        RTC_GEOMETRY_TYPE_ROUND_BEZIER_CURVE = 24, // round (tube-like) Bezier curves
        RTC_GEOMETRY_TYPE_FLAT_BEZIER_CURVE = 25, // flat (ribbon-like) Bezier curves
        RTC_GEOMETRY_TYPE_NORMAL_ORIENTED_BEZIER_CURVE = 26, // flat normal-oriented Bezier curves

        RTC_GEOMETRY_TYPE_ROUND_BSPLINE_CURVE = 32, // round (tube-like) B-spline curves
        RTC_GEOMETRY_TYPE_FLAT_BSPLINE_CURVE = 33, // flat (ribbon-like) B-spline curves
        RTC_GEOMETRY_TYPE_NORMAL_ORIENTED_BSPLINE_CURVE = 34, // flat normal-oriented B-spline curves

        RTC_GEOMETRY_TYPE_ROUND_HERMITE_CURVE = 40, // round (tube-like) Hermite curves
        RTC_GEOMETRY_TYPE_FLAT_HERMITE_CURVE = 41, // flat (ribbon-like) Hermite curves
        RTC_GEOMETRY_TYPE_NORMAL_ORIENTED_HERMITE_CURVE = 42, // flat normal-oriented Hermite curves

        RTC_GEOMETRY_TYPE_SPHERE_POINT = 50,
        RTC_GEOMETRY_TYPE_DISC_POINT = 51,
        RTC_GEOMETRY_TYPE_ORIENTED_DISC_POINT = 52,

        RTC_GEOMETRY_TYPE_USER = 120, // user-defined geometry
        RTC_GEOMETRY_TYPE_INSTANCE = 121  // scene instance
    };

    public enum RTCBufferType
    {
        RTC_BUFFER_TYPE_INDEX = 0,
        RTC_BUFFER_TYPE_VERTEX = 1,
        RTC_BUFFER_TYPE_VERTEX_ATTRIBUTE = 2,
        RTC_BUFFER_TYPE_NORMAL = 3,
        RTC_BUFFER_TYPE_TANGENT = 4,
        RTC_BUFFER_TYPE_NORMAL_DERIVATIVE = 5,

        RTC_BUFFER_TYPE_GRID = 8,

        RTC_BUFFER_TYPE_FACE = 16,
        RTC_BUFFER_TYPE_LEVEL = 17,
        RTC_BUFFER_TYPE_EDGE_CREASE_INDEX = 18,
        RTC_BUFFER_TYPE_EDGE_CREASE_WEIGHT = 19,
        RTC_BUFFER_TYPE_VERTEX_CREASE_INDEX = 20,
        RTC_BUFFER_TYPE_VERTEX_CREASE_WEIGHT = 21,
        RTC_BUFFER_TYPE_HOLE = 22,

        RTC_BUFFER_TYPE_FLAGS = 32
    };

    public enum RTCFormat
    {
        RTC_FORMAT_UNDEFINED = 0,

        /* 8-bit unsigned integer */
        RTC_FORMAT_UCHAR = 0x1001,
        RTC_FORMAT_UCHAR2,
        RTC_FORMAT_UCHAR3,
        RTC_FORMAT_UCHAR4,

        /* 8-bit signed integer */
        RTC_FORMAT_CHAR = 0x2001,
        RTC_FORMAT_CHAR2,
        RTC_FORMAT_CHAR3,
        RTC_FORMAT_CHAR4,

        /* 16-bit unsigned integer */
        RTC_FORMAT_USHORT = 0x3001,
        RTC_FORMAT_USHORT2,
        RTC_FORMAT_USHORT3,
        RTC_FORMAT_USHORT4,

        /* 16-bit signed integer */
        RTC_FORMAT_SHORT = 0x4001,
        RTC_FORMAT_SHORT2,
        RTC_FORMAT_SHORT3,
        RTC_FORMAT_SHORT4,

        /* 32-bit unsigned integer */
        RTC_FORMAT_UINT = 0x5001,
        RTC_FORMAT_UINT2,
        RTC_FORMAT_UINT3,
        RTC_FORMAT_UINT4,

        /* 32-bit signed integer */
        RTC_FORMAT_INT = 0x6001,
        RTC_FORMAT_INT2,
        RTC_FORMAT_INT3,
        RTC_FORMAT_INT4,

        /* 64-bit unsigned integer */
        RTC_FORMAT_ULLONG = 0x7001,
        RTC_FORMAT_ULLONG2,
        RTC_FORMAT_ULLONG3,
        RTC_FORMAT_ULLONG4,

        /* 64-bit signed integer */
        RTC_FORMAT_LLONG = 0x8001,
        RTC_FORMAT_LLONG2,
        RTC_FORMAT_LLONG3,
        RTC_FORMAT_LLONG4,

        /* 32-bit float */
        RTC_FORMAT_FLOAT = 0x9001,
        RTC_FORMAT_FLOAT2,
        RTC_FORMAT_FLOAT3,
        RTC_FORMAT_FLOAT4,
        RTC_FORMAT_FLOAT5,
        RTC_FORMAT_FLOAT6,
        RTC_FORMAT_FLOAT7,
        RTC_FORMAT_FLOAT8,
        RTC_FORMAT_FLOAT9,
        RTC_FORMAT_FLOAT10,
        RTC_FORMAT_FLOAT11,
        RTC_FORMAT_FLOAT12,
        RTC_FORMAT_FLOAT13,
        RTC_FORMAT_FLOAT14,
        RTC_FORMAT_FLOAT15,
        RTC_FORMAT_FLOAT16,

        /* 32-bit float matrix (row-major order) */
        RTC_FORMAT_FLOAT2X2_ROW_MAJOR = 0x9122,
        RTC_FORMAT_FLOAT2X3_ROW_MAJOR = 0x9123,
        RTC_FORMAT_FLOAT2X4_ROW_MAJOR = 0x9124,
        RTC_FORMAT_FLOAT3X2_ROW_MAJOR = 0x9132,
        RTC_FORMAT_FLOAT3X3_ROW_MAJOR = 0x9133,
        RTC_FORMAT_FLOAT3X4_ROW_MAJOR = 0x9134,
        RTC_FORMAT_FLOAT4X2_ROW_MAJOR = 0x9142,
        RTC_FORMAT_FLOAT4X3_ROW_MAJOR = 0x9143,
        RTC_FORMAT_FLOAT4X4_ROW_MAJOR = 0x9144,

        /* 32-bit float matrix (column-major order) */
        RTC_FORMAT_FLOAT2X2_COLUMN_MAJOR = 0x9222,
        RTC_FORMAT_FLOAT2X3_COLUMN_MAJOR = 0x9223,
        RTC_FORMAT_FLOAT2X4_COLUMN_MAJOR = 0x9224,
        RTC_FORMAT_FLOAT3X2_COLUMN_MAJOR = 0x9232,
        RTC_FORMAT_FLOAT3X3_COLUMN_MAJOR = 0x9233,
        RTC_FORMAT_FLOAT3X4_COLUMN_MAJOR = 0x9234,
        RTC_FORMAT_FLOAT4X2_COLUMN_MAJOR = 0x9242,
        RTC_FORMAT_FLOAT4X3_COLUMN_MAJOR = 0x9243,
        RTC_FORMAT_FLOAT4X4_COLUMN_MAJOR = 0x9244,

        /* special 12-byte format for grids */
        RTC_FORMAT_GRID = 0xA001
    };

    public enum RTCIntersectContextFlags
    {
        RTC_INTERSECT_CONTEXT_FLAG_NONE = 0,
        RTC_INTERSECT_CONTEXT_FLAG_INCOHERENT = (0 << 0), // optimize for incoherent rays
        RTC_INTERSECT_CONTEXT_FLAG_COHERENT = (1 << 0)  // optimize for coherent rays
    };

    public struct RTCIntersectContext
    {
        public RTCIntersectContextFlags flags;
        public IntPtr filter;
        public int instID;

        public static RTCIntersectContext rtcInitIntersectContext()
        {
            return new RTCIntersectContext()
            {
                flags = RTCIntersectContextFlags.RTC_INTERSECT_CONTEXT_FLAG_INCOHERENT,
                filter = IntPtr.Zero,
                instID = -1
            };
        }
    }

    public struct RTCRay
    {
        public float orgX;
        public float orgY;
        public float orgZ;
        public float tnear;

        public float dirX;
        public float dirY;
        public float dirZ;
        public float time;

        public float tfar;
        public uint mask;
        public uint id;
        public uint flags;

        public RTCRay(in Vector3 start, in Vector3 dir, float near = 0.01f)
        {
            orgX = start.X;
            orgY = start.Y;
            orgZ = start.Z;

            dirX = dir.X;
            dirY = dir.Y;
            dirZ = dir.Z;

            tnear = near;
            tfar = float.MaxValue;

            time = 0f;
            mask = uint.MaxValue;
            id = 0;
            flags = 0;
        }
    }

    public struct RTCHit
    {
        public float NgX;
        public float NgY;
        public float NgZ;

        public float u;
        public float v;

        public int geomID;
        public int primID;
        public int instID;
    }

    [StructLayout(LayoutKind.Explicit, Size = 80)]
    public struct RTCRayHit
    {
        [FieldOffset(0)] public RTCRay ray;
        [FieldOffset(48)] public RTCHit hit;
    }


    public static class Embree
    {
        public const string DLLName = "embree3.dll";

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr rtcNewDevice(string config);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rtcReleaseDevice(IntPtr device);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr rtcNewScene(IntPtr device);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rtcReleaseScene(IntPtr scene);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr rtcNewGeometry(IntPtr device, RTCGeometryType type);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rtcReleaseGeometry(IntPtr geometry);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rtcCommitGeometry(IntPtr geometry);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint rtcAttachGeometry(IntPtr scene, IntPtr geometry);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rtcCommitScene(IntPtr scene);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rtcSetSharedGeometryBuffer(IntPtr geometry, RTCBufferType type, uint slot, RTCFormat format, IntPtr ptr, uint byteOffset, uint byteStride, uint itemCount);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rtcIntersect1(IntPtr scene, ref RTCIntersectContext context, ref RTCRayHit rayhit);

        [DllImport(DLLName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void rtcOccluded1(IntPtr scene, ref RTCIntersectContext context, ref RTCRay ray);

    }
}
