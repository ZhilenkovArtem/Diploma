using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.IO;
using SynchronizedVectorMeasurementProcessing;
using System.Diagnostics;
using System.Threading.Tasks;
using AsynchronyIdentification;
using ControlActionsSelection;
using C37Library;

namespace ServerUDP
{
    /// <summary>
    /// Класс, реализующий функции сервера централизованной АЛАР
    /// </summary>
    class Program
    {
        private const ushort A_SYNC_AA = 0xAA;
        private const ushort A_SYNC_DATA = 0x01;
        private const ushort A_SYNC_HDR = 0x11;
        private const ushort A_SYNC_CFG1 = 0x21;
        private const ushort A_SYNC_CFG2 = 0x31;
        private const ushort A_SYNC_CMD = 0x41;

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


        static HeaderFrame _header;// = new HeaderFrame("");
        static ConfigFrame _config;// = new ConfigFrame();
        static DataFrame _dataFrame;// = new DataFrame(_config);
        static CMDFrame _cmdFrame;// = new CMDFrame();

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

                    switch ((ushort)data[0])
                    {
                        case A_SYNC_HDR:
                            {
                                //Console.WriteLine("Получен кадр A_SYNC_HDR");
                                _header = new HeaderFrame("");
                                _header.Unpack(data);
                            };
                        break;
                        case A_SYNC_CFG2:
                            {
                                //Console.WriteLine("Получен кадр A_SYNC_CFG2");
                                _config = new ConfigFrame();
                                _config.Unpack(data);                                
                            };
                        break;
                        case A_SYNC_DATA:
                            {
                                //Console.WriteLine("Получен кадр A_SYNC_DATA");
                                _dataFrame = new DataFrame(_config);
                                _dataFrame.Unpack(data);
                                //Console.WriteLine($"В {_dataFrame.AssociateCurrentConfig.FRACSEC.ToString().Substring(6, 2)} " +
                                //    $"мс получен кадр A_SYNC_DATA");
                                var isFault = DetectDisturbance(
                                    _dataFrame.AssociateCurrentConfig);
                                /*foreach (var pmu in _dataFrame.AssociateCurrentConfig.PMUStationList)
                                {
                                    Console.WriteLine($"{pmu.IDCODE}\t{pmu.PhasorValues[0].Magnitude}\t" +
                                        $"{pmu.PhasorValues[0].Phase * 180 / Math.PI}\t{pmu.AnalogValues[0]}");
                                }*/

                                /*var strSOC = _dataFrame.AssociateCurrentConfig.SOC.ToString();
                                var strFRACSEC = _dataFrame.AssociateCurrentConfig.FRACSEC.ToString();
                                var time = new DateTime(
                                    int.Parse(strSOC.Substring(4, 4)),
                                    int.Parse(strSOC.Substring(2, 2)), 
                                    int.Parse(strSOC.Substring(0, 2)),
                                    int.Parse(strFRACSEC.Substring(0, 2)),
                                    int.Parse(strFRACSEC.Substring(2, 2)),
                                    int.Parse(strFRACSEC.Substring(4, 2)),
                                    int.Parse(strFRACSEC.Substring(6, 2) + "0"));
                                Console.WriteLine($"{time}.{time.Millisecond}");*/
                            }
                        break;
                        case A_SYNC_CMD:
                            {
                                //Console.WriteLine("Получен кадр A_SYNC_CMD");
                                _cmdFrame = new CMDFrame();
                                _cmdFrame.Unpack(data);
                            }
                        break;
                    }

                    //byte[] syncByte = new byte[1];
                    //syncByte[0] = data[3];
                    //var hf = BitConverter.ToInt32(data, 0);
                    //var sync = IPAddress.NetworkToHostOrder(hf);
                    //Console.WriteLine($"{string.Join(" ", data)}");

                    /*var memoryStream = new MemoryStream();
                    var binaryFormatter = new BinaryFormatter();
                    memoryStream.Write(data, 0, data.Length);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    var frame = (ConfigurationFrame)binaryFormatter.
                        Deserialize(memoryStream);*/
                    
                    /*Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();*/
                    /*var isFault = DetectDisturbance(frame);*/
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
        private static bool DetectDisturbance(ConfigFrame frame)
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

                if (_flag == true)
                {
                    ControlActionsSelect(_dataForTIIS, groups);
                    _flag = false;
                }

