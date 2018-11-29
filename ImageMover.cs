using System;
using UnityEngine;
using UnityEngine.UI;

	public class ImageMover
	{
		public static Texture2D flipImageHorizontally (Texture2D txt)
		{

			Texture2D flipped = new Texture2D (txt.width, txt.height);


			int xN = txt.width;
			int yN = txt.height;


			for (int i = 0; i < xN; i++) {
				for (int j = 0; j < yN; j++) {
					flipped.SetPixel (xN - i - 1, j, txt.GetPixel (i, j));
				}
			}
			flipped.Apply ();

			return flipped;

		}

	public static Texture2D flipImageVertically (Texture2D txt)
	{

		Texture2D flipped = new Texture2D (txt.width, txt.height);


		int xN = txt.width;
		int yN = txt.height;


		for (int i = 0; i < xN; i++) {
			for (int j = 0; j < yN; j++) {
				flipped.SetPixel (i, yN -j - 1, txt.GetPixel (i, j));
			}
		}
		flipped.Apply ();

		return flipped;

	}


	}


