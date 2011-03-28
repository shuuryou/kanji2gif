using System;
using System.Globalization;
using System.Windows.Forms;

namespace Kanji2GIF
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

			this.Text = string.Format(CultureInfo.CurrentCulture,
				this.Text, AssemblyAttributes.AssemblyTitle,
				AssemblyAttributes.AssemblyVersion);

			mainInstructionLabel.Text = string.Format(CultureInfo.CurrentCulture,
				mainInstructionLabel.Text, AssemblyAttributes.AssemblyTitle);
		}

		private void browseButton_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
				outDirTextBox.Text = folderBrowserDialog.SelectedPath;
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
