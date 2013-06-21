using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace PowerPointController
{
	public partial class PowerPointController : Form
	{
		public PowerPointController()
		{
			InitializeComponent();
		}

		private void openPowerPointFileButton_Click(object sender, EventArgs e)
		{
			if (openPowerPointDialog.ShowDialog() == DialogResult.OK)
			{
				powerPointFilePathTextBox.Text = openPowerPointDialog.FileName;
			}
		}

		private void startButton_Click(object sender, EventArgs e)
		{
			controller.PowerPointController.LoadPowerPoint(powerPointFilePathTextBox.Text);
			//controller.PowerPointController.start();
			//受信開始の処理
			reciever.SocketReciever rec = new reciever.SocketReciever();
			rec.ConnectionOpen();
		}

		private void PowerPointController_FormClosing(object sender, FormClosingEventArgs e)
		{
			controller.PowerPointController.close();
		}

		private void nextButton_Click(object sender, EventArgs e)
		{
			controller.PowerPointController.next();
		}

		private void previousButton_Click(object sender, EventArgs e)
		{
			controller.PowerPointController.previous();
		}

		private void restartButton_Click(object sender, EventArgs e)
		{
			controller.PowerPointController.restart();
		}

		private void endButton_Click(object sender, EventArgs e)
		{
			controller.PowerPointController.end();
		}

		private void IPAddressButton_Click(object sender, EventArgs e)
		{
 			string hostname = Dns.GetHostName();
			IPAddress[] adrList = Dns.GetHostAddresses(hostname);
			foreach (IPAddress address in adrList)
			{
				if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
				{
					IPAddressTextBox.Text = address.ToString();
					break;
				}
			}

		}
	}
}
