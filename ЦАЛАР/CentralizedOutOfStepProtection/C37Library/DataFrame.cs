using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace C37Library
{
    [Serializable]
    public class DataFrame : C37118
    {
        /// <summary>
        /// Data store
        /// </summary>
        public ConfigFrame AssociateCurrentConfig { get; set; }

        /// <summary>
        /// Инициализирует 2 первых байта с идентификатором DataFrame
        /// Добавляет заголовок пакета 0xAA | 0x01
        /// </summary>
        /// <param name="cfg"></param>
        public DataFrame(ConfigFrame cfg)
        {
            this.SYNC = (A_SYNC_AA << 8 | A_SYNC_DATA);
            this.AssociateCurrentConfig = cfg;
        }

        public void Unpack(byte[] buffer)
        {
            SYNC = BitConverter.ToUInt16(buffer, 0);
            FRAMESIZE = BitConverter.ToUInt16(buffer, 2);
            IDCODE = BitConverter.ToUInt16(buffer, 4);
            SOC = BitConverter.ToUInt32(buffer, 6);
            FRACSEC = BitConverter.ToUInt32(buffer, 10);

            int aux = 14;
            for (int i = 0; i < AssociateCurrentConfig.NumberPMU; i++)
            {
                AssociateCurrentConfig.PMUStationList[i].STAT = BitConverter.ToUInt16(buffer, aux);
                aux += 2;

                for (int j = 0; j < AssociateCurrentConfig.PMUStationList[i].PhasorNumber; j++)
                {
                    var real = BitConverter.ToSingle(buffer, aux);
                    aux += 4;
                    var imaginary = BitConverter.ToSingle(buffer, aux);
                    aux += 4;
                    AssociateCurrentConfig.PMUStationList[i].PhasorValues.Add(new Complex(real, imaginary));
                }

                AssociateCurrentConfig.PMUStationList[i].FREQ = BitConverter.ToSingle(buffer, aux);
                aux += 4;
                AssociateCurrentConfig.PMUStationList[i].DFREQ = BitConverter.ToSingle(buffer, aux);
                aux += 4;

                for (int j = 0; j < AssociateCurrentConfig.PMUStationList[i].AnalogNumber; j++)
                {
                    AssociateCurrentConfig.PMUStationList[i].AnalogValues.Add(BitConverter.ToSingle(buffer, aux));
                    aux += 4;
                }
            }

            CHK = BitConverter.ToUInt16(buffer, aux);
        }

        public byte[] Pack()
        {
            ushort size = 14;
            for (int i = 0; i < this.AssociateCurrentConfig.NumberPMU; i++)
            {
                //STAT
                size += 2;
                //PASHOR
                size += (ushort)(8 * AssociateCurrentConfig.PMUStationList[i].PhasorNumber);
                //FREQ and DFREQ
                size += 4;//FREQ
                size += 4;//DFREQ
                //ANALOG
                size += (ushort)(4 * AssociateCurrentConfig.PMUStationList[i].AnalogNumber);
                //DIGITAL
                size += (ushort)(2 * AssociateCurrentConfig.PMUStationList[i].DigitalNumber);
            }
            //Add CRC Empty Field
            size += 2;
            //set frame size
            this.FRAMESIZE = size;

            byte[] buffer = new byte[0];

            buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(SYNC));     
            buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(FRAMESIZE));
            buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(IDCODE));   
            buffer = CompleteByteMas(buffer, 4, BitConverter.GetBytes(SOC));      
            buffer = CompleteByteMas(buffer, 4, BitConverter.GetBytes(FRACSEC));  

            foreach (var pmu in AssociateCurrentConfig.PMUStationList)
            {
                buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(pmu.STAT));

                foreach (var complex in pmu.PhasorValues)
                {
                    buffer = CompleteByteMas(buffer, 4, BitConverter.GetBytes((float)complex.Real));
                    buffer = CompleteByteMas(buffer, 4, BitConverter.GetBytes((float)complex.Imaginary));    
                }

                buffer = CompleteByteMas(buffer, 4, BitConverter.GetBytes(pmu.FREQ)); 
                buffer = CompleteByteMas(buffer, 4, BitConverter.GetBytes(pmu.DFREQ));

                foreach (var val in pmu.AnalogValues)
                {
                    buffer = CompleteByteMas(buffer, 4, BitConverter.GetBytes(val));
                }                                                                         
            }
            var crc = Calc_CRC(buffer, (uint)FRAMESIZE-2);
            this.CHK = crc;
            buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(CHK));
            
            return buffer;
        }
    }
}
