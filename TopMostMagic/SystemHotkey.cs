using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;

namespace TopMostMagic
{
	// TODO: this one can be used in many ways so it will do wrong things: multiple registrations without unregistering, etc...
	public class SystemHotkey : System.ComponentModel.Component, IDisposable
	{
		private System.ComponentModel.Container components = null;
		protected Win32.DummyWindowWithEvent m_Window = new Win32.DummyWindowWithEvent();
		//protected Shortcut m_HotKey = Shortcut.None;
		ShortcutInfo _shortcut;
		protected bool isRegistered = false;
		public event System.EventHandler Pressed;
		public event System.EventHandler Error;

		public SystemHotkey(System.ComponentModel.IContainer container)
		{
			container.Add(this);
			InitializeComponent();
			m_Window.ProcessMessage += new Win32.MessageEventHandler(MessageEvent);
		}

		public SystemHotkey()
		{
			InitializeComponent();
			if (!DesignMode)
			{
				m_Window.ProcessMessage += new Win32.MessageEventHandler(MessageEvent);
			}
		}

		public new void Dispose()
		{
			if (isRegistered)
			{
				if (UnregisterHotkey())
					System.Diagnostics.Debug.WriteLine("Unreg: OK");
			}
			System.Diagnostics.Debug.WriteLine("Disposed");
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}

		protected void MessageEvent(object sender, ref Message m, ref bool Handled)
		{
			if ((m.Msg == (int)Win32.Msgs.WM_HOTKEY) && (m.WParam == (IntPtr)this.GetType().GetHashCode()))
			{
				Handled = true;
				System.Diagnostics.Debug.WriteLine("HOTKEY pressed!");
				if (Pressed != null) Pressed(this, EventArgs.Empty);
			}
		}

		protected bool UnregisterHotkey()
		{
			if (isRegistered)
			{
				isRegistered = !Win32.User32.UnregisterHotKey(m_Window.Handle, this.GetType().GetHashCode());

				return isRegistered;
			}
			else
			{
				return false;
			}
		}

		protected bool RegisterHotkey()
		{
			if (_shortcut.isSomething())
			{
				isRegistered = Win32.User32.RegisterHotKey(m_Window.Handle, this.GetType().GetHashCode(), _shortcut.Modifier, _shortcut.KeyCode);
				return isRegistered;
			}
			else
			{
				return false;
			}
		}

		public bool IsRegistered
		{
			get { return isRegistered; }
		}


		public ShortcutInfo Shortcut
		{
			get
			{
				return _shortcut;
			}
			set
			{
				_shortcut = value;
				RegisterHotkey();
			}
		}
	}
}
