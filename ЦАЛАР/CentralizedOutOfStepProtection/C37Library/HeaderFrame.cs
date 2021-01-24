using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace C37Library
{
	[Serializable]
	public class HeaderFrame : C37118
    {
        public string Info { get; set; }

		public HeaderFrame(string info)
        {
			this.SYNC = (A_SYNC_AA << 8 | A_SYNC_HDR);
			this.FRAMESIZE = 16;
            this.Info = info;
        }

		public void Unpack(byte[] buffer)
        {
			SYNC = BitConverter.ToUInt16(buffer, 0);
			FRAMESIZE = BitConverter.ToUInt16(buffer, 2);
			IDCODE = BitConverter.ToUInt16(buffer, 4);
			SOC = BitConverter.ToUInt32(buffer, 6);
			FRACSEC = BitConverter.ToUInt32(buffer, 10);

			var sizeData = (ushort)(FRAMESIZE - 16);
			char[] symbols = new char[sizeData];
			for (int i = 14; i < 14 + sizeData; i++)
			{
				symbols[i - 14] = BitConverter.ToChar(new byte[2] { buffer[i], 0 }, 0);
			}
			Info = new string(symbols);

			CHK = BitConverter.ToUInt16(buffer, FRAMESIZE - 2);
		}

		public byte[] Pack()
        {
			byte[] buffer = new byte[0];
			
			int size = 16 + Info.Length;
			this.FRAMESIZE = (ushort)size;
			                                                                      
			buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(SYNC));     
			buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(FRAMESIZE));
			buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(IDCODE));   
			buffer = CompleteByteMas(buffer, 4, BitConverter.GetBytes(SOC));      
			buffer = CompleteByteMas(buffer, 4, BitConverter.GetBytes(FRACSEC));  

			var symbols = Info.ToArray();
			var infoBuff = new byte[Info.Length];
			for (int i = 0; i < Info.Length; i++)
			{
				infoBuff[i] = BitConverter.GetBytes(symbols[i])[0];
			}
			buffer = CompleteByteMas(buffer, infoBuff.Length, infoBuff);    

			var crc = Calc_CRC(buffer, (uint)FRAMESIZE - 2);
			this.CHK = crc;
			buffer = CompleteByteMas(buffer, 2, BitConverter.GetBytes(CHK));

			return buffer;
		}

		/*int pack(char[] buff)
		{
			char[] aux_buff;
			short shptr;
			long lptr;
			string str = Info;

			int size = 16 + str.Length;

			FRAMESIZE = size; // set frame size

			//copy buff memory address
			aux_buff = buff;
			//create a short and long pointers, and increment by byte_size(2,4...)
			shptr = (short)aux_buff;
			shptr = (short)IPAddress.HostToNetworkOrder(SYNC);
			aux_buff += 2;
			shptr = (short)aux_buff;
			shptr = (short)IPAddress.HostToNetworkOrder(FRAMESIZE);
			aux_buff += 2;
			shptr = (short)aux_buff;
			shptr = (short)IPAddress.HostToNetworkOrder(IDCODE);
			aux_buff += 2;
			lptr = (long)aux_buff;
			lptr = IPAddress.HostToNetworkOrder(SOC);
			aux_buff += 4;
			lptr = (long)aux_buff;
			lptr = IPAddress.HostToNetworkOrder(FRACSEC);
			aux_buff += 4;
			char[] cstr = new char[str.Length];
			//Get name string and convert to char string
			strcpy(cstr, str.c_str());
			for (int ptr = 0; ptr < str.size(); ptr++)
			{
				aux_buff[ptr] = cstr[ptr];
			}
			aux_buff += str.size();

			// Compute CRC from current frame
			unsigned short crc_aux = this->Calc_CRC(*buff, this->FRAMESIZE_get() - 2);
			this->CHK_set(crc_aux);

			aux_buff = *buff + (this->FRAMESIZE_get() - 2);
			shptr = (unsigned short*) (aux_buff);
			*shptr = htons(this->CHK_get());
			return (this->FRAMESIZE_get());
		}*/
	}
}
