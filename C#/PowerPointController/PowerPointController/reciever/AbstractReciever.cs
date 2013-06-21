using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
	}
}
