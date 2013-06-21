namespace PowerPointController
{
	partial class PowerPointController
	{
		/// <summary>
		/// 必要なデザイナー変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナーで生成されたコード

		/// <summary>
		/// デザイナー サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディターで変更しないでください。
		/// </summary>
		private void InitializeComponent()
		{
			this.openPowerPointDialog = new System.Windows.Forms.OpenFileDialog();
			this.openPowerPointFileButton = new System.Windows.Forms.Button();
			this.powerPointFilePathTextBox = new System.Windows.Forms.TextBox();
			this.startButton = new System.Windows.Forms.Button();
			this.nextButton = new System.Windows.Forms.Button();
			this.previousButton = new System.Windows.Forms.Button();
			this.restartButton = new System.Windows.Forms.Button();
			this.endButton = new System.Windows.Forms.Button();
			this.IPAddresslabel = new System.Windows.Forms.Label();
			this.IPAddressTextBox = new System.Windows.Forms.TextBox();
			this.IPAddressButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// openPowerPointDialog
			// 
			this.openPowerPointDialog.Filter = "PowerPointファイル(*.ppt;*.pptx)|*.ppt;*.pptx";
			this.openPowerPointDialog.Title = "パワーポイントファイルを選択してください";
			// 
			// openPowerPointFileButton
			// 
			this.openPowerPointFileButton.Location = new System.Drawing.Point(392, 24);
			this.openPowerPointFileButton.Name = "openPowerPointFileButton";
			this.openPowerPointFileButton.Size = new System.Drawing.Size(75, 23);
			this.openPowerPointFileButton.TabIndex = 0;
			this.openPowerPointFileButton.Text = "参照";
			this.openPowerPointFileButton.UseVisualStyleBackColor = true;
			this.openPowerPointFileButton.Click += new System.EventHandler(this.openPowerPointFileButton_Click);
			// 
			// powerPointFilePathTextBox
			// 
			this.powerPointFilePathTextBox.Location = new System.Drawing.Point(16, 24);
			this.powerPointFilePathTextBox.Name = "powerPointFilePathTextBox";
			this.powerPointFilePathTextBox.ReadOnly = true;
			this.powerPointFilePathTextBox.Size = new System.Drawing.Size(368, 19);
			this.powerPointFilePathTextBox.TabIndex = 1;
			// 
			// startButton
			// 
			this.startButton.Font = new System.Drawing.Font("MS UI Gothic", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.startButton.Location = new System.Drawing.Point(168, 64);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(152, 64);
			this.startButton.TabIndex = 2;
			this.startButton.Text = "開始";
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			// 
			// nextButton
			// 
			this.nextButton.Location = new System.Drawing.Point(288, 160);
			this.nextButton.Name = "nextButton";
			this.nextButton.Size = new System.Drawing.Size(75, 23);
			this.nextButton.TabIndex = 3;
			this.nextButton.Text = "次へ";
			this.nextButton.UseVisualStyleBackColor = true;
			this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
			// 
			// previousButton
			// 
			this.previousButton.Location = new System.Drawing.Point(120, 160);
			this.previousButton.Name = "previousButton";
			this.previousButton.Size = new System.Drawing.Size(75, 23);
			this.previousButton.TabIndex = 4;
			this.previousButton.Text = "前へ";
			this.previousButton.UseVisualStyleBackColor = true;
			this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
			// 
			// restartButton
			// 
			this.restartButton.Location = new System.Drawing.Point(400, 200);
			this.restartButton.Name = "restartButton";
			this.restartButton.Size = new System.Drawing.Size(75, 23);
			this.restartButton.TabIndex = 5;
			this.restartButton.Text = "再スタート";
			this.restartButton.UseVisualStyleBackColor = true;
			this.restartButton.Click += new System.EventHandler(this.restartButton_Click);
			// 
			// endButton
			// 
			this.endButton.Location = new System.Drawing.Point(400, 232);
			this.endButton.Name = "endButton";
			this.endButton.Size = new System.Drawing.Size(75, 23);
			this.endButton.TabIndex = 6;
			this.endButton.Text = "終了";
			this.endButton.UseVisualStyleBackColor = true;
			this.endButton.Click += new System.EventHandler(this.endButton_Click);
			// 
			// IPAddresslabel
			// 
			this.IPAddresslabel.AutoSize = true;
			this.IPAddresslabel.Location = new System.Drawing.Point(8, 208);
			this.IPAddresslabel.Name = "IPAddresslabel";
			this.IPAddresslabel.Size = new System.Drawing.Size(57, 12);
			this.IPAddresslabel.TabIndex = 7;
			this.IPAddresslabel.Text = "IPAddress";
			// 
			// IPAddressTextBox
			// 
			this.IPAddressTextBox.Location = new System.Drawing.Point(8, 224);
			this.IPAddressTextBox.Name = "IPAddressTextBox";
			this.IPAddressTextBox.ReadOnly = true;
			this.IPAddressTextBox.Size = new System.Drawing.Size(100, 19);
			this.IPAddressTextBox.TabIndex = 8;
			// 
			// IPAddressButton
			// 
			this.IPAddressButton.Location = new System.Drawing.Point(112, 224);
			this.IPAddressButton.Name = "IPAddressButton";
			this.IPAddressButton.Size = new System.Drawing.Size(75, 23);
			this.IPAddressButton.TabIndex = 9;
			this.IPAddressButton.Text = "IP取得";
			this.IPAddressButton.UseVisualStyleBackColor = true;
			this.IPAddressButton.Click += new System.EventHandler(this.IPAddressButton_Click);
			// 
			// PowerPointController
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(487, 262);
			this.Controls.Add(this.IPAddressButton);
			this.Controls.Add(this.IPAddressTextBox);
			this.Controls.Add(this.IPAddresslabel);
			this.Controls.Add(this.endButton);
			this.Controls.Add(this.restartButton);
			this.Controls.Add(this.previousButton);
			this.Controls.Add(this.nextButton);
			this.Controls.Add(this.startButton);
			this.Controls.Add(this.powerPointFilePathTextBox);
			this.Controls.Add(this.openPowerPointFileButton);
			this.Name = "PowerPointController";
			this.Text = "PowerPointController";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PowerPointController_FormClosing);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog openPowerPointDialog;
		private System.Windows.Forms.Button openPowerPointFileButton;
		private System.Windows.Forms.TextBox powerPointFilePathTextBox;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Button nextButton;
		private System.Windows.Forms.Button previousButton;
		private System.Windows.Forms.Button restartButton;
		private System.Windows.Forms.Button endButton;
		private System.Windows.Forms.Label IPAddresslabel;
		private System.Windows.Forms.TextBox IPAddressTextBox;
		private System.Windows.Forms.Button IPAddressButton;
	}
}

