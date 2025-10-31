using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdojarasNaplo
{
	public class ImageProcessing : IImageProcessing
	{
		public static void Grayscale(Bitmap b)
		{
			BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),
				ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			IntPtr Scan0 = bmData.Scan0;

			byte[] redLUT = new byte[256];
			byte[] greenLUT = new byte[256];
			byte[] blueLUT = new byte[256];
			for (int i = 0; i < 256; i++)
			{
				redLUT[i] = (byte)((299 * i) / 1000);
				greenLUT[i] = (byte)((587 * i) / 1000);
				blueLUT[i] = (byte)((114 * i) / 1000);
			}

			unsafe
			{
				byte* pBase = (byte*)Scan0;

				int nHeight = b.Height;
				int nWidth = b.Width;

				Parallel.For(0, nHeight, y =>
				{
					byte* p = pBase + y * stride;

					for (int x = 0; x < nWidth; ++x)
					{
						int gray = (redLUT[p[2]] + greenLUT[p[1]] + blueLUT[p[0]]);
						p[0] = p[1] = p[2] = (byte)gray;
						p += 3;
					}
				});
			}

			b.UnlockBits(bmData);
		}
	}
}
