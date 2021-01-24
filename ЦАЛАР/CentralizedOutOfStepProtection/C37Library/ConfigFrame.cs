using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace C37Library
{
    [Serializable]
    public class ConfigFrame : C37118
    {
        public uint TimeBase { get; set; }
        public ushort NumberPMU { get; set; }
        public ushort DataRate { get; set; }
        /// <summary>
        /// PMU UNIT LIST
        /// </summary>
        public List<PMUStation> PMUStationList { get; set; }

        public ConfigFrame()
        {
            //Set header with RUNNING CONFIG (config mode 2)
            this.SYNC = (A_SYNC_AA << 8 | A_SYNC_CFG2);
            this.NumberPMU = 0;
            this.PMUStationList = new List<PMUStation>();
        }

        public void Unpack(byte[] buffer)
        {
            SYNC = BitConverter.ToUInt16(buffer, 0);
            FRAMESIZE = BitConverter.ToUInt16(buffer, 2);
            IDCODE = BitConverter.ToUInt16(buffer, 4);
            SOC = BitConverter.ToUInt32(buffer, 6);
            FRACSEC = BitConverter.ToUInt32(buffer, 10);
            TimeBase = BitConverter.ToUInt32(buffer, 14);
            NumberPMU = BitConverter.ToUInt16(buffer, 18);

            int aux = 20;
            for (int pos = 0; pos < NumberPMU; pos++)
            {
                var pmu = new PMUStation();

                var stn = new char[16];
                for (int i = aux; i < aux + 16; i++)
                {
                    stn[i - aux] = BitConverter.ToChar(new byte[2] { buffer[i], 0 }, 0);
                }
                pmu.STN = new string(stn);
                aux += 16;

                pmu.IDCODE = BitConverter.ToUInt16(buffer, aux);
                aux += 2;
                pmu.Format = BitConverter.ToUInt16(buffer, aux);
                aux += 2;
                pmu.PhasorNumber = BitConverter.ToUInt16(buffer, aux);
                aux += 2;
                pmu.AnalogNumber = BitConverter.ToUInt16(buffer, aux);
                aux += 2;
                pmu.DigitalNumber = BitConverter.ToUInt16(buffer, aux);
                aux += 2;

                for (int chan = 0; chan < pmu.PhasorNumber; chan++)
                {
                    var channelName = new char[16];
                    for (int i = aux; i < aux + 16; i++)
                    {
                        channelName[i - aux] = BitConverter.ToChar(new byte[2] { buffer[i], 0 }, 0);
                    }
                    pmu.ChannelNamePhasor.Add(new string(channelName));
                    aux += 16;
                }
                for (int chan = 0; chan < pmu.AnalogNumber; chan++)
                {
                    var channelName = new char[16];
                    for (int i = aux; i < aux + 16; i++)
                    {
                        channelName[i - aux] = BitConverter.ToChar(new byte[2] { buffer[i], 0 }, 0);
                    }
                    pmu.ChannelNameAnalog.Add(new string(channelName));
                    aux += 16;
                }
                for (int chan = 0; chan < pmu.DigitalNumber; chan++)
                {
                    var channelName = new char[16];
                    for (int i = aux; i < aux + 16; i++)
                    {
                        channelName[i - aux] = BitConverter.ToChar(new byte[2] { buffer[i], 0 }, 0);
                    }
                    pmu.ChannelNameDigital.Add(new string(channelName));
                    aux += 16;
                }
                for (int un = 0; un < pmu.PhasorNumber; un++)
                {
                    pmu.PhasorUnit.Add(BitConverter.ToUInt32(buffer, aux));
                    aux += 4;
                }
                for (int un = 0; un < pmu.AnalogNumber; un++)
                {
                    pmu.PhasorUnit.Add(BitConverter.ToUInt32(buffer, aux));
                    aux += 4;
                }
                for (int un = 0; un < pmu.DigitalNumber; un++)
                {
                    pmu.PhasorUnit.Add(BitConverter.ToUInt32(buffer, aux));
                    aux += 4;
                }

                pmu.FNOM = BitConverter.ToUInt16(buffer, aux);
                aux += 2;
                pmu.CFGCNT = BitConverter.ToUInt16(buffer, aux);
                aux += 2;
                PMUStationList.Add(pmu);
            }

            DataRate = BitConverter.ToUInt16(buffer, aux);
            aux += 2;
            CHK = BitConverter.ToUInt16(buffer, aux);
        }

        public byte[] Pack()
        {
            ushort size = 24;

            // compute channel numbers foreach exists pmu
            for (int i = 0; i < this.NumberPMU; i++)
            {
                size += 30; // 26 + 4
                size += (ushort)(16 * (PMUStationList[i].PhasorNumber + PMUStationList[i].AnalogNumber + (16 * PMUStationList[i].DigitalNumber)));
                size += (ushort)(4 * PMUStationList[i].PhasorNumber + 4 * PMUStationList[i].AnalogNumber + 4 * PMUStationList[i].DigitalNumber);
            }

            // set frame size
            FRAMESIZE = size;

            byte[] buffer = new byte[0];

            buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(SYNC));      //Console.WriteLine($"SYNC={SYNC}: {string.Join(" ", buffer)}");
            buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(FRAMESIZE)); //Console.WriteLine($"+FRAMESIZE={FRAMESIZE}: {string.Join(" ", buffer)}");
            buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(IDCODE));    //Console.WriteLine($"+IDCODE={IDCODE}: {string.Join(" ", buffer)}");
            buffer = CompleteByteMas(buffer, 4, BitConverter.GetBytes(SOC));       //Console.WriteLine($"+SOC={SOC}: {string.Join(" ", buffer)}");
            buffer = CompleteByteMas(buffer, 4, BitConverter.GetBytes(FRACSEC));   //Console.WriteLine($"+FRACSEC={FRACSEC}: {string.Join(" ", buffer)}");
            buffer = CompleteByteMas(buffer, 4, BitConverter.GetBytes(TimeBase));  //Console.WriteLine($"+TimeBase={TimeBase}: {string.Join(" ", buffer)}");
            buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(NumberPMU)); //Console.WriteLine($"+NumberPMU={NumberPMU}: {string.Join(" ", buffer)}");

            foreach (var pmu in PMUStationList)
            {
                var stnSymbols = pmu.STN.ToArray();
                var stnBuff = new byte[16];
                for (int j = 0; j < 16; j++)
                {
                    stnBuff[j] = BitConverter.GetBytes(stnSymbols[j])[0];
                }
                buffer = CompleteByteMas(buffer, 16, stnBuff);                                 //Console.WriteLine($"+STN={pmu.STN}: ({buffer.Length}) {string.Join(" ", buffer)}");

                buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(pmu.IDCODE));        //Console.WriteLine($"+IDCODE={pmu.IDCODE}: ({buffer.Length}) {string.Join(" ", buffer)}");
                buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(pmu.Format));        //Console.WriteLine($"+Format={pmu.Format}: ({buffer.Length}) {string.Join(" ", buffer)}");
                buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(pmu.PhasorNumber));  //Console.WriteLine($"+PhasorNumber={pmu.PhasorNumber}: ({buffer.Length}) {string.Join(" ", buffer)}");
                buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(pmu.AnalogNumber));  //Console.WriteLine($"+AnalogNumber={pmu.AnalogNumber}: ({buffer.Length}) {string.Join(" ", buffer)}");
                buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(pmu.DigitalNumber)); //Console.WriteLine($"+DigitalNumber={pmu.DigitalNumber}: ({buffer.Length}) {string.Join(" ", buffer)}");

                // Channels Name
                //Phasor
                for (int i = 0; i < pmu.PhasorNumber; i++)
                {
                    var symbols = pmu.ChannelNamePhasor[i].ToArray();
                    var tempBuff = new byte[16];
                    for (int j = 0; j < 16; j++)
                    {
                        tempBuff[j] = BitConverter.GetBytes(symbols[j])[0];
                    }
                    buffer = CompleteByteMas(buffer, 16, tempBuff);                            //Console.WriteLine($"+ChannelNamePhasor{i}={pmu.ChannelNamePhasor[i]}: ({buffer.Length}) {string.Join(" ", buffer)}");
                }
                //Analog
                for (int i = 0; i < pmu.AnalogNumber; i++)
                {
                    var symbols = pmu.ChannelNameAnalog[i].ToArray();
                    var tempBuff = new byte[16];                         
                    for (int j = 0; j < 16; j++)
                    {
                        tempBuff[j] = BitConverter.GetBytes(symbols[j])[0];
                    }
                    buffer = CompleteByteMas(buffer, 16, tempBuff);                            //Console.WriteLine($"+ChannelNameAnalog{i}={pmu.ChannelNameAnalog[i]}: ({buffer.Length}) {string.Join(" ", buffer)}");
                }
                //UNIT FACTOR
                //Phasor
                for (int i = 0; i < pmu.PhasorNumber; i++)
                {
                    buffer = CompleteByteMas(buffer, 4, BitConverter.GetBytes(pmu.PhasorUnit[i])); //Console.WriteLine($"+PhasorUnit{i}={pmu.PhasorUnit[i]}: ({buffer.Length}) {string.Join(" ", buffer)}");
                }
                //Analog
                for (int i = 0; i < pmu.AnalogNumber; i++)
                {
                    buffer = CompleteByteMas(buffer, 4, BitConverter.GetBytes(pmu.AnalogUnit[i])); //Console.WriteLine($"+AnalogUnit{i}={pmu.AnalogUnit[i]}: ({buffer.Length}) {string.Join(" ", buffer)}");
                }

                buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(pmu.FNOM));   //Console.WriteLine($"+FNOM={pmu.FNOM}: ({buffer.Length}) {string.Join(" ", buffer)}");
                buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(pmu.CFGCNT)); //Console.WriteLine($"+CFGCNT={pmu.CFGCNT}: ({buffer.Length}) {string.Join(" ", buffer)}");
            }

            buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(DataRate));       //Console.WriteLine($"+DataRate={DataRate}: ({buffer.Length}) {string.Join(" ", buffer)}");

            var crc = Calc_CRC(buffer, (uint)FRAMESIZE - 2);
            CHK = crc;
            buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(CHK));            //Console.WriteLine($"+CHK={CHK}: ({buffer.Length}) {string.Join(" ", buffer)}");

            return buffer;
        }

        /// <summary>
        /// ADD a PMUStation UNIT to PMUStationList and increase NumberPMU
        /// </summary>
        /// <param name="pmu"></param>
        public void PMUStationAdd(PMUStation pmu)
        {
            this.PMUStationList.Add(pmu);
            this.NumberPMU++;
        }
    }
}
