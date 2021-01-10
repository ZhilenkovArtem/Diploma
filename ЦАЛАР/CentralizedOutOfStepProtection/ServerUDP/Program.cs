using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using SynchronizedVectorMeasurementProcessing;
using System.Diagnostics;
using System.Threading.Tasks;
using AsynchronyIdentification;
using ControlActionsSelection;

namespace ServerUDP
{
    /// <summary>
    /// Класс, реализующий функции сервера централизованной АЛАР
    /// </summary>
    class Program
    {
        /// <summary>
        /// Порт сервера
        /// </summary>
        const int _localPort = 5000;
        /// <summary>
        /// Программный интерфейс для обеспечения обмена данными
        /// </summary>
        static Socket _listeningSocket;
        /// <summary>
        /// Блок срезов данных СВИ за промежуток времени
        /// </summary>
        static ConfigDeviationsInWithin60ms _configDeviations = 
            new ConfigDeviationsInWithin60ms();
        /// <summary>
        /// Данные для передаче в подсистему Идентификации
        /// возникновения асинхронного режима
        /// TIIS - TransientInstabilityIdentificationSubsystem
        /// </summary>
        static List<ConfigurationRedonePmuData> _dataForTIIS = 
            new List<ConfigurationRedonePmuData>();
        /// <summary>
        /// Срез данных за предыдущий промежуток времени
        /// </summary>
        static ConfigurationRedonePmuData _previousRedoneConfig = 
            new ConfigurationRedonePmuData();

        /// <summary>
        /// Точка входа
        /// </summary>
        static void Main()
        {
            try
            {
                _listeningSocket = new Socket(AddressFamily.InterNetwork, 
                    SocketType.Dgram, ProtocolType.Udp);
                IPEndPoint localIP = 
                    new IPEndPoint(IPAddress.Parse("127.0.0.1"), _localPort);
                _listeningSocket.Bind(localIP);

                while (true)
                {
                    //StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    byte[] data = new byte[4096];

                    EndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0);

                    bytes = _listeningSocket.ReceiveFrom(data, ref remoteIp);
                    var memoryStream = new MemoryStream();
                    var binaryFormatter = new BinaryFormatter();
                    memoryStream.Write(data, 0, data.Length);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var frame = (ConfigurationFrame)binaryFormatter.
                        Deserialize(memoryStream);
                    
                    /*Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();*/
                    var isFault = DetectDisturbance(frame);
                    /*stopwatch.Stop();
                    TimeSpan timespan = stopwatch.Elapsed;
                    string elapsedTime = String.Format(
                        "{0:0000}", timespan.Milliseconds);*/

                    /*do
                    {
                        bytes = listeningSocket.ReceiveFrom(data, ref remoteIp);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (listeningSocket.Available > 0);*/

                    IPEndPoint remoteFullIp = remoteIp as IPEndPoint;

                    //Console.WriteLine($"{remoteFullIp.Address}:" +
                    //    $"{remoteFullIp.Port} - {frame.Time}");
                }
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
            if (_listeningSocket != null)
            {
                _listeningSocket.Shutdown(SocketShutdown.Both);
                _listeningSocket.Close();
                _listeningSocket = null;
            }
        }

        /// <summary>
        /// Идентифицирует возмущение в энергосистеме
        /// </summary>
        /// <param name="frame">фрейм данных в момент времени</param>
        /// <returns>Значение, было ли возмущение</returns>
        private static bool DetectDisturbance(ConfigurationFrame frame)
        {
            var redoneConfig = new ConfigurationRedonePmuData();
            if (_configDeviations.DeviationsInWithin60ms.Count != 0)
            {
                redoneConfig = GetActualConfig(frame, _previousRedoneConfig);
            }
            else
            {
                redoneConfig = GetFirstConfig(frame);                
            }
            _previousRedoneConfig = new ConfigurationRedonePmuData(
                redoneConfig.Data, redoneConfig.Time);

            var listWithoutGenerators = 
                GetFrameWithoutGenerators(redoneConfig);
            _dataForTIIS = 
                AddNewFrameInList(_dataForTIIS, listWithoutGenerators);

            var listWithoutNodes = GetFrameWithoutNodes(redoneConfig);
            var devlist = new DeviationsList(listWithoutNodes);
            _configDeviations = 
                _configDeviations.AddNewDevList(_configDeviations, devlist);
            var isFault = DetectionOfFault.DetectFault(devlist.DevList);
            
            var countDev = _configDeviations.DeviationsInWithin60ms.Count;
            if (isFault && countDev ==
                ConfigDeviationsInWithin60ms.DEVIATIONSLISTSCOUNT)
            {
                var groups = FindingGroupsCoherentGenerators.
                    GetGroupsCoherentGenerators(devlist.DevList);
                ControlActionsSelect(_dataForTIIS, groups);

                _configDeviations = new ConfigDeviationsInWithin60ms();
                _dataForTIIS = new List<ConfigurationRedonePmuData>();
            }
            return isFault;
        }

