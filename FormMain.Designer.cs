namespace TuneStream_Win32
{
    partial class FormMain
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
            serverModeButton = new Button();
            clientModeButton = new Button();
            connectButton = new Button();
            deviceListComboBox = new ComboBox();
            label1 = new Label();
            labelStatus = new Label();
            SuspendLayout();
            // 
            // serverModeButton
            // 
            serverModeButton.Location = new Point(12, 28);
            serverModeButton.Name = "serverModeButton";
            serverModeButton.Size = new Size(112, 34);
            serverModeButton.TabIndex = 0;
            serverModeButton.Text = "Start Server";
            serverModeButton.UseVisualStyleBackColor = true;
            serverModeButton.Click += ServerModeButton_Click;
            // 
            // clientModeButton
            // 
            clientModeButton.Location = new Point(353, 28);
            clientModeButton.Name = "clientModeButton";
            clientModeButton.Size = new Size(227, 34);
            clientModeButton.TabIndex = 1;
            clientModeButton.Text = "Scan for Devices";
            clientModeButton.UseVisualStyleBackColor = true;
            clientModeButton.Click += ClientModeButton_Click;
            // 
            // connectButton
            // 
            connectButton.Location = new Point(629, 107);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(159, 34);
            connectButton.TabIndex = 2;
            connectButton.Text = "Connect";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += ConnectButton_Click;
            // 
            // deviceListComboBox
            // 
            deviceListComboBox.FormattingEnabled = true;
            deviceListComboBox.Location = new Point(353, 68);
            deviceListComboBox.Name = "deviceListComboBox";
            deviceListComboBox.Size = new Size(435, 33);
            deviceListComboBox.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 247);
            label1.Name = "label1";
            label1.Size = new Size(69, 25);
            label1.TabIndex = 4;
            label1.Text = "Status: ";
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Location = new Point(87, 247);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(66, 25);
            labelStatus.TabIndex = 5;
            labelStatus.Text = "Standy";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 468);
            Controls.Add(labelStatus);
            Controls.Add(label1);
            Controls.Add(deviceListComboBox);
            Controls.Add(connectButton);
            Controls.Add(clientModeButton);
            Controls.Add(serverModeButton);
            Name = "FormMain";
            Text = "FormMain";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button serverModeButton;
        private Button clientModeButton;
        private Button connectButton;
        private ComboBox deviceListComboBox;
        private Label label1;
        private Label labelStatus;
    }
}