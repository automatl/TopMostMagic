using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TopMostMagic
{
	public partial class ChangeHotkeyForm : Form
	{
		private string current;
		private string result;
		public string getResult()
		{
			return this.result;
		}

		private void setResult()
		{
			this.result = this.hotkeyComboBox.Text;
		}

		public ChangeHotkeyForm(string current)
		{
			this.current = current;
			InitializeComponent();
		}

		private void ChangeHotkeyForm_Load(object sender, EventArgs e)
		{
			List<string> items = SystemHotkeyEnumeration.getShortcutArray(true);

			foreach (string item in items)
			{
				this.hotkeyComboBox.Items.Add(item);
			}

			this.hotkeyComboBox.Text = SystemHotkeyEnumeration.humanizeShortcut(this.current);
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			this.setResult();
		}
	}
}
