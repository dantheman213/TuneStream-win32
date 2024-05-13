using System;
using System.IO;
using System.Windows.Forms;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using NAudio.Wave;
using System.Diagnostics;

namespace TuneStream_Win32
{
    public partial class FormMain : Form
    {
        private BluetoothListener bluetoothListener;
        private BluetoothClient bluetoothClient;
        private WaveOutEvent waveOut;
        private BufferedWaveProvider bufferedProvider;
        private string audioFilePath;

        private bool startScanning;

        public FormMain()
        {
            InitializeComponent();
            waveOut = new WaveOutEvent();
        }

        private void ServerModeButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "MP3 Files|*.mp3",
                Title = "Select an MP3 File"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                audioFilePath = openFileDialog.FileName;

                serverModeButton.Text = "Server started...";
                serverModeButton.Enabled = false;
                clientModeButton.Enabled = false;
                deviceListComboBox.Enabled = false;
                connectButton.Enabled = false;

                StartBluetoothServer();
            }
        }

        private void ClientModeButton_Click(object sender, EventArgs e)
        {
            clientModeButton.Text = "Scanning...";
            clientModeButton.Enabled = false;
            serverModeButton.Enabled = false;

            ScanForDevices();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (deviceListComboBox.SelectedItem is BluetoothDeviceInfo deviceInfo)
            {
                ConnectToDevice(deviceInfo);
            }
        }

        private void ScanForDevices()
        {
            labelStatus.Text = "Scanning for devices to connect to...";
            timerScan.Enabled = true;
            startScanning = true;

            bluetoothClient = new BluetoothClient();
            BluetoothDeviceInfo[] devices = bluetoothClient.DiscoverDevices();
            Debug.WriteLine($"Found {devices.Length} device(s)...");

            deviceListComboBox.Items.Clear();
            foreach (var device in devices)
            {
                deviceListComboBox.Items.Add(device);
                deviceListComboBox.DisplayMember = "DeviceName";
                Debug.WriteLine($"Found bluetooth device: ${device}");
            }
            connectButton.Enabled = deviceListComboBox.Items.Count > 0;
        }

        private void ConnectToDevice(BluetoothDeviceInfo deviceInfo)
        {
            try
            {
                labelStatus.Text = "Attempting to connect to bluetooth device...";
                Debug.WriteLine($"Attempting to connect to bluetooth device {deviceInfo.DeviceName} / {deviceInfo.DeviceAddress} ...");

                bluetoothClient.BeginConnect(deviceInfo.DeviceAddress, BluetoothService.SerialPort, ClientConnectCallback, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to connect: {ex.Message}\n\n{ex.StackTrace}");
            }
        }

        private void StartBluetoothServer()
        {
            bluetoothListener = new BluetoothListener(BluetoothService.SerialPort);
            bluetoothListener.Start();
            bluetoothListener.BeginAcceptBluetoothClient(ServerAcceptClientCallback, null);

            labelStatus.Text = "Server is listening for connections...";
        }

        private void ServerAcceptClientCallback(IAsyncResult ar)
        {
            try
            {
                Debug.WriteLine("Received a request to connect...");
                var client = bluetoothListener.EndAcceptBluetoothClient(ar);
                var clientStream = client.GetStream();
                bluetoothListener.BeginAcceptBluetoothClient(ServerAcceptClientCallback, null);
                StreamAudioToClient(clientStream, audioFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error accepting client: {ex.Message}\n\n{ex.StackTrace}");
            }
        }

        private void ClientConnectCallback(IAsyncResult ar)
        {
            try
            {
                bluetoothClient.EndConnect(ar);
                var stream = bluetoothClient.GetStream();
                StartAudioStream(stream);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to server: {ex.Message}\n\n{ex.StackTrace}");
            }
        }

        private void StreamAudioToClient(Stream clientStream, string filePath)
        {
            using (var reader = new AudioFileReader(filePath))
            {
                var bufferedProvider = new BufferedWaveProvider(reader.WaveFormat);
                var buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    clientStream.Write(buffer, 0, bytesRead);
                }
            }
        }

        private void StartAudioStream(Stream netStream)
        {
            bufferedProvider = new BufferedWaveProvider(new WaveFormat(44100, 16, 2));
            waveOut.Init(bufferedProvider);
            var buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = netStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                bufferedProvider.AddSamples(buffer, 0, bytesRead);
            }
            waveOut.Play();
        }

        private void timerScan_Tick(object sender, EventArgs e)
        {
            if (startScanning)
            {
                ScanForDevices();
            }
        }
    }
}
