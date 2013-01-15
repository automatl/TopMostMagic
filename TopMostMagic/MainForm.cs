using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TopMostMagic;
using System.Runtime.InteropServices;
using System.IO;

namespace TopMostMagic
{
	public partial class MainForm : Form
	{
		protected SystemHotkey hotkey;

		[DllImport("user32.dll")]
		static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();
		[DllImport("user32.dll")]
		static extern int GetWindowLong(IntPtr hWnd, int nIndex);

		static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
		static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

		static readonly int GWL_EXSTYLE = -20;

		const UInt32 WS_EX_TOPMOST = 0x0008;

		const UInt32 SWP_NOSIZE = 0x0001;
		const UInt32 SWP_NOMOVE = 0x0002;
		const UInt32 SWP_SHOWWINDOW = 0x0040;

		const string hotkeyPath = "hotkey";

		public MainForm()
		{
			InitializeComponent();
			trayIcon.Visible = !File.Exists("notray");
			this.Visible = false;

			this.trayIcon.ContextMenu = new ContextMenu();
			ContextMenu menu = this.trayIcon.ContextMenu;
			menu.MenuItems.Add("About", new EventHandler(this.aboutToolStripMenuItem_Click));
			menu.MenuItems.Add("-");
			menu.MenuItems.Add("Change Hotkey", new EventHandler(this.changeHotkeyClick));
			menu.MenuItems.Add("-");
			menu.MenuItems.Add("Exit", new EventHandler(this.exitToolStripMenuItem_Click));

			this.WindowState = FormWindowState.Minimized;
			this.FormBorderStyle = FormBorderStyle.FixedSingle;

			ShowInTaskbar = false;

			Form form1 = new Form();

			form1.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			form1.ShowInTaskbar = false;

			this.Owner = form1;
		}

		private string readHotkey()
		{
			string hotkey = "CtrlT";

			if (File.Exists(MainForm.hotkeyPath))
			{
				TextReader tr = new StreamReader(MainForm.hotkeyPath);
				string buf = tr.ReadToEnd();

				if (buf.Length > 0)
				{
					hotkey = buf;
				}
				tr.Close();
			}

			return hotkey;
		}

		private void writeHotkey(string value)
		{
			if (File.Exists(MainForm.hotkeyPath))
			{
				File.Delete(MainForm.hotkeyPath);
			}

			StreamWriter file = new System.IO.StreamWriter(MainForm.hotkeyPath);
			file.Write(value);
			file.Close();
		}

		private void initHotkeyObj()
		{
			string hotkey = this.readHotkey();

			try
			{
				this.hotkey.Shortcut = SystemHotkeyEnumeration.getValueByKey(hotkey);
			}
			catch (InvalidSystemHotkeyException ex)
			{
				ex.Message.Clone();
				MessageBox.Show("Invalid shortcut passed - [" + hotkey + "] falling back to Ctrl+T", "TopMost Magic", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				this.hotkey.Shortcut = Shortcut.CtrlT;
			}
			
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			this.hotkey = new SystemHotkey();
			this.hotkey.Pressed += new System.EventHandler(this.handler);
			this.initHotkeyObj();
		}

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		public void handler(object sender, EventArgs e)
		{
			IntPtr windowHandle = GetForegroundWindow();

			IntPtr flag;
			if ((GetWindowLong(windowHandle, GWL_EXSTYLE) & WS_EX_TOPMOST) != 0)
			{
				flag = HWND_NOTOPMOST;
			}
			else
			{
				flag = HWND_TOPMOST;
			}

			SetWindowPos(windowHandle, flag, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string txt = 
				"Always missed an ability to make any window 'Always On Top' ;)\n" + 
				"You can do that pressing by " + 
				SystemHotkeyEnumeration.humanizeShortcut(this.hotkey.Shortcut) + 
				" now (affects active window).\n(c) 2013, Automatl";

			MessageBox.Show(txt, "TopMost Magic", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void changeHotkeyClick(object sender, EventArgs e)
		{
			ChangeHotkeyForm form = new ChangeHotkeyForm(this.hotkey.Shortcut.ToString());
			DialogResult res = form.ShowDialog();

			if (res == DialogResult.OK)
			{
				this.writeHotkey(form.getResult());
				this.initHotkeyObj();
			}
		}

		private void hideButton_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}
	}
}
