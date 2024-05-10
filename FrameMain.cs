using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using NAudio.Wave;
using System.Reflection.PortableExecutable;

namespace TuneStream_Win32
{
    public partial class FrameMain : Form
    {
        private BluetoothClient btClient;
        private BluetoothListener btListener;
        private Mp3FileReader mp3Reader;
        private BufferedWaveProvider bufferedWaveProvider;
        private WaveOut waveOut;

        public FrameMain()
        {
            InitializeComponent();
            InitializeBluetooth();
        }

        private void InitializeBluetooth()
        {
            if (BluetoothRadio.PrimaryRadio.Mode != RadioMode.Discoverable)
                BluetoothRadio.PrimaryRadio.Mode = RadioMode.Discoverable;
            btClient = new BluetoothClient();
            btListener = new BluetoothListener(BluetoothService.SerialPort);
            btListener.Start();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (radioButtonSender.Checked)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "MP3 Files (*.mp3)|*.mp3|All files (*.*)|*.*";
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Get the path of specified file
                        var filePath = openFileDialog.FileName;

                        // Initialize Mp3FileReader with the selected file
                        mp3Reader = new Mp3FileReader(filePath);
                        StreamMp3();
                    }
                }

                labelStatus.Text = "Status: Looking for a client...";
            }
            else
            {
                labelStatus.Text = "Status: Searching for bluetooth server...";
                Task.Run(() => AcceptConnection());
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            mp3Reader?.Dispose();
            mp3Reader = null;
            waveOut?.Stop();
            labelStatus.Text = "Status: Stopped...";
        }

        private async Task AcceptConnection()
        {
            try
            {
                // Run the blocking call in a separate task to keep UI responsive
                var client = await Task.Run(() => btListener.AcceptBluetoothClient());
                btClient = client;
                Task.Run(() => StartReceivingAudio()); // Continue to process audio in a separate task
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., Bluetooth connectivity issues)
                MessageBox.Show($"Failed to accept connection: {ex.Message}", "Bluetooth Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StreamMp3()
        {
            var buffer = new byte[4096];
            int bytesRead;
            while ((bytesRead = mp3Reader.Read(buffer, 0, buffer.Length)) > 0 && mp3Reader != null)
            {
                if (btClient?.Connected ?? false)
                {
                    labelStatus.Text = "Status: Transmitting...";
                    btClient.GetStream().Write(buffer, 0, bytesRead);
                }
            }
        }

        private void StartReceivingAudio()
        {
            bufferedWaveProvider = new BufferedWaveProvider(waveOut.OutputWaveFormat);
            waveOut.Init(bufferedWaveProvider);

            var stream = btClient.GetStream();
            byte[] buffer = new byte[4096];
            int bytesRead;
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                bufferedWaveProvider.AddSamples(buffer, 0, bytesRead);
            }
            waveOut.Play();
        }
    }
}