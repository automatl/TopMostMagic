using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TopMostMagic.Win32;

namespace TopMostMagic
{
	public class ShortcutInfo
	{
		public ShortcutInfo()
		{
			Modifier = 0;
			KeyCode = 0;
		}

		public ShortcutInfo(bool alt, bool ctrl, bool shift, char key)
		{
			KeyCode = (uint)key;
			Modifier = 0;

			if (alt)
			{
				Modifier += (uint)Modifiers.MOD_ALT;
			}

			if (ctrl)
			{
				Modifier += (uint)Modifiers.MOD_CONTROL;
			}

			if (shift)
			{
				Modifier += (uint)Modifiers.MOD_SHIFT;
			}
		}

		public bool isSomething()
		{
			return KeyCode > 0 && Modifier > 0;
		}

		public bool hasAlt()
		{
			return (Modifier & (uint)Modifiers.MOD_ALT) == (uint)Modifiers.MOD_ALT;
		}

		public bool hasCtrl()
		{
			return (Modifier & (uint)Modifiers.MOD_CONTROL) == (uint)Modifiers.MOD_CONTROL;
		}

		public bool hasShift()
		{
			return (Modifier & (uint)Modifiers.MOD_SHIFT) == (uint)Modifiers.MOD_SHIFT;
		}

		public override string ToString()
		{
			string result = "";

			List<string> mods = new List<string>();

			if (hasCtrl())
			{
				mods.Add("Ctrl");
			}

			if (hasAlt())
			{
				mods.Add("Alt");
			}

			if (hasShift())
			{
				mods.Add("Shift");
			}

			result = mods.Aggregate((string a, string b) => {return a + "+" + b;});
			result += "+" + ((char)KeyCode);

			return result;
		}

		public UInt32 Modifier { get; set; }
		public UInt32 KeyCode { get; set; }
	}
}
