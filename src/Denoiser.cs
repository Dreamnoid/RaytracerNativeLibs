using RaytracerNativeLibs.Interop;
using System.Linq;
using System.Numerics;

namespace RaytracerNativeLibs
{
    public static class Denoiser
    {
        public static void Denoise(Vector4[] buffer, int width, int height)
        {
			var outputArray = new float[width * height * 4];

			using (var colorBuffer = new CBuffer(buffer.SelectMany(v => new[] { v.X, v.Y, v.Z, v.W }).ToArray()))
			using (var outputBuffer = new CBuffer(outputArray))
			{
				var odevice = OIDN.oidnNewDevice(OIDNDeviceType.OIDN_DEVICE_TYPE_DEFAULT);
				OIDN.oidnSetDevice1b(odevice, "setAffinity", false);
				OIDN.oidnCommitDevice(odevice);

				var filter = OIDN.oidnNewFilter(odevice, "RT");
				OIDN.oidnSetSharedFilterImage(filter, "color", colorBuffer.Pointer, OIDNFormat.OIDN_FORMAT_FLOAT3, (uint)width, (uint)height, 0, 4 * 4, 0);
				OIDN.oidnSetSharedFilterImage(filter, "output", outputBuffer.Pointer, OIDNFormat.OIDN_FORMAT_FLOAT3, (uint)width, (uint)height, 0, 4 * 4, 0);

				OIDN.oidnCommitFilter(filter);
				OIDN.oidnExecuteFilter(filter);
				OIDN.oidnReleaseDevice(filter);
				OIDN.oidnReleaseDevice(odevice);

				outputBuffer.Download(outputArray);
			}

			for (int y = 0; y < height; ++y)
			{
				for (int x = 0; x < width; ++x)
				{
					var index = y * width + x;
					var r = outputArray[index * 4 + 0];
					var g = outputArray[index * 4 + 1];
					var b = outputArray[index * 4 + 2];
					var a = outputArray[index * 4 + 3];
					buffer[index] = new Vector4(r, g, b, buffer[index].W);
				}
			}
		}
    }
}
