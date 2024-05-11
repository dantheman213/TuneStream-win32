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
            SuspendLayout();
            // 
            // serverModeButton
            // 
            serverModeButton.Location = new Point(99, 28);
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
            connectButton.Location = new Point(306, 191);
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
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 254);
            Controls.Add(deviceListComboBox);
            Controls.Add(connectButton);
            Controls.Add(clientModeButton);
            Controls.Add(serverModeButton);
            Name = "FormMain";
            Text = "FormMain";
            ResumeLayout(false);
        }

        #endregion

        private Button serverModeButton;
        private Button clientModeButton;
        private Button connectButton;
        private ComboBox deviceListComboBox;
    }
}