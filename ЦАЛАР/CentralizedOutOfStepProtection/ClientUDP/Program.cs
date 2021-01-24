using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using C37Library;
using System.Numerics;

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

                EndPoint remotePoint = new IPEndPoint(
                        IPAddress.Parse("127.0.0.1"), remotePort);

                byte[] packedHeader;
                byte[] packedConfig;
                byte[] packedData;
                byte[] packedCommand;

                foreach (var frame in frameList)
                {
                    packedHeader = frame.Header.Pack();  
                    packedConfig = frame.Config.Pack();  
                    packedData = frame.Data.Pack();      
                    packedCommand = frame.Command.Pack();

                    listeningSocket.SendTo(packedHeader, remotePoint);
                    listeningSocket.SendTo(packedConfig, remotePoint);
                    listeningSocket.SendTo(packedData, remotePoint);
                    listeningSocket.SendTo(packedCommand, remotePoint);

                    Thread.Sleep(20);
                }
                
                /*HeaderFrame header = new HeaderFrame("Simulated PMU");

                ConfigFrame config = new ConfigFrame();

                config.SOC = 21012021;
                config.FRACSEC = 20323030;
                config.TimeBase= 0;
                config.DataRate = 50;

                PMUStation pmu = new PMUStation("PMU 1st", 666, false, true, true, false);
                PMUStation pmu2 = new PMUStation("PMU 2nd", 667, false, false, true, false);

                pmu.PhasorAdd("FasorTensaoA", 915527, (int)PhasorUnitBit.Voltage);
                pmu.PhasorAdd("FasorCorrenteA", 45776, (int)PhasorUnitBit.Current);
                pmu.AnalogAdd("Ana_TensaoA", 100, 0);
                pmu.AnalogAdd("Ana_CorrenteA", 100, (int)AnalogUnitBit.PeakAnalogInput);

                pmu.FNOM = (ushort)FreqNom.FN50HZ;
                pmu.CFGCNT = 1;
                pmu.STAT = 2048;
                pmu.PhasorValues[0] = new Complex(500, 180);
                pmu.PhasorValues[1] = new Complex(330, -180);
                pmu.AnalogValues[0] = 34.15F;
                pmu.AnalogValues[1] = -100.10F;
                pmu.FREQ = 50.05F;
                pmu.DFREQ = 1.2F;

                pmu2.AnalogAdd("TensaoA", 1, (int)AnalogUnitBit.RMSAnalogInput);
                pmu2.AnalogAdd("CorrenteA", 1, (int)AnalogUnitBit.PeakAnalogInput);
                pmu2.FNOM = (ushort)FreqNom.FN50HZ;
                pmu2.CFGCNT = 1;
                pmu2.STAT = 2048;
                pmu2.AnalogValues[0] = 3.1415F;
                pmu2.AnalogValues[1] = 6.2830F;
                pmu2.FREQ = 2000;
                pmu2.DFREQ = 200;

                config.PMUStationAdd(pmu);
                config.PMUStationAdd(pmu2);
                
                DataFrame dataFrame = new DataFrame(config);
                dataFrame.IDCODE = 2;
                dataFrame.SOC = 21012021;
                dataFrame.FRACSEC = 0;

                CMDFrame cmdFrame = new CMDFrame();
                cmdFrame.CMD = 0x05;
                cmdFrame.SOC = 21012021;
                cmdFrame.IDCODE = 2;

                var packedHeader = header.Pack();
                var packedConfig = config.Pack();
                var packedData = dataFrame.Pack();
                var packedCommand = cmdFrame.Pack();
                
                EndPoint remotePoint = new IPEndPoint(
                    IPAddress.Parse("127.0.0.1"), remotePort);

                listeningSocket.SendTo(packedHeader, remotePoint);
                listeningSocket.SendTo(packedConfig, remotePoint);
                listeningSocket.SendTo(packedData, remotePoint);
                listeningSocket.SendTo(packedCommand, remotePoint);*/

                /*var binaryFormatter = new BinaryFormatter();
                    var memoryStream = new MemoryStream();
                    binaryFormatter.Serialize(memoryStream, frame);

                    byte[] data = memoryStream.ToArray();//Encoding.Unicode.GetBytes();
                    EndPoint remotePoint = new IPEndPoint(
                        IPAddress.Parse("127.0.0.1"), remotePort);
                    listeningSocket.SendTo(data, remotePoint);

                    Thread.Sleep(100);*/
                //}

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

        private static void SendFrame(object frame)
        {
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream();
            binaryFormatter.Serialize(memoryStream, frame);

            byte[] data = memoryStream.ToArray();//Encoding.Unicode.GetBytes();
            EndPoint remotePoint = new IPEndPoint(
                IPAddress.Parse("127.0.0.1"), remotePort);
            listeningSocket.SendTo(data, remotePoint);
        }
    }
}
