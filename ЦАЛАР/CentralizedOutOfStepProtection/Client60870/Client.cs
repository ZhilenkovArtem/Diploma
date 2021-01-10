using System;
using lib60870;
using UsingOfPayload;
using System.Linq;
//using System.Threading;

namespace testclient
{
	class Client
	{
        /// <summary>
        /// Общий адрес (common address)
        /// </summary>
        private static byte ca = 160;

        /// <summary>
        /// IP-адрес сервера
        /// </summary>
        private static string ipAdress = "192.168.1.3";

        /// <summary>
        /// Номер порта сервера
        /// </summary>
        private static string port = "2404";

        /// <summary>
        /// Путь к файлу с телеметрией
        /// </summary>
        private const string TELEMETRIESPATH = @"C:\Users\Артем Жиленков\" + 
            @"Desktop\Магистрский IT\Diploma\ЦАЛАР\Output\Telemetries\" + 
            @"telemetries24.dat";

        /// <summary>
        /// Точка входа
        /// </summary>
        public static void Main ()
		{
            Console.WriteLine("Соединение с сервером " + ipAdress + ". Порт " + port);
            Connection con = new Connection(ipAdress, int.Parse(port));

            con.DebugOutput = false;
            //con.SetASDUReceivedHandler(asduReceivedHandler, null);
            con.Connect();

            var telemetries = Telemetry.LoadList(TELEMETRIESPATH);
            var singlePointInformation = telemetries.Where(item => item.TypeId == UsingOfPayload.TypeID.SinglePointInformation);
            var floatingPointInformation = telemetries.Where(item => item.TypeId == UsingOfPayload.TypeID.MeasuredValueShort);

            // Начинаем передачу данных среза
            InformationObject io = new Bitstring32(0, Convert.ToUInt32("1"), new QualityDescriptor());
            ASDU asdu = new ASDU(con.Parameters, CauseOfTransmission.ACTIVATION, false, false, 0, ca, false);
            asdu.AddInformationObject(io);
            con.SendASDU(asdu);

            asdu = new ASDU(con.Parameters, CauseOfTransmission.PERIODIC, false, false, 1, ca, false);
            int valuesInASDU = 0;
            int countOfInfo = 0;
            foreach (var telemetry in singlePointInformation)
            {
                bool bValue = (telemetry.Value >= 1.0f);
                io = new SinglePointInformation(telemetry.ObjectAddress, bValue, new QualityDescriptor());
                asdu.AddInformationObject(io);
                valuesInASDU++;
                countOfInfo++;
                if (valuesInASDU == 30 || countOfInfo == singlePointInformation.Count())
                {
                    con.SendASDU(asdu);
                    asdu = new ASDU(con.Parameters, CauseOfTransmission.PERIODIC, false, false, 1, ca, false);
                    valuesInASDU = 0;
                }
            }
            countOfInfo = 0;
            foreach (var telemetry in floatingPointInformation)
            {
                io = new MeasuredValueShort(telemetry.ObjectAddress, (float)telemetry.Value, new QualityDescriptor());
                asdu.AddInformationObject(io);
                valuesInASDU++;
                countOfInfo++;
                if (valuesInASDU == 30 || countOfInfo == singlePointInformation.Count())
                {
                    con.SendASDU(asdu);
                    asdu = new ASDU(con.Parameters, CauseOfTransmission.PERIODIC, false, false, 1, ca, false);
                    valuesInASDU = 0;
                }
            }
            // Завершаем передачу данных среза
            io = new Bitstring32(0, Convert.ToUInt32("0"), new QualityDescriptor());
            asdu = new ASDU(con.Parameters, CauseOfTransmission.DEACTIVATION, false, false, 0, ca, false);
            asdu.AddInformationObject(io);
            con.SendASDU(asdu);

            /*bool DataReady;
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

                ASDU newAsdu = new ASDU(con.Parameters, CauseOfTransmission.PERIODIC, false, false, 1, ca, false);
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
                if (DataReady) con.SendASDU(newAsdu);                
            }*/

            /*ASDU asdu1 = new ASDU(con.Parameters, CauseOfTransmission.PERIODIC, false, false, 1, ca, false);
            
            for (float val = 0; val < 60; val++)
            {
                io = new MeasuredValueShort(55, val, new QualityDescriptor());
                asdu1.AddInformationObject(io);
            }
            con.SendASDU(asdu1);

            ASDU asdu2 = new ASDU(con.Parameters, CauseOfTransmission.PERIODIC, false, false, 1, ca, false);
            int value = 0;
            for (float i = 0; i < 60; i++)
            {                
                bool bValue = (value >= 1.0f);
                io = new SinglePointInformation(55, bValue, new QualityDescriptor());
                asdu2.AddInformationObject(io);
                value = (value == 0) ? 1 : 0;
            }
            con.SendASDU(asdu2);*/
        }

        /// <summary>
        /// Обработчик полученных ASDU -
        /// - Application specific data unit
        /// </summary>
        /// <param name="parameter">параметр</param>
        /// <param name="asdu">ASDU</param>
        /// <returns>Булево значение о выполнении без ошибок</returns>
		/*private static bool asduReceivedHandler(object parameter, ASDU asdu)
        {
            Console.WriteLine("Получено асду: Ca=" + asdu.Ca + ", TypeId=" + asdu.TypeId);
            if (asdu.Ca != ca)
            {
                Console.WriteLine("Неизестный адрес АСДУ");
                return false;
            }
            if (asdu.TypeId == TypeID.M_SP_NA_1)
            {
                for (int i = 0; i < asdu.NumberOfElements; i++)
                {
                    var val = (SinglePointInformation)asdu.GetElement(i);
                    Console.WriteLine("  IOA: " + val.ObjectAddress + " SP value: " + val.Value);
                }
            }
            else if (asdu.TypeId == TypeID.M_ME_NC_1)
            {
                for (int i = 0; i < asdu.NumberOfElements; i++)
                {
                    var mfv = (MeasuredValueShort)asdu.GetElement(i);
                    Console.WriteLine("  IOA: " + mfv.ObjectAddress + " float value: " + mfv.Value);
                }
            }
            else if (asdu.TypeId == TypeID.M_BO_NA_1)
            {
                for (int i = 0; i < asdu.NumberOfElements; i++)
                {
                    var mfv = (Bitstring32)asdu.GetElement(i);
                    Console.WriteLine("M_BO_NA_1");
                    Console.WriteLine("  IOA: " + mfv.ObjectAddress + " int value: " + mfv.Value);
                }
            }
            else
            {
                Console.WriteLine("Неизвестный тип сообщения!");
                return false;
            }
            return true;
        }*/
    }
}
