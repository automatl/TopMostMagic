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
		private ShortcutInfo result;
		public ShortcutInfo getResult()
		{
			return this.result;
		}

		private void updateResult()
		{
			if (txtButton.Text.Length > 0)
			{
				this.result = new ShortcutInfo(chkAlt.Checked, chkCtrl.Checked, chkShift.Checked, txtButton.Text[0]);
			}
		}

		public ChangeHotkeyForm(ShortcutInfo current)
		{
			result = current;
			InitializeComponent();

			chkAlt.Checked = result.hasAlt();
			chkShift.Checked = result.hasShift();
			chkCtrl.Checked = result.hasCtrl();
			txtButton.Text = ((char)result.KeyCode).ToString();
		}

		private void ChangeHotkeyForm_Load(object sender, EventArgs e)
		{
			this.result = new ShortcutInfo();
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			updateResult();
		}

		private void txtButton_KeyUp(object sender, KeyEventArgs e)
		{
			e.Handled = true;

			if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
			{
				txtButton.Text = e.KeyCode.ToString();
			}
			else if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
			{
				txtButton.Text = e.KeyCode.ToString()[1].ToString();
			}
			else
			{
				txtButton.Text = "";
			}
		}
	}
}
