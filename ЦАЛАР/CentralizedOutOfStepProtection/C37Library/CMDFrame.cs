using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C37Library
{
    [Serializable]
    public class CMDFrame : C37118
    {
        public ushort CMD { get; set; }
        public byte[] EXTRAFRAME { get; set; }

        public CMDFrame()
        {
            this.SYNC = (A_SYNC_AA << 8 | A_SYNC_CMD);
            this.FRAMESIZE = 18;
            this.EXTRAFRAME = new byte[0];
        }

        public void Unpack(byte[] buffer)
        {
            SYNC = BitConverter.ToUInt16(buffer, 0);
            FRAMESIZE = BitConverter.ToUInt16(buffer, 2);
            IDCODE = BitConverter.ToUInt16(buffer, 4);
            SOC = BitConverter.ToUInt32(buffer, 6);
            FRACSEC = BitConverter.ToUInt32(buffer, 10);
            CMD = BitConverter.ToUInt16(buffer, 14);

            for (int ptr = 0; ptr < FRAMESIZE - 18; ptr++)
            {
                EXTRAFRAME[ptr] = buffer[ptr + 16];
            }

            CHK = BitConverter.ToUInt16(buffer, FRAMESIZE - 2);
        }

        public byte[] Pack()
        {
            byte[] buffer = new byte[0];

            FRAMESIZE = (ushort)(EXTRAFRAME.Length + 18);

            buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(SYNC));
            buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(FRAMESIZE));
            buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(IDCODE));
            buffer = CompleteByteMas(buffer, 4, BitConverter.GetBytes(SOC));
            buffer = CompleteByteMas(buffer, 4, BitConverter.GetBytes(FRACSEC));
            buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(CMD));

            //if (EXTRAFRAME != null)
            //{
                byte[] extraBuf = new byte[EXTRAFRAME.Length];
                for (int ptr = 0; ptr < FRAMESIZE - 18; ptr++)
                {
                    extraBuf[ptr] = EXTRAFRAME[ptr];
                }
                buffer = CompleteByteMas(buffer, extraBuf.Length, extraBuf);
            //}

            var crc = Calc_CRC(buffer, (uint)FRAMESIZE - 2);
            this.CHK = crc;
            buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(CHK));

            return buffer;
        }
    }
}
