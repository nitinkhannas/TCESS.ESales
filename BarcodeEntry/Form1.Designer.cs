namespace BarcodeEntry
{
	partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbl_BarCode = new System.Windows.Forms.Label();
            this.lbl_TotalCount = new System.Windows.Forms.Label();
            this.lbl_TotalCountMsg = new System.Windows.Forms.Label();
            this.lbl_Barcodemsg = new System.Windows.Forms.Label();
            this.lbl_TruckNoMsg = new System.Windows.Forms.Label();
            this.lbl_TimeMsg = new System.Windows.Forms.Label();
            this.Panel_BarCode = new System.Windows.Forms.Panel();
            this.lbl_TruckTracking = new System.Windows.Forms.Label();
            this.lbl_RecentTruck = new System.Windows.Forms.Label();
            this.lbl_Time = new System.Windows.Forms.Label();
            this.lbl_truckno = new System.Windows.Forms.Label();
            this.txt_BarCode = new System.Windows.Forms.TextBox();
            this.lblpost = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.Panel_BarCode.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1003, 312);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lbl_BarCode
            // 
            this.lbl_BarCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_BarCode.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_BarCode.ForeColor = System.Drawing.Color.Black;
            this.lbl_BarCode.Location = new System.Drawing.Point(3, 216);
            this.lbl_BarCode.Name = "lbl_BarCode";
            this.lbl_BarCode.Size = new System.Drawing.Size(147, 26);
            this.lbl_BarCode.TabIndex = 1;
            this.lbl_BarCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_TotalCount
            // 
            this.lbl_TotalCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_TotalCount.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TotalCount.Location = new System.Drawing.Point(379, 90);
            this.lbl_TotalCount.Name = "lbl_TotalCount";
            this.lbl_TotalCount.Size = new System.Drawing.Size(152, 26);
            this.lbl_TotalCount.TabIndex = 4;
            this.lbl_TotalCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_TotalCountMsg
            // 
            this.lbl_TotalCountMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_TotalCountMsg.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TotalCountMsg.Location = new System.Drawing.Point(144, 90);
            this.lbl_TotalCountMsg.Name = "lbl_TotalCountMsg";
            this.lbl_TotalCountMsg.Size = new System.Drawing.Size(236, 26);
            this.lbl_TotalCountMsg.TabIndex = 3;
            this.lbl_TotalCountMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Barcodemsg
            // 
            this.lbl_Barcodemsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Barcodemsg.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Barcodemsg.Location = new System.Drawing.Point(3, 190);
            this.lbl_Barcodemsg.Name = "lbl_Barcodemsg";
            this.lbl_Barcodemsg.Size = new System.Drawing.Size(147, 26);
            this.lbl_Barcodemsg.TabIndex = 5;
            this.lbl_Barcodemsg.Text = "BarCode ID";
            this.lbl_Barcodemsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_TruckNoMsg
            // 
            this.lbl_TruckNoMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_TruckNoMsg.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TruckNoMsg.Location = new System.Drawing.Point(186, 190);
            this.lbl_TruckNoMsg.Name = "lbl_TruckNoMsg";
            this.lbl_TruckNoMsg.Size = new System.Drawing.Size(163, 26);
            this.lbl_TruckNoMsg.TabIndex = 6;
            this.lbl_TruckNoMsg.Text = "Truck No";
            this.lbl_TruckNoMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_TimeMsg
            // 
            this.lbl_TimeMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_TimeMsg.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TimeMsg.Location = new System.Drawing.Point(379, 190);
            this.lbl_TimeMsg.Name = "lbl_TimeMsg";
            this.lbl_TimeMsg.Size = new System.Drawing.Size(152, 26);
            this.lbl_TimeMsg.TabIndex = 7;
            this.lbl_TimeMsg.Text = "Time";
            this.lbl_TimeMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Panel_BarCode
            // 
            this.Panel_BarCode.AutoSize = true;
            this.Panel_BarCode.BackColor = System.Drawing.Color.White;
            this.Panel_BarCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_BarCode.Controls.Add(this.lblpost);
            this.Panel_BarCode.Controls.Add(this.txt_BarCode);
            this.Panel_BarCode.Controls.Add(this.lbl_TruckTracking);
            this.Panel_BarCode.Controls.Add(this.lbl_RecentTruck);
            this.Panel_BarCode.Controls.Add(this.lbl_Time);
            this.Panel_BarCode.Controls.Add(this.lbl_truckno);
            this.Panel_BarCode.Controls.Add(this.lbl_TimeMsg);
            this.Panel_BarCode.Controls.Add(this.lbl_TruckNoMsg);
            this.Panel_BarCode.Controls.Add(this.lbl_Barcodemsg);
            this.Panel_BarCode.Controls.Add(this.lbl_TotalCountMsg);
            this.Panel_BarCode.Controls.Add(this.lbl_TotalCount);
            this.Panel_BarCode.Controls.Add(this.lbl_BarCode);
            this.Panel_BarCode.Location = new System.Drawing.Point(239, 320);
            this.Panel_BarCode.Name = "Panel_BarCode";
            this.Panel_BarCode.Size = new System.Drawing.Size(559, 268);
            this.Panel_BarCode.TabIndex = 5;
            // 
            // lbl_TruckTracking
            // 
            this.lbl_TruckTracking.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TruckTracking.Location = new System.Drawing.Point(86, 14);
            this.lbl_TruckTracking.Name = "lbl_TruckTracking";
            this.lbl_TruckTracking.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_TruckTracking.Size = new System.Drawing.Size(387, 48);
            this.lbl_TruckTracking.TabIndex = 12;
            this.lbl_TruckTracking.Text = "TRUCK TRACKING SYSTEM";
            // 
            // lbl_RecentTruck
            // 
            this.lbl_RecentTruck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_RecentTruck.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_RecentTruck.Location = new System.Drawing.Point(144, 139);
            this.lbl_RecentTruck.Name = "lbl_RecentTruck";
            this.lbl_RecentTruck.Size = new System.Drawing.Size(236, 26);
            this.lbl_RecentTruck.TabIndex = 11;
            this.lbl_RecentTruck.Text = "Recent Truck No";
            // 
            // lbl_Time
            // 
            this.lbl_Time.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Time.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Time.Location = new System.Drawing.Point(379, 216);
            this.lbl_Time.Name = "lbl_Time";
            this.lbl_Time.Size = new System.Drawing.Size(152, 26);
            this.lbl_Time.TabIndex = 10;
            this.lbl_Time.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_truckno
            // 
            this.lbl_truckno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_truckno.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_truckno.Location = new System.Drawing.Point(186, 216);
            this.lbl_truckno.Name = "lbl_truckno";
            this.lbl_truckno.Size = new System.Drawing.Size(163, 26);
            this.lbl_truckno.TabIndex = 9;
            this.lbl_truckno.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_BarCode
            // 
            this.txt_BarCode.BackColor = System.Drawing.Color.White;
            this.txt_BarCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_BarCode.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_BarCode.ForeColor = System.Drawing.Color.Black;
            this.txt_BarCode.Location = new System.Drawing.Point(521, 14);
            this.txt_BarCode.MaxLength = 9;
            this.txt_BarCode.Name = "txt_BarCode";
            this.txt_BarCode.Size = new System.Drawing.Size(10, 28);
            this.txt_BarCode.TabIndex = 14;
            // 
            // lblpost
            // 
            this.lblpost.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpost.Location = new System.Drawing.Point(186, 45);
            this.lblpost.Name = "lblpost";
            this.lblpost.Size = new System.Drawing.Size(287, 33);
            this.lblpost.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(980, 600);
            this.Controls.Add(this.Panel_BarCode);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BarCode Reader";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.Panel_BarCode.ResumeLayout(false);
            this.Panel_BarCode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lbl_BarCode;
		private System.Windows.Forms.Label lbl_TotalCount;
		private System.Windows.Forms.Label lbl_TotalCountMsg;
		private System.Windows.Forms.Label lbl_Barcodemsg;
		private System.Windows.Forms.Label lbl_TruckNoMsg;
		private System.Windows.Forms.Label lbl_TimeMsg;
		private System.Windows.Forms.Panel Panel_BarCode;
		private System.Windows.Forms.Label lbl_Time;
        private System.Windows.Forms.Label lbl_truckno;
        private System.Windows.Forms.Label lbl_TruckTracking;
        private System.Windows.Forms.Label lbl_RecentTruck;
        private System.Windows.Forms.TextBox txt_BarCode;
        private System.Windows.Forms.Label lblpost;
	}
}

