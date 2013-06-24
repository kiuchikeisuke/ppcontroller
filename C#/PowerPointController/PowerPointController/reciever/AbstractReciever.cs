using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PowerPointController.reciever
{
	public abstract class AbstractReciever
	{
		public abstract void ConnectionOpen();

		public abstract void ConnectionClose();

		protected void start()
		{
			controller.PowerPointController.start();
		}

		protected void reStart()
		{
			controller.PowerPointController.restart();
		}

		protected void next()
		{
			controller.PowerPointController.next();
		}
		protected void previous()
		{
			controller.PowerPointController.previous();
		}

		protected void end()
		{
			controller.PowerPointController.end();
		}

		protected int getSlideLength()
		{
			return controller.PowerPointController.getSlideLength();
		}

		protected void jump(int index)
		{
			controller.PowerPointController.jump(index);
		}

		protected Bitmap getSlideImage(int index)
		{
			return controller.PowerPointController.getSlideImageStream(index);
		}
	}
}