        /// <summary>
        /// Идентификация возникновения АР и выбор сечения ДС
        /// </summary>
        /// <param name="data">данные для идентификации АР</param>
        /// <param name="groups">группы когерентных генераторов</param>
        private static async void ControlActionsSelect(
            List<ConfigurationRedonePmuData> data, 
            Tuple<List<float>, List<float>> groups)
        {
            //Console.WriteLine("Начало метода ControlActionsSelect");

            /*Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();*/
            var answer = await Task.Run(() => InteractionWithR.GetAnswer(data));
            /*stopwatch.Stop();
            TimeSpan timespan = stopwatch.Elapsed;
            string elapsedTime = 
                String.Format("{0:0000}", timespan.Milliseconds);*/

            if (answer == "1 " || answer == "2 ")
            {
                //Console.WriteLine($"Идентифицировано возникновение " +
                //    $"асинхронного режима\nСечение деления системы:");
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                var slice = 
                    SliceSelection.SelectSlice(groups.Item1, groups.Item2);
                stopwatch.Stop();
                TimeSpan timespan = stopwatch.Elapsed;
                string elapsedTime =
                    String.Format("{0:0000}", timespan.Milliseconds);
                /*foreach (var lineSegment in slice)
                {
                    Console.WriteLine($"{lineSegment.StartNode}-" +
                        $"{lineSegment.EndNode}");
                }*/
                Console.WriteLine(elapsedTime + " ms");
            }

            /*Console.WriteLine($"Метод вернул {answer}\n" +
                $"Время работы {elapsedTime} ms\n" +
                $"Конец метода ControlActionsSelect");*/
        }

        /// <summary>
        /// Преобразует фрейм, полученный первым, в другой формат
        /// </summary>
        /// <param name="frame">фрейм</param>
        /// <returns></returns>
        private static ConfigurationRedonePmuData GetFirstConfig(
            ConfigurationFrame frame)
        {
            var pmuDataList = new List<RedonePmuData>();

            for (int i = 0; i < frame.Data.Count; i++)
            {
                var angleOfCurrentFrame = 
                    (float)(frame.Data[i].Voltage.Phase * 180 / Math.PI);

                pmuDataList.Add(new RedonePmuData(frame.Data[i].IDCode,
                    (float)frame.Data[i].Voltage.Magnitude,
                    angleOfCurrentFrame, frame.Data[i].Power));
            }
            return new ConfigurationRedonePmuData(pmuDataList, frame.Time);
        }

        /// <summary>
        /// Исправляет фрейм в другой формат
        /// </summary>
        /// <param name="currentFrame">текущий фрейм</param>
        /// <param name="previousConfig">предыдущий фрейм</param>
        /// <returns></returns>
        private static ConfigurationRedonePmuData GetActualConfig(
            ConfigurationFrame currentFrame, 
            ConfigurationRedonePmuData previousConfig)
        {
            var pmuDataList = new List<RedonePmuData>();

            for (int i = 0; i < currentFrame.Data.Count; i++)
            {
                var angleOfCurrentFrame = 
                    (float)(currentFrame.Data[i].Voltage.Phase * 180 / Math.PI);
                var angleOfPreviousFrame = previousConfig.Data[i].VoltagePhase;

                if ((angleOfCurrentFrame - angleOfPreviousFrame) < -250)
                {
                    angleOfCurrentFrame = angleOfCurrentFrame + 360;
                }
                else if ((angleOfCurrentFrame - angleOfPreviousFrame) > 250)
                {
                    angleOfCurrentFrame = angleOfCurrentFrame - 360;
                }

                pmuDataList.Add(new RedonePmuData(currentFrame.Data[i].IDCode,
                    (float)currentFrame.Data[i].Voltage.Magnitude, 
                    angleOfCurrentFrame, currentFrame.Data[i].Power));
            }
            return new ConfigurationRedonePmuData(
                pmuDataList, currentFrame.Time);
        }

        /// <summary>
        /// Получает часть фрейма без данных узлов электрической сети
        /// </summary>
        /// <param name="frame">фрейм</param>
        /// <returns>фрейм без узлов</returns>
        private static ConfigurationRedonePmuData GetFrameWithoutNodes(
            ConfigurationRedonePmuData frame)
        {
            var newData = 
                frame.Data.Where(pmu => !float.IsNaN(pmu.Power)).ToList();
            return new ConfigurationRedonePmuData(newData, frame.Time);
        }

        /// <summary>
        /// Получает часть фрейма без данных генераторов
        /// </summary>
        /// <param name="frame">фрейм</param>
        /// <returns>фрейм без генераторов</returns>
        private static ConfigurationRedonePmuData GetFrameWithoutGenerators(
            ConfigurationRedonePmuData frame)
        {
            var newData = 
                frame.Data.Where(pmu => float.IsNaN(pmu.Power)).ToList();
            return new ConfigurationRedonePmuData(newData, frame.Time);
        }

        /// <summary>
        /// Добавляет новый фрейм в лист фреймов
        /// </summary>
        /// <param name="frameList">лист фреймов</param>
        /// <param name="frame">новый фрейм</param>
        /// <returns>обновленный лист фреймов</returns>
        private static List<ConfigurationRedonePmuData> AddNewFrameInList(
            List<ConfigurationRedonePmuData> frameList,
            ConfigurationRedonePmuData frame)
        {
            var frameListsCount = frameList.Count;
            var maxCount = ConfigDeviationsInWithin60ms.DEVIATIONSLISTSCOUNT;
            if (frameListsCount == maxCount)
            {
                for (int i = 0; i < maxCount - 1; i++)
                {
                    frameList[i] = frameList[i + 1];
                }
                frameList[maxCount - 1] = frame;
            }
            else if (0 <= frameListsCount &&
                frameListsCount < maxCount)
            {
                frameList.Add(frame);
            }
            else
            {
                throw new Exception($"Число frameList в списке не лежит в " +
                    $"диапазоне от 0 до {maxCount}");
            }
            return frameList;
        }
    }
}
