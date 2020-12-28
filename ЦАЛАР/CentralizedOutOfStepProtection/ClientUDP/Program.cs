using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using SynchronizedVectorMeasurementProcessing;

namespace ClientUDP
{
    class Program
    {
        const int remotePort = 5000;
        static Socket listeningSocket;

        static void Main(string[] args)
        {
            var frameList = TestData.GetTestData();
            //int[] massive = new int[] { 1, 2, 3 };
            //Console.WriteLine("Для отправки сообщений введите сообщение и нажмите Enter");

            try
            {
                listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                
                foreach (var frame in frameList) //for (int i = 0; i < massive.Length; i++)
                {
                    var binaryFormatter = new BinaryFormatter();
                    var memoryStream = new MemoryStream();
                    binaryFormatter.Serialize(memoryStream, /*massive[i]*/frame);

                    byte[] data = memoryStream.ToArray();//Encoding.Unicode.GetBytes();
                    EndPoint remotePoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), remotePort);
                    listeningSocket.SendTo(data, remotePoint);

                    Thread.Sleep(100);
                }

                /*while (true)
                {
                    string message = Console.ReadLine();

                    byte[] data = Encoding.Unicode.GetBytes(message);
                    EndPoint remotePoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), remotePort);
                    listeningSocket.SendTo(data, remotePoint);
                }*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Close();
            }
            Console.ReadKey();
        }
                        
        private static void Close()
        {
            if (listeningSocket != null)
            {
                listeningSocket.Shutdown(SocketShutdown.Both);
                listeningSocket.Close();
                listeningSocket = null;
            }
        }
    }
}
