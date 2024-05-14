using System;
using System.IO;
using System.Windows.Forms;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using NAudio.Wave;
using System.Diagnostics;
using System.Threading.Tasks;

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

        private async void ServerModeButton_Click(object sender, EventArgs e)
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

                await StartBluetoothServerAsync();
            }
        }

        private async void ClientModeButton_Click(object sender, EventArgs e)
        {
            clientModeButton.Text = "Scanning...";
            clientModeButton.Enabled = false;
            serverModeButton.Enabled = false;

            await ScanForDevicesAsync();
        }

        private async void ConnectButton_Click(object sender, EventArgs e)
        {
            if (deviceListComboBox.SelectedItem is BluetoothDeviceInfo deviceInfo)
            {
                await ConnectToDeviceAsync(deviceInfo);
            }
        }

        private async Task ScanForDevicesAsync()
        {
            labelStatus.Text = "Scanning for devices to connect to...";
            startScanning = true;

            bluetoothClient = new BluetoothClient();
            BluetoothDeviceInfo[] devices = await Task.Run(() => bluetoothClient.DiscoverDevices());
            Debug.WriteLine($"Found {devices.Length} device(s)...");

            deviceListComboBox.Items.Clear();
            foreach (var device in devices)
            {
                deviceListComboBox.Items.Add(device);
                deviceListComboBox.DisplayMember = "DeviceName";
                Debug.WriteLine($"Found bluetooth device: {device}");
            }
            connectButton.Enabled = deviceListComboBox.Items.Count > 0;
        }

        private async Task ConnectToDeviceAsync(BluetoothDeviceInfo deviceInfo)
        {
            try
            {
                labelStatus.Text = "Attempting to connect to bluetooth device...";
                Debug.WriteLine($"Attempting to connect to bluetooth device {deviceInfo.DeviceName} / {deviceInfo.DeviceAddress} ...");

                await Task.Factory.FromAsync(bluetoothClient.BeginConnect, bluetoothClient.EndConnect, deviceInfo.DeviceAddress, BluetoothService.SerialPort, null);
                StartAudioStream(bluetoothClient.GetStream());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to connect: {ex.Message}\n\n{ex.StackTrace}");
            }
        }

        private async Task StartBluetoothServerAsync()
        {
            bluetoothListener = new BluetoothListener(BluetoothService.SerialPort);
            bluetoothListener.Start();
            labelStatus.Text = "Server is listening for connections...";

            while (true)
            {
                var client = await Task.Factory.FromAsync(bluetoothListener.BeginAcceptBluetoothClient, bluetoothListener.EndAcceptBluetoothClient, null);
                Debug.WriteLine("Received a request to connect...");
                var clientStream = client.GetStream();
                _ = StreamAudioToClientAsync(clientStream, audioFilePath);
            }
        }

        private async Task StreamAudioToClientAsync(Stream clientStream, string filePath)
        {
            try
            {
                using (var reader = new AudioFileReader(filePath))
                {
                    var buffer = new byte[1024];
                    int bytesRead;
                    while ((bytesRead = await reader.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await clientStream.WriteAsync(buffer, 0, bytesRead);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error streaming audio: {ex.Message}\n\n{ex.StackTrace}");
            }
        }

        private async void StartAudioStream(Stream netStream)
        {
            bufferedProvider = new BufferedWaveProvider(new WaveFormat(44100, 16, 2));
            waveOut.Init(bufferedProvider);
            var buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = await netStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                bufferedProvider.AddSamples(buffer, 0, bytesRead);
            }
            waveOut.Play();
        }
    }
}
