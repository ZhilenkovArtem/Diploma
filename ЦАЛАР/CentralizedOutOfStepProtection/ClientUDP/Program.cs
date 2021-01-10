using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ClientUDP
{
    /// <summary>
    /// Класс, реализующий функции КСВД, передающей СВИ серверу ЦАЛАР
    /// </summary>
    class Program
    {
        /// <summary>
        /// Порт сервера
        /// </summary>
        const int remotePort = 5000;
        /// <summary>
        /// Программный интерфейс для обеспечения обмена данными
        /// </summary>
        static Socket listeningSocket;

        /// <summary>
        /// Точка входа
        /// </summary>
        static void Main()
        {
            var frameList = TestData.GetTestData();

            try
            {
                listeningSocket = new Socket(AddressFamily.InterNetwork, 
                    SocketType.Dgram, ProtocolType.Udp);
                
                foreach (var frame in frameList)
                {
                    var binaryFormatter = new BinaryFormatter();
                    var memoryStream = new MemoryStream();
                    binaryFormatter.Serialize(memoryStream, frame);

                    byte[] data = memoryStream.ToArray();//Encoding.Unicode.GetBytes();
                    EndPoint remotePoint = new IPEndPoint(
                        IPAddress.Parse("127.0.0.1"), remotePort);
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

        /// <summary>
        /// Закрывает интерфейса обмена данными
        /// </summary>
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
