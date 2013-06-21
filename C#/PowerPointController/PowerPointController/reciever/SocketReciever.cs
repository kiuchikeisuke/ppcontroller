using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

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

							switch (recievedStr)
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
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
						ConnectionClose();
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
