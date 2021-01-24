using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C37Library
{
    [Serializable]
    public class C37118
    {
        /// <summary>
        /// IEEE Std C37.118-2005 SynchroPhasor Message Prefixes
        /// 0xAA = Frame Synchronization byte. Start of Message Character
        /// </summary>
        protected const ushort A_SYNC_AA = 0xAA;

        /// <summary>
        /// IEEE Std C37.118-2005 SynchroPhasor Message Prefixes
        /// 0x01 = Data Frame byte.
        /// </summary>
        protected const ushort A_SYNC_DATA = 0x01;

        /// <summary>
        /// IEEE Std C37.118-2005 SynchroPhasor Message Prefixes
        /// 0x11 = Header Frame byte.
        /// </summary>
        protected const ushort A_SYNC_HDR = 0x11;

        /// <summary>
        /// IEEE Std C37.118-2005 SynchroPhasor Message Prefixes
        /// 0x01 = Config1 Frame byte.
        /// </summary>
        protected const ushort A_SYNC_CFG1 = 0x21;

        /// <summary>
        /// IEEE Std C37.118-2005 SynchroPhasor Message Prefixes
        /// 0x01 = Config2 Frame byte.
        /// </summary>
        protected const ushort A_SYNC_CFG2 = 0x31;

        /// <summary>
        /// IEEE Std C37.118-2005 SynchroPhasor Message Prefixes
        /// 0x01 = Command Frame byte.
        /// </summary>
        protected const ushort A_SYNC_CMD = 0x41;

        public ushort SYNC { get; set; }
        public ushort FRAMESIZE { get; set; }
        public ushort IDCODE { get; set; }
        public uint SOC { get; set; }
        public uint FRACSEC { get; set; }
        public ushort CHK { get; set; }

        public ushort Calc_CRC(byte[] sData, uint iDataLen)
        {
            ushort iCrc = 0xFFFF;   // 0xFFFF is specific for SynchroPhasor Data CRC
            ushort iCalc1;
            ushort iCalc2;
            for (ushort i = 0; i < iDataLen; i++)
            {
                iCalc1 = (ushort)((iCrc >> 8) ^ sData[i]);
                iCrc <<= 8;
                iCalc2 = (ushort)(iCalc1 ^ (iCalc1 >> 4));
                iCrc ^= iCalc2;
                iCalc2 <<= 5;
                iCrc ^= iCalc2;
                iCalc2 <<= 7;
                iCrc ^= iCalc2;
            }
            return iCrc;
        }

        protected byte[] CompleteByteMas(byte[] mainMas, int bytesNmb, byte[] newBytes)
        {
            var newMas = new byte[mainMas.Length + bytesNmb];

            mainMas.CopyTo(newMas, 0);
            newBytes.CopyTo(newMas, mainMas.Length);

            return newMas;
        }
    }

    public enum PhasorUnitBit
    {
        Voltage = 0,
        Current = 1,
    };

    public enum AnalogUnitBit
    {
        SinglePointOnWave = 0,
        RMSAnalogInput = 1,
        PeakAnalogInput = 2,
    };

    public enum FreqNom
    {
        FN60HZ = 0,
        FN50HZ = 1,
    };
}
