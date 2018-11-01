namespace ManualAssembly
{
    partial class Screwdriver1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Screwdriver1));
            this.cboScrew1Port = new System.Windows.Forms.ComboBox();
            this.cboScrew1Baut = new System.Windows.Forms.ComboBox();
            this.cboScrew1Data = new System.Windows.Forms.ComboBox();
            this.cboScrew1Stop = new System.Windows.Forms.ComboBox();
            this.cboScrew1Parity = new System.Windows.Forms.ComboBox();
            this.lbldatabit = new System.Windows.Forms.Label();
            this.lblStopbit = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblparity = new System.Windows.Forms.Label();
            this.lblbaudrate = new System.Windows.Forms.Label();
            this.bCancel = new System.Windows.Forms.Button();
            this.bOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboScrew1Port
            // 
            this.cboScrew1Port.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboScrew1Port.FormattingEnabled = true;
            this.cboScrew1Port.Items.AddRange(new object[] {
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
            this.cboScrew1Port.Location = new System.Drawing.Point(125, 123);
            this.cboScrew1Port.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboScrew1Port.Name = "cboScrew1Port";
            this.cboScrew1Port.Size = new System.Drawing.Size(79, 20);
            this.cboScrew1Port.TabIndex = 113;
            this.cboScrew1Port.Text = "COM1";
            // 
            // cboScrew1Baut
            // 
            this.cboScrew1Baut.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboScrew1Baut.FormattingEnabled = true;
            this.cboScrew1Baut.Items.AddRange(new object[] {
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "115200"});
            this.cboScrew1Baut.Location = new System.Drawing.Point(125, 170);
            this.cboScrew1Baut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboScrew1Baut.Name = "cboScrew1Baut";
            this.cboScrew1Baut.Size = new System.Drawing.Size(79, 20);
            this.cboScrew1Baut.TabIndex = 114;
            this.cboScrew1Baut.Text = "9600";
            // 
            // cboScrew1Data
            // 
            this.cboScrew1Data.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboScrew1Data.FormattingEnabled = true;
            this.cboScrew1Data.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cboScrew1Data.Location = new System.Drawing.Point(322, 122);
            this.cboScrew1Data.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboScrew1Data.Name = "cboScrew1Data";
            this.cboScrew1Data.Size = new System.Drawing.Size(79, 20);
            this.cboScrew1Data.TabIndex = 110;
            this.cboScrew1Data.Text = "8";
            // 
            // cboScrew1Stop
            // 
            this.cboScrew1Stop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboScrew1Stop.FormattingEnabled = true;
            this.cboScrew1Stop.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.cboScrew1Stop.Location = new System.Drawing.Point(322, 170);
            this.cboScrew1Stop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboScrew1Stop.Name = "cboScrew1Stop";
            this.cboScrew1Stop.Size = new System.Drawing.Size(79, 20);
            this.cboScrew1Stop.TabIndex = 111;
            this.cboScrew1Stop.Text = "1";
            // 
            // cboScrew1Parity
            // 
            this.cboScrew1Parity.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboScrew1Parity.FormattingEnabled = true;
            this.cboScrew1Parity.Items.AddRange(new object[] {
            "NONE",
            "ODD",
            "EVEN"});
            this.cboScrew1Parity.Location = new System.Drawing.Point(125, 220);
            this.cboScrew1Parity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboScrew1Parity.Name = "cboScrew1Parity";
            this.cboScrew1Parity.Size = new System.Drawing.Size(79, 20);
            this.cboScrew1Parity.TabIndex = 112;
            this.cboScrew1Parity.Text = "NONE";
            // 
            // lbldatabit
            // 
            this.lbldatabit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbldatabit.AutoSize = true;
            this.lbldatabit.Location = new System.Drawing.Point(246, 129);
            this.lbldatabit.Name = "lbldatabit";
            this.lbldatabit.Size = new System.Drawing.Size(41, 12);
            this.lbldatabit.TabIndex = 109;
            this.lbldatabit.Text = "数据位";
            // 
            // lblStopbit
            // 
            this.lblStopbit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblStopbit.AutoSize = true;
            this.lblStopbit.Location = new System.Drawing.Point(246, 179);
            this.lblStopbit.Name = "lblStopbit";
            this.lblStopbit.Size = new System.Drawing.Size(41, 12);
            this.lblStopbit.TabIndex = 105;
            this.lblStopbit.Text = "停止位";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 107;
            this.label1.Text = "端口";
            // 
            // lblparity
            // 
            this.lblparity.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblparity.AutoSize = true;
            this.lblparity.Location = new System.Drawing.Point(61, 222);
            this.lblparity.Name = "lblparity";
            this.lblparity.Size = new System.Drawing.Size(41, 12);
            this.lblparity.TabIndex = 106;
            this.lblparity.Text = "校验位";
            // 
            // lblbaudrate
            // 
            this.lblbaudrate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblbaudrate.AutoSize = true;
            this.lblbaudrate.Location = new System.Drawing.Point(61, 173);
            this.lblbaudrate.Name = "lblbaudrate";
            this.lblbaudrate.Size = new System.Drawing.Size(41, 12);
            this.lblbaudrate.TabIndex = 108;
            this.lblbaudrate.Text = "波特率";
            // 
            // bCancel
            // 
            this.bCancel.Image = ((System.Drawing.Image)(resources.GetObject("bCancel.Image")));
            this.bCancel.Location = new System.Drawing.Point(322, 310);
            this.bCancel.Margin = new System.Windows.Forms.Padding(2);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(91, 41);
            this.bCancel.TabIndex = 116;
            this.bCancel.Text = "Cancel ";
            this.bCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bOK
            // 
            this.bOK.Image = ((System.Drawing.Image)(resources.GetObject("bOK.Image")));
            this.bOK.Location = new System.Drawing.Point(209, 310);
            this.bOK.Margin = new System.Windows.Forms.Padding(2);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(95, 41);
            this.bOK.TabIndex = 115;
            this.bOK.Text = "OK";
            this.bOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // Screwdriver1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 418);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.cboScrew1Port);
            this.Controls.Add(this.cboScrew1Baut);
            this.Controls.Add(this.cboScrew1Data);
            this.Controls.Add(this.cboScrew1Stop);
            this.Controls.Add(this.cboScrew1Parity);
            this.Controls.Add(this.lbldatabit);
            this.Controls.Add(this.lblStopbit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblparity);
            this.Controls.Add(this.lblbaudrate);
            this.Name = "Screwdriver1";
            this.Text = "Screwdriver1";
            this.Load += new System.EventHandler(this.Screwdriver1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboScrew1Port;
        private System.Windows.Forms.ComboBox cboScrew1Baut;
        private System.Windows.Forms.ComboBox cboScrew1Data;
        private System.Windows.Forms.ComboBox cboScrew1Stop;
        private System.Windows.Forms.ComboBox cboScrew1Parity;
        private System.Windows.Forms.Label lbldatabit;
        private System.Windows.Forms.Label lblStopbit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblparity;
        private System.Windows.Forms.Label lblbaudrate;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bOK;
    }
}