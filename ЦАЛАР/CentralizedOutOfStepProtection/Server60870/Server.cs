using System;
using lib60870;
using UsingOfPayload;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace testserver
{
    class Server104
	{
        /// <summary>
        /// Общий адрес (common address)
        /// </summary>
        //static byte ca = 160;

        /// <summary>
        /// IP-адрес сервера
        /// </summary>
        static string _ipAdress = "192.168.1.3";

        /// <summary>
        /// Номер порта сервера
        /// </summary>
        static int _ServerPort = 2404;

        static List<Telemetry> _telemetries = new List<Telemetry>();

        /// <summary>
        /// Путь к файлам с расчетными данными
        /// </summary>
        private const string PATH = @"C:\Users\Артем Жиленков\Desktop\" +
            @"Магистрский IT\Diploma\ЦАЛАР\Output\DataDB\";

        /// <summary>
        /// Точка входа
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

            /*Console.WriteLine("Сервер запущен. IP " + ipAdress + " Порт: "+ServerPort);
            Console.WriteLine("Общий адрес АСДУ = " + ca);

            bool DataReady;
            while (true)
            {
                int type = 0;
                Console.WriteLine("Выберете тип передаваемых данных");
                Console.WriteLine("1 - Одноэлементная информация, M_SP_NA_1");
                Console.WriteLine("2 - Целочисленная информация, M_BO_NA_1");
                Console.WriteLine("3 - С плавающей точкой, M_ME_NC_1");

                type = int.Parse(Console.ReadLine());

                Console.WriteLine("Введите IOA");
                int oa = int.Parse(Console.ReadLine());

                Console.WriteLine("Введите данные");
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

                    Console.WriteLine("Значение поставленно в очередь на отправку" + Environment.NewLine);
                }
                else
                {
                    Console.WriteLine("Неверный тип данных");
                }
            }*/
        }

        /// <summary>
        /// Обработчик ASDU - Application specific data unit
        /// </summary>
        /// <param name="parameter">параметр</param>
        /// <param name="connection">сервер подключения</param>
        /// <param name="asdu">ASDU</param>
        /// <returns>Булево значение о выполнении без ошибок</returns>
        private static bool asduHandler(object parameter,
            ServerConnection connection, ASDU asdu)
        {
            //Console.WriteLine("Получено асду"/*: Ca=" + asdu.Ca */+
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
                Console.WriteLine("Неизвестный тип сообщения!");
                return false;
            }
            return true;
        }

        private static async void DefineMode()
        {
            Console.WriteLine("Начало метода DefineMode");
            var mode = await Task.Run(() => Comparison.GetMode(PATH, _telemetries));
            _telemetries = new List<Telemetry>();
            Console.WriteLine($"Mode {mode}\nКонец метода DefineMode");
        }
    }
}       
