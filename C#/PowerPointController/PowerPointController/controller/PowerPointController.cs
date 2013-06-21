using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerPointController.controller
{
	class PowerPointController
	{
		private static String FilePath = null;
		private static Microsoft.Office.Interop.PowerPoint.Application App = null;
		private static Microsoft.Office.Interop.PowerPoint.Presentation Ppt = null;
		private static int[] slideIndex;

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