                _configDeviations = new ConfigDeviationsInWithin60ms();
                _dataForTIIS = new List<ConfigurationRedonePmuData>();
            }
            return isFault;
        }

        static bool _flag = true;
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
                Console.WriteLine($"Результат классификации: {answer} (асинхронный режим возникнет)");
                //Console.WriteLine($"Идентифицировано возникновение " +
                //    $"асинхронного режима\nСечение деления системы:");
                /*Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();*/
                var slice = 
                    SliceSelection.SelectSlice(groups.Item1, groups.Item2);
                /*stopwatch.Stop();
                TimeSpan timespan = stopwatch.Elapsed;
                string elapsedTime =
                    String.Format("{0:0000}", timespan.Milliseconds);*/
                Console.WriteLine("Сечение деления системы:");
                foreach (var lineSegment in slice)
                {
                    Console.WriteLine($"{lineSegment.StartNode}-" +
                        $"{lineSegment.EndNode}");
                }
                //Console.WriteLine(elapsedTime + " ms");
            }
            else
            {
                Console.WriteLine($"Результат классификации: {answer} (асинхронный режим не возникнет)");
            }

            /*Console.WriteLine($"Время работы {elapsedTime} ms\n" +
                $"Конец метода ControlActionsSelect");*/
        }

        /// <summary>
        /// Преобразует фрейм, полученный первым, в другой формат
        /// </summary>
        /// <param name="frame">фрейм</param>
        /// <returns></returns>
        private static ConfigurationRedonePmuData GetFirstConfig(
            ConfigFrame frame)
        {
            var pmuDataList = new List<RedonePmuData>();

            /*for (int i = 0; i < frame.Data.Count; i++)
            {
                var angleOfCurrentFrame = 
                    (float)(frame.Data[i].Voltage.Phase * 180 / Math.PI);

                pmuDataList.Add(new RedonePmuData(frame.Data[i].IDCode,
                    (float)frame.Data[i].Voltage.Magnitude,
                    angleOfCurrentFrame, frame.Data[i].Power));
            }
            return new ConfigurationRedonePmuData(pmuDataList, frame.Time);*/

            foreach (var pmu in frame.PMUStationList)
            {
                var idCode = pmu.IDCODE;
                var angle = 
                    (float)(pmu.PhasorValues[0].Phase * 180 / Math.PI);
                var magnitude = (float)pmu.PhasorValues[0].Magnitude;

                if (pmu.AnalogNumber != 0)
                {
                    var power = pmu.AnalogValues[0];

                    pmuDataList.Add(
                    new RedonePmuData(idCode, magnitude, angle, power));
                }
                else
                {
                    pmuDataList.Add(
                        new RedonePmuData(idCode, magnitude, angle));
                }
            }
            var time = GetDateTime(frame.SOC, frame.FRACSEC);

            return new ConfigurationRedonePmuData(pmuDataList, time);
        }

        /// <summary>
        /// Исправляет фрейм в другой формат
        /// </summary>
        /// <param name="currentFrame">текущий фрейм</param>
        /// <param name="previousConfig">предыдущий фрейм</param>
        /// <returns></returns>
        private static ConfigurationRedonePmuData GetActualConfig(
            ConfigFrame currentFrame, 
            ConfigurationRedonePmuData previousConfig)
        {
            var pmuDataList = new List<RedonePmuData>();

            for (int i = 0; i < currentFrame.PMUStationList.Count; i++)
            {
                var pmu = currentFrame.PMUStationList[i];

                var idCodeOfCurrentFrame = pmu.IDCODE;
                var angleOfCurrentFrame = 
                    (float)(pmu.PhasorValues[0].Phase * 180 / Math.PI);
                var magnitudeOfCurrentFrame = 
                    (float)pmu.PhasorValues[0].Magnitude;

                var angleOfPreviousFrame = previousConfig.Data[i].VoltagePhase;
                if ((angleOfCurrentFrame - angleOfPreviousFrame) < -250)
                {
                    angleOfCurrentFrame = angleOfCurrentFrame + 360;
                }
                else if ((angleOfCurrentFrame - angleOfPreviousFrame) > 250)
                {
                    angleOfCurrentFrame = angleOfCurrentFrame - 360;
                }

                if (pmu.AnalogNumber != 0)
                {
                    pmuDataList.Add(
                        new RedonePmuData(idCodeOfCurrentFrame,
                        magnitudeOfCurrentFrame,
                        angleOfCurrentFrame, pmu.AnalogValues[0]));
                }
                else
                {
                    pmuDataList.Add(
                        new RedonePmuData(idCodeOfCurrentFrame, 
                        magnitudeOfCurrentFrame, angleOfCurrentFrame));
                }
            }
            var time = GetDateTime(currentFrame.SOC, currentFrame.FRACSEC);

            return new ConfigurationRedonePmuData(
                pmuDataList, time);
        }

        private static DateTime GetDateTime(uint soc, uint fracsec)
        {
            var strSOC = soc.ToString();
            var strFRACSEC = fracsec.ToString();
            return new DateTime(
                int.Parse(strSOC.Substring(4, 4)),
                int.Parse(strSOC.Substring(2, 2)),
                int.Parse(strSOC.Substring(0, 2)),
                int.Parse(strFRACSEC.Substring(0, 2)),
                int.Parse(strFRACSEC.Substring(2, 2)),
                int.Parse(strFRACSEC.Substring(4, 2)),
                int.Parse(strFRACSEC.Substring(6, 2) + "0"));
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
