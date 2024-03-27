namespace Kanji2GIF
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.wordlistLabel = new System.Windows.Forms.Label();
            this.outDirLabel = new System.Windows.Forms.Label();
            this.colorCheckBox = new System.Windows.Forms.CheckBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.buttonFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.wordlistTextBox = new System.Windows.Forms.TextBox();
            this.outDirTextBox = new System.Windows.Forms.TextBox();
            this.mainInstructionLabel = new System.Windows.Forms.Label();
            this.strokeDelayLabel = new System.Windows.Forms.Label();
            this.loopDelayLabel = new System.Windows.Forms.Label();
            this.strokeDelayFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.strokeDelayUpDown = new System.Windows.Forms.NumericUpDown();
            this.secondsLabel1 = new System.Windows.Forms.Label();
            this.loopDelayFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.loopDelayUpDown = new System.Windows.Forms.NumericUpDown();
            this.secondsLabel2 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.imageSizeLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.widthHeightUpDown = new System.Windows.Forms.NumericUpDown();
            this.pixelsLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            this.buttonFlowLayoutPanel.SuspendLayout();
            this.strokeDelayFlowLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.strokeDelayUpDown)).BeginInit();
            this.loopDelayFlowLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loopDelayUpDown)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.widthHeightUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.wordlistLabel, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.outDirLabel, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.colorCheckBox, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.browseButton, 2, 2);
            this.tableLayoutPanel.Controls.Add(this.buttonFlowLayoutPanel, 0, 7);
            this.tableLayoutPanel.Controls.Add(this.wordlistTextBox, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.outDirTextBox, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.mainInstructionLabel, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.strokeDelayLabel, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.loopDelayLabel, 0, 5);
            this.tableLayoutPanel.Controls.Add(this.strokeDelayFlowLayoutPanel, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.loopDelayFlowLayoutPanel, 1, 5);
            this.tableLayoutPanel.Controls.Add(this.imageSizeLabel, 0, 6);
            this.tableLayoutPanel.Controls.Add(this.flowLayoutPanel1, 1, 6);
            this.tableLayoutPanel.Location = new System.Drawing.Point(8, 8);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 8;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(472, 398);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // wordlistLabel
            // 
            this.wordlistLabel.AutoSize = true;
            this.wordlistLabel.Location = new System.Drawing.Point(3, 51);
            this.wordlistLabel.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.wordlistLabel.Name = "wordlistLabel";
            this.wordlistLabel.Size = new System.Drawing.Size(51, 13);
            this.wordlistLabel.TabIndex = 1;
            this.wordlistLabel.Text = "&Word list:";
            // 
            // outDirLabel
            // 
            this.outDirLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.outDirLabel.AutoSize = true;
            this.outDirLabel.Location = new System.Drawing.Point(3, 247);
            this.outDirLabel.Name = "outDirLabel";
            this.outDirLabel.Size = new System.Drawing.Size(69, 13);
            this.outDirLabel.TabIndex = 3;
            this.outDirLabel.Text = "&Save to:";
            // 
            // colorCheckBox
            // 
            this.colorCheckBox.AutoSize = true;
            this.colorCheckBox.Location = new System.Drawing.Point(78, 271);
            this.colorCheckBox.Name = "colorCheckBox";
            this.colorCheckBox.Size = new System.Drawing.Size(242, 17);
            this.colorCheckBox.TabIndex = 6;
            this.colorCheckBox.Text = "Make every drawn stroke use a different &color";
            this.colorCheckBox.UseVisualStyleBackColor = true;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(394, 242);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 5;
            this.browseButton.Text = "&Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // buttonFlowLayoutPanel
            // 
            this.buttonFlowLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.SetColumnSpan(this.buttonFlowLayoutPanel, 3);
            this.buttonFlowLayoutPanel.Controls.Add(this.okButton);
            this.buttonFlowLayoutPanel.Controls.Add(this.cancelButton);
            this.buttonFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonFlowLayoutPanel.Location = new System.Drawing.Point(0, 369);
            this.buttonFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.buttonFlowLayoutPanel.Name = "buttonFlowLayoutPanel";
            this.buttonFlowLayoutPanel.Size = new System.Drawing.Size(472, 29);
            this.buttonFlowLayoutPanel.TabIndex = 13;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(394, 3);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(313, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // wordlistTextBox
            // 
            this.tableLayoutPanel.SetColumnSpan(this.wordlistTextBox, 2);
            this.wordlistTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wordlistTextBox.Location = new System.Drawing.Point(78, 51);
            this.wordlistTextBox.Multiline = true;
            this.wordlistTextBox.Name = "wordlistTextBox";
            this.wordlistTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.wordlistTextBox.Size = new System.Drawing.Size(391, 185);
            this.wordlistTextBox.TabIndex = 2;
            this.wordlistTextBox.WordWrap = false;
            // 
            // outDirTextBox
            // 
            this.outDirTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.outDirTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.outDirTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.outDirTextBox.Location = new System.Drawing.Point(78, 243);
            this.outDirTextBox.Name = "outDirTextBox";
            this.outDirTextBox.Size = new System.Drawing.Size(310, 20);
            this.outDirTextBox.TabIndex = 4;
            // 
            // mainInstructionLabel
            // 
            this.mainInstructionLabel.AutoSize = true;
            this.tableLayoutPanel.SetColumnSpan(this.mainInstructionLabel, 3);
            this.mainInstructionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainInstructionLabel.Location = new System.Drawing.Point(3, 0);
            this.mainInstructionLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 9);
            this.mainInstructionLabel.Name = "mainInstructionLabel";
            this.mainInstructionLabel.Size = new System.Drawing.Size(466, 39);
            this.mainInstructionLabel.TabIndex = 0;
            this.mainInstructionLabel.Text = "{0} is a command line program. This window has opened because you have not specif" +
    "ied any command line arguments. Please complete the following fields and click O" +
    "K to generate stroke order guides.";
            // 
            // strokeDelayLabel
            // 
            this.strokeDelayLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.strokeDelayLabel.AutoSize = true;
            this.strokeDelayLabel.Location = new System.Drawing.Point(3, 297);
            this.strokeDelayLabel.Name = "strokeDelayLabel";
            this.strokeDelayLabel.Size = new System.Drawing.Size(69, 13);
            this.strokeDelayLabel.TabIndex = 7;
            this.strokeDelayLabel.Text = "Stroke &delay:";
            // 
            // loopDelayLabel
            // 
            this.loopDelayLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.loopDelayLabel.AutoSize = true;
            this.loopDelayLabel.Location = new System.Drawing.Point(3, 323);
            this.loopDelayLabel.Name = "loopDelayLabel";
            this.loopDelayLabel.Size = new System.Drawing.Size(69, 13);
            this.loopDelayLabel.TabIndex = 9;
            this.loopDelayLabel.Text = "&Loop delay:";
            // 
            // strokeDelayFlowLayoutPanel
            // 
            this.strokeDelayFlowLayoutPanel.AutoSize = true;
            this.strokeDelayFlowLayoutPanel.Controls.Add(this.strokeDelayUpDown);
            this.strokeDelayFlowLayoutPanel.Controls.Add(this.secondsLabel1);
            this.strokeDelayFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.strokeDelayFlowLayoutPanel.Location = new System.Drawing.Point(75, 291);
            this.strokeDelayFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.strokeDelayFlowLayoutPanel.Name = "strokeDelayFlowLayoutPanel";
            this.strokeDelayFlowLayoutPanel.Size = new System.Drawing.Size(316, 26);
            this.strokeDelayFlowLayoutPanel.TabIndex = 8;
            // 
            // strokeDelayUpDown
            // 
            this.strokeDelayUpDown.DecimalPlaces = 1;
            this.strokeDelayUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.strokeDelayUpDown.Location = new System.Drawing.Point(3, 3);
            this.strokeDelayUpDown.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.strokeDelayUpDown.Name = "strokeDelayUpDown";
            this.strokeDelayUpDown.Size = new System.Drawing.Size(69, 20);
            this.strokeDelayUpDown.TabIndex = 0;
            this.strokeDelayUpDown.ThousandsSeparator = true;
            this.strokeDelayUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // secondsLabel1
            // 
            this.secondsLabel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.secondsLabel1.AutoSize = true;
            this.secondsLabel1.Location = new System.Drawing.Point(78, 6);
            this.secondsLabel1.Name = "secondsLabel1";
            this.secondsLabel1.Size = new System.Drawing.Size(47, 13);
            this.secondsLabel1.TabIndex = 1;
            this.secondsLabel1.Text = "seconds";
            // 
            // loopDelayFlowLayoutPanel
            // 
            this.loopDelayFlowLayoutPanel.AutoSize = true;
            this.loopDelayFlowLayoutPanel.Controls.Add(this.loopDelayUpDown);
            this.loopDelayFlowLayoutPanel.Controls.Add(this.secondsLabel2);
            this.loopDelayFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loopDelayFlowLayoutPanel.Location = new System.Drawing.Point(75, 317);
            this.loopDelayFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.loopDelayFlowLayoutPanel.Name = "loopDelayFlowLayoutPanel";
            this.loopDelayFlowLayoutPanel.Size = new System.Drawing.Size(316, 26);
            this.loopDelayFlowLayoutPanel.TabIndex = 10;
            // 
            // loopDelayUpDown
            // 
            this.loopDelayUpDown.DecimalPlaces = 1;
            this.loopDelayUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.loopDelayUpDown.Location = new System.Drawing.Point(3, 3);
            this.loopDelayUpDown.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.loopDelayUpDown.Name = "loopDelayUpDown";
            this.loopDelayUpDown.Size = new System.Drawing.Size(69, 20);
            this.loopDelayUpDown.TabIndex = 0;
            this.loopDelayUpDown.ThousandsSeparator = true;
            this.loopDelayUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // secondsLabel2
            // 
            this.secondsLabel2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.secondsLabel2.AutoSize = true;
            this.secondsLabel2.Location = new System.Drawing.Point(78, 6);
            this.secondsLabel2.Name = "secondsLabel2";
            this.secondsLabel2.Size = new System.Drawing.Size(47, 13);
            this.secondsLabel2.TabIndex = 1;
            this.secondsLabel2.Text = "seconds";
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Select the folder in which stroke order guides will be saved.";
            // 
            // imageSizeLabel
            // 
            this.imageSizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.imageSizeLabel.AutoSize = true;
            this.imageSizeLabel.Location = new System.Drawing.Point(3, 349);
            this.imageSizeLabel.Name = "imageSizeLabel";
            this.imageSizeLabel.Size = new System.Drawing.Size(69, 13);
            this.imageSizeLabel.TabIndex = 11;
            this.imageSizeLabel.Text = "Image si&ze:";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.widthHeightUpDown);
            this.flowLayoutPanel1.Controls.Add(this.pixelsLabel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(75, 343);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(316, 26);
            this.flowLayoutPanel1.TabIndex = 12;
            // 
            // widthHeightUpDown
            // 
            this.widthHeightUpDown.Location = new System.Drawing.Point(3, 3);
            this.widthHeightUpDown.Maximum = new decimal(new int[] {
            8192,
            0,
            0,
            0});
            this.widthHeightUpDown.Name = "widthHeightUpDown";
            this.widthHeightUpDown.Size = new System.Drawing.Size(69, 20);
            this.widthHeightUpDown.TabIndex = 0;
            this.widthHeightUpDown.Value = new decimal(new int[] {
            109,
            0,
            0,
            0});
            // 
            // pixelsLabel
            // 
            this.pixelsLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pixelsLabel.AutoSize = true;
            this.pixelsLabel.Location = new System.Drawing.Point(78, 6);
            this.pixelsLabel.Name = "pixelsLabel";
            this.pixelsLabel.Size = new System.Drawing.Size(18, 13);
            this.pixelsLabel.TabIndex = 1;
            this.pixelsLabel.Text = "px";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(484, 411);
            this.Controls.Add(this.tableLayoutPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "{0} {1} GUI";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.buttonFlowLayoutPanel.ResumeLayout(false);
            this.strokeDelayFlowLayoutPanel.ResumeLayout(false);
            this.strokeDelayFlowLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.strokeDelayUpDown)).EndInit();
            this.loopDelayFlowLayoutPanel.ResumeLayout(false);
            this.loopDelayFlowLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loopDelayUpDown)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.widthHeightUpDown)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Label wordlistLabel;
		private System.Windows.Forms.Label outDirLabel;
		private System.Windows.Forms.Button browseButton;
		private System.Windows.Forms.FlowLayoutPanel buttonFlowLayoutPanel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label mainInstructionLabel;
		private System.Windows.Forms.Label strokeDelayLabel;
		private System.Windows.Forms.Label loopDelayLabel;
		private System.Windows.Forms.FlowLayoutPanel strokeDelayFlowLayoutPanel;
		private System.Windows.Forms.Label secondsLabel1;
		private System.Windows.Forms.FlowLayoutPanel loopDelayFlowLayoutPanel;
		private System.Windows.Forms.Label secondsLabel2;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		internal System.Windows.Forms.CheckBox colorCheckBox;
		internal System.Windows.Forms.TextBox wordlistTextBox;
		internal System.Windows.Forms.TextBox outDirTextBox;
		internal System.Windows.Forms.NumericUpDown strokeDelayUpDown;
		internal System.Windows.Forms.NumericUpDown loopDelayUpDown;
        private System.Windows.Forms.Label imageSizeLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        internal System.Windows.Forms.NumericUpDown widthHeightUpDown;
        private System.Windows.Forms.Label pixelsLabel;
    }
}