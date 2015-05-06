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

		const string hotkeyPath = "hotkey_binary";

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

		private ShortcutInfo readHotkey()
		{
			ShortcutInfo hotkey = new ShortcutInfo();

			if (File.Exists(MainForm.hotkeyPath))
			{
				using (var tr = new BinaryReader(File.Open(MainForm.hotkeyPath, FileMode.Open)))
				{
					hotkey.Modifier = tr.ReadUInt32();
					hotkey.KeyCode = tr.ReadUInt32();
				}
			}

			return hotkey;
		}

		private void writeHotkey(ShortcutInfo value)
		{
			if (File.Exists(MainForm.hotkeyPath))
			{
				File.Delete(MainForm.hotkeyPath);
			}

			using (var file = new System.IO.BinaryWriter(File.Open(MainForm.hotkeyPath, FileMode.OpenOrCreate)))
			{
				file.Write(value.Modifier);
				file.Write(value.KeyCode);
			}
		}

		private void initHotkeyObj()
		{
			this.hotkey.Shortcut = this.readHotkey();
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
			IntPtr windowHandle = Win32.User32.GetForegroundWindow();

			IntPtr flag;
			if ((Win32.User32.GetWindowLong(windowHandle, Win32.User32.GWL_EXSTYLE) & Win32.User32.WS_EX_TOPMOST) != 0)
			{
				flag = Win32.User32.HWND_NOTOPMOST;
			}
			else
			{
				flag = Win32.User32.HWND_TOPMOST;
			}

			Win32.User32.SetWindowPos(windowHandle, flag, 0, 0, 0, 0, Win32.User32.SWP_NOMOVE | Win32.User32.SWP_NOSIZE | Win32.User32.SWP_SHOWWINDOW);
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
				this.hotkey.Shortcut.ToString() + 
				" now (affects active window).\n(c) 2015, Automatl";

			MessageBox.Show(txt, "TopMost Magic", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void changeHotkeyClick(object sender, EventArgs e)
		{
			ChangeHotkeyForm form = new ChangeHotkeyForm(this.hotkey.Shortcut);
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
