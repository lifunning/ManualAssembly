namespace ManualAssembly
{
    partial class Screwdriver3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Screwdriver3));
            this.bCancel = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.cboScrew3Port = new System.Windows.Forms.ComboBox();
            this.cboScrew3Baut = new System.Windows.Forms.ComboBox();
            this.cboScrew3Data = new System.Windows.Forms.ComboBox();
            this.cboScrew3Stop = new System.Windows.Forms.ComboBox();
            this.cboScrew3Parity = new System.Windows.Forms.ComboBox();
            this.lbldatabit = new System.Windows.Forms.Label();
            this.lblStopbit = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblparity = new System.Windows.Forms.Label();
            this.lblbaudrate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bCancel
            // 
            this.bCancel.Image = ((System.Drawing.Image)(resources.GetObject("bCancel.Image")));
            this.bCancel.Location = new System.Drawing.Point(321, 283);
            this.bCancel.Margin = new System.Windows.Forms.Padding(2);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(91, 41);
            this.bCancel.TabIndex = 130;
            this.bCancel.Text = "Cancel ";
            this.bCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bOK
            // 
            this.bOK.Image = ((System.Drawing.Image)(resources.GetObject("bOK.Image")));
            this.bOK.Location = new System.Drawing.Point(208, 283);
            this.bOK.Margin = new System.Windows.Forms.Padding(2);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(95, 41);
            this.bOK.TabIndex = 129;
            this.bOK.Text = "OK";
            this.bOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // cboScrew3Port
            // 
            this.cboScrew3Port.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboScrew3Port.FormattingEnabled = true;
            this.cboScrew3Port.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12",
            "COM13",
            "COM14",
            "COM15",
            "COM16",
            "COM17",
            "COM18",
            "COM19",
            "COM20"});
            this.cboScrew3Port.Location = new System.Drawing.Point(124, 96);
            this.cboScrew3Port.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboScrew3Port.Name = "cboScrew3Port";
            this.cboScrew3Port.Size = new System.Drawing.Size(79, 20);
            this.cboScrew3Port.TabIndex = 127;
            this.cboScrew3Port.Text = "COM1";
            // 
            // cboScrew3Baut
            // 
            this.cboScrew3Baut.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboScrew3Baut.FormattingEnabled = true;
            this.cboScrew3Baut.Items.AddRange(new object[] {
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "115200"});
            this.cboScrew3Baut.Location = new System.Drawing.Point(124, 143);
            this.cboScrew3Baut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboScrew3Baut.Name = "cboScrew3Baut";
            this.cboScrew3Baut.Size = new System.Drawing.Size(79, 20);
            this.cboScrew3Baut.TabIndex = 128;
            this.cboScrew3Baut.Text = "9600";
            // 
            // cboScrew3Data
            // 
            this.cboScrew3Data.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboScrew3Data.FormattingEnabled = true;
            this.cboScrew3Data.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cboScrew3Data.Location = new System.Drawing.Point(321, 95);
            this.cboScrew3Data.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboScrew3Data.Name = "cboScrew3Data";
            this.cboScrew3Data.Size = new System.Drawing.Size(79, 20);
            this.cboScrew3Data.TabIndex = 123;
            this.cboScrew3Data.Text = "8";
            // 
            // cboScrew3Stop
            // 
            this.cboScrew3Stop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboScrew3Stop.FormattingEnabled = true;
            this.cboScrew3Stop.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.cboScrew3Stop.Location = new System.Drawing.Point(321, 143);
            this.cboScrew3Stop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboScrew3Stop.Name = "cboScrew3Stop";
            this.cboScrew3Stop.Size = new System.Drawing.Size(79, 20);
            this.cboScrew3Stop.TabIndex = 125;
            this.cboScrew3Stop.Text = "1";
            // 
            // cboScrew3Parity
            // 
            this.cboScrew3Parity.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboScrew3Parity.FormattingEnabled = true;
            this.cboScrew3Parity.Items.AddRange(new object[] {
            "NONE",
            "ODD",
            "EVEN"});
            this.cboScrew3Parity.Location = new System.Drawing.Point(124, 193);
            this.cboScrew3Parity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboScrew3Parity.Name = "cboScrew3Parity";
            this.cboScrew3Parity.Size = new System.Drawing.Size(79, 20);
            this.cboScrew3Parity.TabIndex = 126;
            this.cboScrew3Parity.Text = "NONE";
            // 
            // lbldatabit
            // 
            this.lbldatabit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbldatabit.AutoSize = true;
            this.lbldatabit.Location = new System.Drawing.Point(245, 102);
            this.lbldatabit.Name = "lbldatabit";
            this.lbldatabit.Size = new System.Drawing.Size(41, 12);
            this.lbldatabit.TabIndex = 122;
            this.lbldatabit.Text = "数据位";
            // 
            // lblStopbit
            // 
            this.lblStopbit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStopbit.AutoSize = true;
            this.lblStopbit.Location = new System.Drawing.Point(245, 152);
            this.lblStopbit.Name = "lblStopbit";
            this.lblStopbit.Size = new System.Drawing.Size(41, 12);
            this.lblStopbit.TabIndex = 118;
            this.lblStopbit.Text = "停止位";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 120;
            this.label1.Text = "端口";
            // 
            // lblparity
            // 
            this.lblparity.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblparity.AutoSize = true;
            this.lblparity.Location = new System.Drawing.Point(60, 195);
            this.lblparity.Name = "lblparity";
            this.lblparity.Size = new System.Drawing.Size(41, 12);
            this.lblparity.TabIndex = 119;
            this.lblparity.Text = "校验位";
            // 
            // lblbaudrate
            // 
            this.lblbaudrate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblbaudrate.AutoSize = true;
            this.lblbaudrate.Location = new System.Drawing.Point(60, 146);
            this.lblbaudrate.Name = "lblbaudrate";
            this.lblbaudrate.Size = new System.Drawing.Size(41, 12);
            this.lblbaudrate.TabIndex = 121;
            this.lblbaudrate.Text = "波特率";
            // 
            // Screwdriver3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 418);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.cboScrew3Port);
            this.Controls.Add(this.cboScrew3Baut);
            this.Controls.Add(this.cboScrew3Data);
            this.Controls.Add(this.cboScrew3Stop);
            this.Controls.Add(this.cboScrew3Parity);
            this.Controls.Add(this.lbldatabit);
            this.Controls.Add(this.lblStopbit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblparity);
            this.Controls.Add(this.lblbaudrate);
            this.Name = "Screwdriver3";
            this.Text = "Screwdriver3";
            this.Load += new System.EventHandler(this.Screwdriver3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.ComboBox cboScrew3Port;
        private System.Windows.Forms.ComboBox cboScrew3Baut;
        private System.Windows.Forms.ComboBox cboScrew3Data;
        private System.Windows.Forms.ComboBox cboScrew3Stop;
        private System.Windows.Forms.ComboBox cboScrew3Parity;
        private System.Windows.Forms.Label lbldatabit;
        private System.Windows.Forms.Label lblStopbit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblparity;
        private System.Windows.Forms.Label lblbaudrate;
    }
}