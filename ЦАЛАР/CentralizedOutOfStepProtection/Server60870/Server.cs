using System;
using lib60870;
using UsingOfPayload;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace testserver
{
    class Server104
	{
        /// <summary>
        /// ����� ����� (common address)
        /// </summary>
        //static byte ca = 160;

        /// <summary>
        /// IP-����� �������
        /// </summary>
        static string _ipAdress = "192.168.1.3";

        /// <summary>
        /// ����� ����� �������
        /// </summary>
        static int _ServerPort = 2404;

        static List<Telemetry> _telemetries = new List<Telemetry>();

        /// <summary>
        /// ���� � ������ � ���������� �������
        /// </summary>
        private const string PATH = @"C:\Users\����� ��������\Desktop\" +
            @"����������� IT\Diploma\�����\Output\DataDB\";

        /// <summary>
        /// ����� �����
        /// </summary>
        public static void Main()
        {
            Server server = new Server();
            server.DebugOutput = false;
            server.MaxQueueSize = 10;
            server.ServerMode = ServerMode.SINGLE_REDUNDANCY_GROUP;
            server.SetASDUHandler(asduHandler, null);

            server.SetLocalAddress(_ipAdress);
            server.SetLocalPort(_ServerPort);
            server.Start();

            /*Console.WriteLine("������ �������. IP " + ipAdress + " ����: "+ServerPort);
            Console.WriteLine("����� ����� ���� = " + ca);

            bool DataReady;
            while (true)
            {
                int type = 0;
                Console.WriteLine("�������� ��� ������������ ������");
                Console.WriteLine("1 - �������������� ����������, M_SP_NA_1");
                Console.WriteLine("2 - ������������� ����������, M_BO_NA_1");
                Console.WriteLine("3 - � ��������� ������, M_ME_NC_1");

                type = int.Parse(Console.ReadLine());

                Console.WriteLine("������� IOA");
                int oa = int.Parse(Console.ReadLine());

                Console.WriteLine("������� ������");
                string sdata = Console.ReadLine();

                sdata = sdata.Replace('.', ',');
                float value = float.Parse(sdata);
                                
                ASDU newAsdu = new ASDU(server.GetConnectionParameters(), CauseOfTransmission.PERIODIC, false, false, 1, ca, false);
                InformationObject io;

                DataReady = true;
                switch (type)
                {
                    case 1:
                        bool bValue = (value >= 1.0f);
                        io = new SinglePointInformation(oa, bValue, new QualityDescriptor());
                        newAsdu.AddInformationObject(io);
                        break;
                    case 2:
                        io = new Bitstring32(oa, Convert.ToUInt32(value), new QualityDescriptor());
                        newAsdu.AddInformationObject(io);
                        break;
                    case 3:
                        io = new MeasuredValueShort(oa, value, new QualityDescriptor());
                        newAsdu.AddInformationObject(io);
                        break;

                    default:
                        DataReady = false;
                        break;
                }
                if (DataReady)
                {
                    server.EnqueueASDU(newAsdu);

                    Console.WriteLine("�������� ����������� � ������� �� ��������" + Environment.NewLine);
                }
                else
                {
                    Console.WriteLine("�������� ��� ������");
                }
            }*/
        }

        /// <summary>
        /// ���������� ASDU - Application specific data unit
        /// </summary>
        /// <param name="parameter">��������</param>
        /// <param name="connection">������ �����������</param>
        /// <param name="asdu">ASDU</param>
        /// <returns>������ �������� � ���������� ��� ������</returns>
        private static bool asduHandler(object parameter,
            ServerConnection connection, ASDU asdu)
        {
            //Console.WriteLine("�������� ����"/*: Ca=" + asdu.Ca */+
            //    ", TypeId=" + asdu.TypeId);

            if (asdu.TypeId == lib60870.TypeID.M_SP_NA_1)
            {
                for (int i = 0; i < asdu.NumberOfElements; i++)
                {
                    var val = (SinglePointInformation)asdu.GetElement(i);
                    //Console.WriteLine("  IOA: " + val.ObjectAddress + 
                    //    " SP value: " + val.Value);
                    _telemetries.Add(new Telemetry(
                        val.ObjectAddress, 
                        val.Value ? 1 : 0,
                        UsingOfPayload.TypeID.SinglePointInformation));
                }
            }
            else if (asdu.TypeId == lib60870.TypeID.M_ME_NC_1)
            {
                for (int i = 0; i < asdu.NumberOfElements; i++)
                {
                    var val = (MeasuredValueShort)asdu.GetElement(i);
                    //Console.WriteLine("  IOA: " + val.ObjectAddress + 
                    //    " float value: " + val.Value);
                    _telemetries.Add(new Telemetry(
                        val.ObjectAddress,
                        val.Value,
                        UsingOfPayload.TypeID.MeasuredValueShort));
                }
            }
            else if (asdu.TypeId == lib60870.TypeID.M_BO_NA_1)
            {
                for (int i = 0; i < asdu.NumberOfElements; i++)
                {
                    var val = (Bitstring32)asdu.GetElement(i);
                    //Console.WriteLine("  IOA: " + val.ObjectAddress + 
                    //    " int value: " + val.Value);
                    if (val.Value == 0)
                    {
                        DefineMode();
                    }
                }
            }
            else
            {
                Console.WriteLine("����������� ��� ���������!");
                return false;
            }
            return true;
        }

        private static async void DefineMode()
        {
            Console.WriteLine("������ ������ DefineMode");
            var mode = await Task.Run(() => Comparison.GetMode(PATH, _telemetries));
            _telemetries = new List<Telemetry>();
            SendModeNumber(mode.Substring(7, 2));
            Console.WriteLine($"Mode {mode.Substring(7, 2)}\n����� ������ DefineMode");
        }

        private static void SendModeNumber(string modeNumber)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("192.168.1.3"), 8005);

            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // ������������ � ���������� �����
            socket.Connect(ipPoint);
            string message = modeNumber;
            byte[] data = Encoding.Unicode.GetBytes(message);
            socket.Send(data);

            /*/ �������� �����
            data = new byte[256]; // ����� ��� ������
            StringBuilder builder = new StringBuilder();
            int bytes = 0; // ���������� ���������� ����

            do
            {
                bytes = socket.Receive(data, data.Length, 0);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (socket.Available > 0);
            Console.WriteLine("����� �������: " + builder.ToString());*/

            // ��������� �����
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}       
