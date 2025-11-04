using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdojarasNaplo
{
	public static class ImageProcessing
	{

		public static async Task<string> ConvertToGrayScaleAsync(string path)
		{
			using var input = File.OpenRead(path);
			using var original = SKBitmap.Decode(input);

			var gray = new SKBitmap(original.Width, original.Height);
			for (int y = 0; y < original.Height; y++)
			{
				for (int x = 0; x < original.Width; x++)
				{
					var color = original.GetPixel(x, y);

					byte g = (byte)(0.3 * color.Red + 0.59 * color.Green + 0.11 * color.Blue);

					gray.SetPixel(x, y, new SKColor(g, g, g));
				}
			}

			string grayPath = Path.Combine(FileSystem.AppDataDirectory, $"gray_{Path.GetFileName(path)}");

			using var output = File.OpenWrite(grayPath);
			using var image = SKImage.FromBitmap(gray);
			using var data = image.Encode(SKEncodedImageFormat.Jpeg, 90);
			data.SaveTo(output);

			return grayPath;
		}
	}
}
