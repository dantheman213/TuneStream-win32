namespace TuneStream_Win32
{
    partial class FrameMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnStart = new Button();
            radioButtonSender = new RadioButton();
            radioButtonReceiver = new RadioButton();
            btnStop = new Button();
            labelStatus = new Label();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Location = new Point(487, 343);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(112, 34);
            btnStart.TabIndex = 0;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // radioButtonSender
            // 
            radioButtonSender.AutoSize = true;
            radioButtonSender.Location = new Point(495, 409);
            radioButtonSender.Name = "radioButtonSender";
            radioButtonSender.Size = new Size(92, 29);
            radioButtonSender.TabIndex = 1;
            radioButtonSender.TabStop = true;
            radioButtonSender.Text = "Sender";
            radioButtonSender.UseVisualStyleBackColor = true;
            // 
            // radioButtonReceiver
            // 
            radioButtonReceiver.AutoSize = true;
            radioButtonReceiver.Checked = true;
            radioButtonReceiver.Location = new Point(605, 409);
            radioButtonReceiver.Name = "radioButtonReceiver";
            radioButtonReceiver.Size = new Size(101, 29);
            radioButtonReceiver.TabIndex = 2;
            radioButtonReceiver.TabStop = true;
            radioButtonReceiver.Text = "Receiver";
            radioButtonReceiver.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            btnStop.Location = new Point(605, 343);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(112, 34);
            btnStop.TabIndex = 3;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += btnStop_Click;
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Location = new Point(537, 257);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(134, 25);
            labelStatus.TabIndex = 4;
            labelStatus.Text = "Status: Standby";
            // 
            // FrameMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1256, 788);
            Controls.Add(labelStatus);
            Controls.Add(btnStop);
            Controls.Add(radioButtonReceiver);
            Controls.Add(radioButtonSender);
            Controls.Add(btnStart);
            Name = "FrameMain";
            Text = "TuneStream";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStart;
        private RadioButton radioButtonSender;
        private RadioButton radioButtonReceiver;
        private Button btnStop;
        private Label labelStatus;
    }
}
