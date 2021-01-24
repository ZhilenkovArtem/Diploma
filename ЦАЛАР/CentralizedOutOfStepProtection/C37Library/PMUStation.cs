using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace C37Library
{
	[Serializable]
	public class PMUStation
	{
		//STATION NAME
		public string STN { get; set; }
		public ushort IDCODE { get; set; }
		public ushort Format { get; set; }
		public ushort PhasorNumber { get; set; }
		public ushort AnalogNumber { get; set; }
		public ushort DigitalNumber { get; set; }

		//CHANNEL NAMES
		public List<string> ChannelNamePhasor { get; set; }
		public List<string> ChannelNameAnalog { get; set; }
		public List<string> ChannelNameDigital { get; set; }

		//Channel Values
		public List<Complex> PhasorValues { get; set; }
		public List<float> AnalogValues { get; set; }
		public List<List<bool>> DigitalValues { get; set; }

		//CHANNEL UNIT
		public List<uint> PhasorUnit { get; set; }
		public List<uint> AnalogUnit { get; set; }
		public List<uint> DigitalUnit { get; set; }

		public ushort FNOM { get; set; }
		public ushort CFGCNT { get; set; }
		public ushort STAT { get; set; }

		public float FREQ { get; set; }
		public float DFREQ { get; set; }

		public PMUStation(string name, ushort idcode, bool FreqType, bool AnalogType, bool PhasorType, bool CoordType)
		{
			this.STN = ChangeName(name);
			this.IDCODE = idcode;
			this.FormatSet(FreqType, AnalogType, PhasorType, CoordType);

			this.ChannelNamePhasor = new List<string>();
			this.PhasorUnit = new List<uint>();
			this.PhasorNumber = 0;
			this.PhasorValues = new List<Complex>();

			this.ChannelNameAnalog = new List<string>();
			this.AnalogUnit = new List<uint>();
			this.AnalogNumber = 0;
			this.AnalogValues = new List<float>();

			this.DigitalNumber = 0;
		}

		public PMUStation()
		{
			this.ChannelNamePhasor = new List<string>();
			this.PhasorUnit = new List<uint>();
			this.PhasorNumber = 0;
			this.PhasorValues = new List<Complex>();

			this.ChannelNameAnalog = new List<string>();
			this.AnalogUnit = new List<uint>();
			this.AnalogNumber = 0;
			this.AnalogValues = new List<float>();

			this.DigitalNumber = 0;
		}

		public void FormatSet(bool FreqType, bool AnalogType, bool PhasorType, bool CoordType)
		{
			//if true = float or polar, false = integer or rectang. 
			this.Format = 0;
			this.Format |= (CoordType ? 1 : 0);
			this.Format |= (ushort)((PhasorType ? 1 : 0) << 1);
			this.Format = (ushort)((AnalogType ? 1 : 0) << 2);
			this.Format = (ushort)((FreqType ? 1 : 0) << 3);
		}

		public void FormatSet(ushort FormatWord)
		{
			this.Format = FormatWord;
		}

		public bool FormatPhasorTypeGet()
		{
			return ((this.Format & 0x02) >> 1) == 1 ? true : false;
		}

		public bool FormatAnalogTypeGet()
		{
			return ((this.Format & 0x04) >> 2) == 1 ? true : false;
		}

		public bool FormatFREQTypeGet()
		{
			return ((this.Format & 0x08) >> 3) == 1 ? true : false;
		}

		/// <summary>
		/// Установить PHASOR CHANNEL NAME, (TYPE|FACTOR) 
		/// Поле только одной станции PMU
		/// с использованием factor, представленного в 24-битном формате, 
		/// определяемом пользователем
		/// </summary>
		/// <param name="name"></param>
		/// <param name="factor"></param>
		/// <param name="type"></param>
		public void PhasorAdd(string name, uint factor, uint type)
		{
			this.ChannelNamePhasor.Add(ChangeName(name));
			this.PhasorUnit.Add((type << 24) | (factor & 0x0FFFFFF));
			this.PhasorNumber++;
			this.PhasorValues.Add(new Complex(0, 0));
		}

		/// <summary>
		/// Установить PHASOR CHANNEL NAME, (TYPE|FACTOR=1) 
		/// Поле только одной станции PMU
		/// c использованием factor = 1, представленным float числом
		/// </summary>
		/// <param name="name"></param>
		/// <param name=""></param>
		/// <param name="type"></param>
		public void PhasorAdd(string name, uint type)
		{
			this.ChannelNamePhasor.Add(ChangeName(name));
			this.PhasorUnit.Add((type << 24) | 0x01);
			this.PhasorNumber++;
			this.PhasorValues.Add(new Complex(0, 0));
		}

		/// <summary>
		/// Установить ANALOG CHANNEL NAME, (TYPE|FACTOR)
		/// Поле только одной станции PMU
		/// с использованием factor, представленного в 24-битном формате, 
		/// определяемом пользователем
		/// </summary>
		/// <param name="name"></param>
		/// <param name="factor"></param>
		/// <param name="type"></param>
		public void AnalogAdd(string name, uint factor, uint type)
		{
			this.ChannelNameAnalog.Add(ChangeName(name));
			this.AnalogUnit.Add((type << 24) | (factor & 0x0FFFFFF));
			this.AnalogNumber++;
			this.AnalogValues.Add((float)0.0);
		}

		/// <summary>
		/// Установить ANALOG CHANNEL NAME, (TYPE|FACTOR)
		/// Поле только одной станции PMU
		/// c использованием factor = 1, представленным float числом
		/// </summary>
		/// <param name="name"></param>
		/// <param name="type"></param>
		public void AnalogAdd(string name, uint type)
		{
			this.ChannelNameAnalog.Add(ChangeName(name));
			this.AnalogUnit.Add((type << 24) | 0x01);
			this.AnalogNumber++;
			this.AnalogValues.Add((float)0.0);
		}

		private string ChangeName(string name)
        {
			string newName;
			if (name.Length >= 16)
			{
				newName = name.Substring(0, 16);
			}
			else
			{
				int n = 16 - name.Length;
				newName = name;
				for (int i = 0; i < n; i++)
				{
					newName += " ";
				}
			}
			return newName;
		}
	}
}
