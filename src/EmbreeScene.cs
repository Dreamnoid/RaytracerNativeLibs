using RaytracerNativeLibs.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace RaytracerNativeLibs
{
    public class EmbreeScene : IDisposable
    {
        private readonly IntPtr _device;
        private readonly IntPtr _scene;
        private readonly IntPtr _geo;

        private readonly CBuffer _vertices;
        private readonly CBuffer _indices;

        public EmbreeScene(Vector3[] vertices, int[] indices = null)
        {
            indices = indices ?? vertices.Select((v, idx) => idx).ToArray();

            _vertices = new CBuffer(vertices.SelectMany(v => new[] { v.X, v.Y, v.Z }).ToArray());
            _indices = new CBuffer(indices);

            _device = Embree.rtcNewDevice(null);

            _geo = Embree.rtcNewGeometry(_device, RTCGeometryType.RTC_GEOMETRY_TYPE_TRIANGLE);
            Embree.rtcSetSharedGeometryBuffer(_geo, RTCBufferType.RTC_BUFFER_TYPE_VERTEX, 0, RTCFormat.RTC_FORMAT_FLOAT3, _vertices.Pointer, 0, sizeof(float) * 3, (uint)vertices.Length);
            Embree.rtcSetSharedGeometryBuffer(_geo, RTCBufferType.RTC_BUFFER_TYPE_INDEX, 0, RTCFormat.RTC_FORMAT_UINT3, _indices.Pointer, 0, sizeof(uint) * 3, (uint)(indices.Length / 3));
            Embree.rtcCommitGeometry(_geo);

            _scene = Embree.rtcNewScene(_device);
            Embree.rtcAttachGeometry(_scene, _geo);
            Embree.rtcCommitScene(_scene);
        }

        public void Dispose()
        {
            Embree.rtcReleaseScene(_scene);
            Embree.rtcReleaseGeometry(_geo);
            Embree.rtcReleaseDevice(_device);

            _indices.Dispose();
            _vertices.Dispose();
        }

        public bool Intersect(in Vector3 start, in Vector3 dir, out int triangleIndex, out Vector3 hit , out Vector3 normal)
        {
            var context = RTCIntersectContext.rtcInitIntersectContext();

            var rh = new RTCRayHit()
            {
                ray = new RTCRay(start, dir),
                hit = new RTCHit()
                {
                    primID = -1,
                    geomID = -1
                }
            };

            Embree.rtcIntersect1(_scene, ref context, ref rh);

            if (rh.ray.tfar != float.MaxValue)
            {
                triangleIndex = rh.hit.geomID;
                hit = start + dir * rh.ray.tfar;
                normal = new Vector3(rh.hit.NgX, rh.hit.NgY, rh.hit.NgZ);
                return true;
            }

            triangleIndex = 0;
            hit = Vector3.Zero;
            normal = Vector3.Zero;
            return false;
        }

        public bool Occluded(in Vector3 start, in Vector3 dir)
        {
            var context = RTCIntersectContext.rtcInitIntersectContext();
            var ray = new RTCRay(start, dir);
            Embree.rtcOccluded1(_scene, ref context, ref ray);
            return (ray.tfar != float.MaxValue);
        }
    }
}
