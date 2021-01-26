using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ControlActionsSelection
{
    [Serializable]
    public class SliceList
    {
        public List<SplitSlice> SplitSlices { get; set; }

        public SliceList (List<SplitSlice> splitSlices)
        {
            this.SplitSlices = splitSlices;
        }
    }

    [Serializable]
    /// <summary>
    /// Класс, описывающий структуру среза данных
    /// </summary>
    /// 
    public class SplitSlice
    {
        public List<float> FirstGroup { get; set; }

        public List<float> SecondGroup { get; set; }

        public List<LineSegmentForSplitting> Slice { get; set; }
    }

    [Serializable]
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
    }

    /// <summary>
    /// Выбор сечения
    /// </summary>
    public class SliceSelection
    {
        public static List<LineSegmentForSplitting> SelectSlice(
            List<float> group1, List<float> group2)
        {
            var sliceList = GetCutSets(); //Console.WriteLine("Сечения-кандидаты деления системы получены");//TestData.GetTestData();
            bool flag;

            foreach (var slice in sliceList)
            {
                flag = true;
                foreach (var gen in group1)
                {
                    if (!slice.FirstGroup.Contains(gen))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag == true)
                {
                    foreach (var gen in group2)
                    {
                        if (!slice.SecondGroup.Contains(gen))
                        {
                            flag = false;
                            break;
                        }
                    }
                }
                if (flag == true)
                {
                    return slice.Slice;
                }
            }
            return new List<LineSegmentForSplitting>();
        }

        private static List<SplitSlice> GetCutSets()
        {
            EndPoint ipPoint = new IPEndPoint(IPAddress.Parse("192.168.1.3"), 8005);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(ipPoint);
            string message = "cutsets";
            byte[] data = Encoding.Unicode.GetBytes(message);
            socket.Send(data);

            // получаем ответ
            data = new byte[12288];
            //StringBuilder builder = new StringBuilder();
            int bytes = 0;
            //bytes = socket.ReceiveFrom(data, ref ipPoint);
            //do
            //{
            bytes = socket.Receive(data, data.Length, 0);
            /*    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (socket.Available > 0);
            Console.WriteLine("ответ сервера: " + builder.ToString());*/
            var memoryStream = new MemoryStream();
            var binaryFormatter = new BinaryFormatter();
            memoryStream.Write(data, 0, data.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);
            var sliceList = (SliceList)binaryFormatter.Deserialize(memoryStream);

            // закрываем сокет
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

            return sliceList.SplitSlices;
        }
    }
}
