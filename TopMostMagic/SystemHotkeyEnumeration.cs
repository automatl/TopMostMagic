using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace TopMostMagic
{

class InvalidSystemHotkeyException : Exception { };

class SystemHotkeyEnumeration
{
	public static string humanizeShortcut(System.Windows.Forms.Shortcut sh)
	{
		return SystemHotkeyEnumeration.humanizeShortcut(sh.ToString());
	}

	public static string humanizeShortcut(string sh)
	{
		sh = sh.Replace("Ctrl", "Ctrl+");
		sh = sh.Replace("Alt", "Alt+");
		sh = sh.Replace("Shift", "Shift+");

		return sh;
	}

	public static List<string> getShortcutArray(bool humanized)
	{
		List<string> col = new List<string>();

		Hashtable vals = SystemHotkeyEnumeration.init();

		ArrayList keys = new ArrayList(vals.Keys);

		if (humanized)
		{
			for (int i = 0; i < keys.Count; i++)
			//foreach (string key in keys)
			{
				keys[i] = SystemHotkeyEnumeration.humanizeShortcut(keys[i] as string);
				//key = SystemHotkeyEnumeration.humanizeShortcut(key);
			}
		}

		List<string> ololo = new List<string>();
		ololo.AddRange(keys.ToArray(typeof(string)) as string[]);

		return ololo;
	}

public static System.Windows.Forms.Shortcut getValueByKey(string key)
{
	key = key.Replace("+", "");
	Hashtable hash = SystemHotkeyEnumeration.init();
	int value;

	try
	{
		value = (int)hash[key];
	}
	catch (NullReferenceException ex)
	{
		ex.Message.Clone();
		value = 0;
		throw new InvalidSystemHotkeyException();
	}

	return (System.Windows.Forms.Shortcut)value;
}

protected static Hashtable init()
{
//hash.Add();
	Hashtable hash = new Hashtable();
	// after Ctrl, Shift, Alt +
	// ololo
hash.Add("None", 0);



hash.Add("Ins", 45);



hash.Add("Del", 46);



hash.Add("F1", 112);



hash.Add("F2", 113);



hash.Add("F3", 114);



hash.Add("F4", 115);



hash.Add("F5", 116);



hash.Add("F6", 117);



hash.Add("F7", 118);



hash.Add("F8", 119);



hash.Add("F9", 120);



hash.Add("F10", 121);



hash.Add("F11", 122);



hash.Add("F12", 123);



hash.Add("ShiftIns", 65581);



hash.Add("ShiftDel", 65582);



hash.Add("ShiftF1", 65648);



hash.Add("ShiftF2", 65649);



hash.Add("ShiftF3", 65650);



hash.Add("ShiftF4", 65651);



hash.Add("ShiftF5", 65652);



hash.Add("ShiftF6", 65653);



hash.Add("ShiftF7", 65654);



hash.Add("ShiftF8", 65655);



hash.Add("ShiftF9", 65656);



hash.Add("ShiftF10", 65657);



hash.Add("ShiftF11", 65658);



hash.Add("ShiftF12", 65659);



hash.Add("CtrlIns", 131117);



hash.Add("CtrlDel", 131118);



hash.Add("Ctrl0", 131120);



hash.Add("Ctrl1", 131121);



hash.Add("Ctrl2", 131122);



hash.Add("Ctrl3", 131123);



hash.Add("Ctrl4", 131124);



hash.Add("Ctrl5", 131125);



hash.Add("Ctrl6", 131126);



hash.Add("Ctrl7", 131127);



hash.Add("Ctrl8", 131128);



hash.Add("Ctrl9", 131129);



hash.Add("CtrlA", 131137);



hash.Add("CtrlB", 131138);



hash.Add("CtrlC", 131139);



hash.Add("CtrlD", 131140);



hash.Add("CtrlE", 131141);



hash.Add("CtrlF", 131142);



hash.Add("CtrlG", 131143);



hash.Add("CtrlH", 131144);



hash.Add("CtrlI", 131145);



hash.Add("CtrlJ", 131146);



hash.Add("CtrlK", 131147);



hash.Add("CtrlL", 131148);



hash.Add("CtrlM", 131149);



hash.Add("CtrlN", 131150);



hash.Add("CtrlO", 131151);



hash.Add("CtrlP", 131152);



hash.Add("CtrlQ", 131153);



hash.Add("CtrlR", 131154);



hash.Add("CtrlS", 131155);



hash.Add("CtrlT", 131156);



hash.Add("CtrlU", 131157);



hash.Add("CtrlV", 131158);



hash.Add("CtrlW", 131159);



hash.Add("CtrlX", 131160);



hash.Add("CtrlY", 131161);



hash.Add("CtrlZ", 131162);



hash.Add("CtrlF1", 131184);



hash.Add("CtrlF2", 131185);



hash.Add("CtrlF3", 131186);



hash.Add("CtrlF4", 131187);



hash.Add("CtrlF5", 131188);



hash.Add("CtrlF6", 131189);



hash.Add("CtrlF7", 131190);



hash.Add("CtrlF8", 131191);



hash.Add("CtrlF9", 131192);



hash.Add("CtrlF10", 131193);



hash.Add("CtrlF11", 131194);



hash.Add("CtrlF12", 131195);



hash.Add("CtrlShift0", 196656);



hash.Add("CtrlShift1", 196657);



hash.Add("CtrlShift2", 196658);



hash.Add("CtrlShift3", 196659);



hash.Add("CtrlShift4", 196660);



hash.Add("CtrlShift5", 196661);



hash.Add("CtrlShift6", 196662);



hash.Add("CtrlShift7", 196663);



hash.Add("CtrlShift8", 196664);



hash.Add("CtrlShift9", 196665);



hash.Add("CtrlShiftA", 196673);



hash.Add("CtrlShiftB", 196674);



hash.Add("CtrlShiftC", 196675);



hash.Add("CtrlShiftD", 196676);



hash.Add("CtrlShiftE", 196677);



hash.Add("CtrlShiftF", 196678);



hash.Add("CtrlShiftG", 196679);



hash.Add("CtrlShiftH", 196680);



hash.Add("CtrlShiftI", 196681);



hash.Add("CtrlShiftJ", 196682);



hash.Add("CtrlShiftK", 196683);



hash.Add("CtrlShiftL", 196684);



hash.Add("CtrlShiftM", 196685);



hash.Add("CtrlShiftN", 196686);



hash.Add("CtrlShiftO", 196687);



hash.Add("CtrlShiftP", 196688);



hash.Add("CtrlShiftQ", 196689);



hash.Add("CtrlShiftR", 196690);



hash.Add("CtrlShiftS", 196691);



hash.Add("CtrlShiftT", 196692);



hash.Add("CtrlShiftU", 196693);



hash.Add("CtrlShiftV", 196694);



hash.Add("CtrlShiftW", 196695);



hash.Add("CtrlShiftX", 196696);



hash.Add("CtrlShiftY", 196697);



hash.Add("CtrlShiftZ", 196698);



hash.Add("CtrlShiftF1", 196720);



hash.Add("CtrlShiftF2", 196721);



hash.Add("CtrlShiftF3", 196722);



hash.Add("CtrlShiftF4", 196723);



hash.Add("CtrlShiftF5", 196724);



hash.Add("CtrlShiftF6", 196725);



hash.Add("CtrlShiftF7", 196726);



hash.Add("CtrlShiftF8", 196727);



hash.Add("CtrlShiftF9", 196728);



hash.Add("CtrlShiftF10", 196729);



hash.Add("CtrlShiftF11", 196730);



hash.Add("CtrlShiftF12", 196731);



hash.Add("AltBksp", 262152);



hash.Add("AltLeftArrow", 262181);



hash.Add("AltUpArrow", 262182);



hash.Add("AltRightArrow", 262183);



hash.Add("AltDownArrow", 262184);



hash.Add("Alt0", 262192);



hash.Add("Alt1", 262193);



hash.Add("Alt2", 262194);



hash.Add("Alt3", 262195);



hash.Add("Alt4", 262196);



hash.Add("Alt5", 262197);



hash.Add("Alt6", 262198);



hash.Add("Alt7", 262199);



hash.Add("Alt8", 262200);



hash.Add("Alt9", 262201);



hash.Add("AltF1", 262256);



hash.Add("AltF2", 262257);



hash.Add("AltF3", 262258);



hash.Add("AltF4", 262259);



hash.Add("AltF5", 262260);



hash.Add("AltF6", 262261);



hash.Add("AltF7", 262262);



hash.Add("AltF8", 262263);



hash.Add("AltF9", 262264);



hash.Add("AltF10", 262265);



hash.Add("AltF11", 262266);



hash.Add("AltF12", 262267);

return hash;
}
}
}
