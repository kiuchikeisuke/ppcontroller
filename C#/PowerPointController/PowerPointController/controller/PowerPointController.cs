using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace PowerPointController.controller
{
	class PowerPointController
	{
		private static String FilePath = null;
		private static Microsoft.Office.Interop.PowerPoint.Application App = null;
		private static Microsoft.Office.Interop.PowerPoint.Presentation Ppt = null;
		private static int[] slideIndex;
		private static String imageFilePath = null;

		public static void LoadPowerPoint(String filePath)
		{
			if(filePath == null || filePath == ""){
				return;
			}
			FilePath = filePath;
			App = new Microsoft.Office.Interop.PowerPoint.Application();
			App.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;

			Ppt = App.Presentations.Open(filePath, 
				Microsoft.Office.Core.MsoTriState.msoTrue, 
				Microsoft.Office.Core.MsoTriState.msoTrue, 
				Microsoft.Office.Core.MsoTriState.msoTrue);

			slideIndex = new int[Ppt.Slides.Count];
			for (int i = 0; i < slideIndex.Length; i++)
			{
				slideIndex[i] = i + 1;
			}
			            
			imageFilePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\slides";
			if (Directory.Exists(imageFilePath) == false)
			{
				Directory.CreateDirectory(imageFilePath);
			}
			try
			{
				for (int i = 1; i <= Ppt.Slides.Count; i++)
				{
					string filepath = imageFilePath + String.Format("\\slide{0:0000}.jpg", i);
					Ppt.Slides[i].Export(filepath, "jpg", 230, 180);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);

			}

		}

		public static void next()
		{
			if (Ppt == null)
				return;

			if (Ppt.Slides.Count >= Ppt.SlideShowWindow.View.CurrentShowPosition)
			{
				Ppt.SlideShowWindow.View.Next();
			}
		}
		public static int getSlideLength()
		{
			if (Ppt == null)
			{
				return -1;
			}
			return Ppt.Slides.Count;
		}

		public static void jump(int index)
		{
			if (Ppt == null || Ppt.Slides.Count < index)
			{
				return;
			}
			Ppt.SlideShowWindow.View.GotoSlide(index, Microsoft.Office.Core.MsoTriState.msoTrue);
		}

		public static Bitmap getSlideImageStream(int index)
		{
			if (Ppt == null)
			{
				return null;
			}
			string filepath = imageFilePath + String.Format("\\slide{0:0000}.jpg", index);
			Bitmap img = new Bitmap(filepath);
			return img;

		}

		public static void previous()
		{
			if (Ppt == null)
				return;

			if (1 < Ppt.SlideShowWindow.View.CurrentShowPosition)
			{
				Ppt.SlideShowWindow.View.Previous();
			}
		}

		public static void start()
		{
			if (Ppt == null)
				return;

			Microsoft.Office.Interop.PowerPoint.SlideShowSettings settings;
			settings = Ppt.SlideShowSettings;

			settings.StartingSlide = 1;
			settings.EndingSlide = slideIndex[slideIndex.Length - 1];

			settings.Run();
		}

		public static void restart()
		{
			if (FilePath == null || FilePath == "")
			{
				return;
			}
			close();
			LoadPowerPoint(FilePath);
			start();
		}

		public static void end()
		{
			close();
		}

		public static void close()
		{
			if (Ppt != null)
			{
				Ppt.Close();
				Ppt = null;
			}

			if (App != null)
			{
				App.Quit();
				App = null;
			}
		}
	}
}
