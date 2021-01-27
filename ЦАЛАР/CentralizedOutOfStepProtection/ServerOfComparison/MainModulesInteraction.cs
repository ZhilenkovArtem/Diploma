using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using ControlActionsSelection;

namespace ServerOfComparison
{
    class MainModulesInteraction
    {
        /// <summary>
        /// Index of "Asynchrony Identification"
        /// </summary>
        private const string AIREQUEST = "classifier";

        /// <summary>
        /// Index of "Control Actions Selection"
        /// </summary>
        private const string CASREQUEST = "cutsets";

        /// <summary>
        /// Номер режима
        /// </summary>
        private static int _modeNumber;

        /// <summary>
        /// Порт для приема входящих запросов
        /// </summary>
        private static int _port = 8005;

        private static void Main()
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("192.168.1.3"), _port);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listenSocket.Bind(ipPoint);
                listenSocket.Listen(100);

                //Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] data = new byte[256];

                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);

                    var strBuilder = builder.ToString();
                    
                    switch (strBuilder)
                    {
                        case AIREQUEST:
                            {
                                Console.WriteLine("Получен запрос AI_REQUEST от подсистемы Идентификации АР");
                                string path = @"C:\Users\Артем Жиленков\Desktop\Магистрский IT\Diploma\ЦАЛАР\classifier.CSV";
                                data = Encoding.Unicode.GetBytes(path);
                                handler.Send(data);
                            }
                        break;
                        case CASREQUEST:
                            {
                                Console.WriteLine("Получен запрос CAS_REQUEST от подсистемы Выбора УВ");
                                var sliceList = TestData.GetTestData();
                                var binaryFormatter = new BinaryFormatter();
                                var memoryStream = new MemoryStream();
                                binaryFormatter.Serialize(memoryStream, sliceList);
                                byte[] sliceData = memoryStream.ToArray();
                                handler.Send(sliceData);
                            }
                        break;
                        default:
                            {
                                _modeNumber = int.Parse(strBuilder);
                                Console.WriteLine($"Значение номера режима _modeNumber обновлено. _modeNumber = {_modeNumber}");
                            }
                        break;
                    }
                    
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    /*
    /// <summary>
    /// Класс, описывающий структуру среза данных
    /// </summary>
    public class SplitSlice
    {
        public List<float> FirstGroup { get; set; }

        public List<float> SecondGroup { get; set; }

        public List<LineSegmentForSplitting> Slice { get; set; }
    }

    /// <summary>
    /// Структура данных для сегмента 
    /// </summary>
    public struct LineSegmentForSplitting
    {
        public int StartNode { get; set; }

        public int EndNode { get; set; }

        public LineSegmentForSplitting(int startNode, int endNode)
        {
            this.StartNode = startNode;
            this.EndNode = endNode;
        }
    }*/
}
