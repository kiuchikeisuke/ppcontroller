using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Drawing;

namespace PowerPointController.reciever
{
	 public class SocketReciever : AbstractReciever
	{
		 TcpClient server = null;
		 TcpListener listener = null;
		 private const int PORT = 12344;
		 Encoding encUTF8 = Encoding.UTF8;

		 Thread threadServer = null;

		public override void ConnectionOpen()
		{
			try
			{
				threadServer = new Thread(new ThreadStart(ServerListen));
				threadServer.Start();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				listener.Stop();
			}
		}

		private void ServerListen()
		{
			bool quitFlag = false;
			while (quitFlag == false)
			{
				if (listener == null)
				{
					listener = new TcpListener(IPAddress.Any, PORT);
				}
				listener.Start();

				Console.WriteLine("start server");


				server = listener.AcceptTcpClient();

				NetworkStream stream = null;
				Byte[] bytes = new Byte[1024];
				while (true)
				{
					try
					{
						stream = server.GetStream();
						int intCount = stream.Read(bytes, 0, bytes.Length);

						if (intCount != 0)
						{
							Byte[] getByte = new byte[intCount];
							for (int i = 0; i < intCount; i++)
							{
								getByte[i] = bytes[i];
							}
							string recievedStr = encUTF8.GetString(getByte);
							recievedStr = recievedStr.Replace("\n", "");
							string[] recievedStrs = recievedStr.Split(' ');
							string cmd = recievedStrs[0];
							int index = -1;
							if (recievedStrs.Length == 2)
							{
								index = Int16.Parse(recievedStrs[1]);
							}
							switch (cmd)
							{
								case "ppc_start":
									start();
									break;
								case "ppc_restart":
									reStart();
									break;
								case "ppc_next":
									next();
									break;
								case "ppc_previous":
									previous();
									break;
								case "ppc_end":
									end();
									break;
								case "ppc_quit":
									end();
									ConnectionClose();
									quitFlag = true;
									return;
								case "ppc_jump":
									jump(index);
									break;
								case "ppc_load_slide":
									Bitmap img = getSlideImage(index);
									ImageConverter imgConv = new ImageConverter();
									byte[] imgByte = (byte[])imgConv.ConvertTo(img, typeof(byte[]));
									stream.Write(imgByte, 0, imgByte.Length);
									stream.Flush();
									break;
								case "ppc_load_count":
									int count = getSlideLength();
									byte[] tmp = encUTF8.GetBytes(count.ToString());
									stream.Write(tmp, 0, tmp.Length);
									stream.Flush();
									break;
								default:
									Console.WriteLine(recievedStr);
									break;
							}
						}
						else
						{
							break;
						}
					}
					catch (ThreadAbortException e)
					{
						Console.WriteLine(e.ToString());
						ConnectionClose();
						break;
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
						ConnectionClose();
						break;
					}
				}

			}
		}

		public override void ConnectionClose()
		{
			if (listener != null)
			{
				listener.Stop();
				listener = null;
			}
			if (server != null && server.Connected)
			{
				server.Close();
			}
		}
	}
}
