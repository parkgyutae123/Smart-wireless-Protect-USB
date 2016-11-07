using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System;

namespace Smart_wireless_Protect_USB
{
    class BluetoothInfo
    {
        BluetoothClient bluetoothclient = new BluetoothClient();//Client(Local) Info
        BluetoothRadio bluetoothradio = BluetoothRadio.PrimaryRadio;//Peer devices Info

        string local_device_name = string.Empty;
        string local_device_connected = string.Empty;
        string local_device_address = string.Empty;
        string rssi = string.Empty;
        string peer_device_name = string.Empty;
        string peer_device_connected = string.Empty;
        string peer_device_address = string.Empty;

        public string GetLocalDeviceName //연결할 장치 이름
        {
            get
            {
                return local_device_name;
            }
            private set
            {
                local_device_name = value;
            }
        }
        public string GetLocalDeviceConnected // 연결할 장치의 접속유무
        {
            get
            {
                return local_device_connected;
            }
            private set
            {
                local_device_connected = value;
            }
        }
        public string GetLocalDeviceAddress // 연결할 장치의 MAC주소
        {
            get
            {
                return local_device_address;
            }
            private set
            {
                local_device_address = value;
            }
        }
        public string GetRSSI // 장치 거리
        {
            get
            {
                return rssi;
            }
            private set
            {
                rssi = value;
            }
        }
        public string GetPeerDeviceName // 블루투스 동글 이름
        {
            get
            {
                return bluetoothradio.Name;
            }
            private set
            {
                peer_device_name = value;
            }
        }
        public string GetPeerDeviceConnected // 블루투스 동글 연결유무
        {
            get
            {
                return peer_device_connected;
            }
            private set
            {
                peer_device_connected = value;
            }
        }
        public string GetPeerDeviceAddress // 블루투스 동글 MAC주소
        {
            get
            {
                return peer_device_address;
            }
            private set
            {
                peer_device_address = value;
            }
        }
        void LocalDeviceInfo()
        {
            var devices = bluetoothclient.DiscoverDevices();

            foreach (var device in devices)
            {
                GetLocalDeviceName = device.DeviceName;
                GetLocalDeviceConnected = device.Connected.ToString();
                GetLocalDeviceAddress = string.Format("{0:C}", device.DeviceAddress);
                GetRSSI = Convert.ToString(device.Rssi);
            }
        }
        void PeerDeviceInfo()
        {
            if (bluetoothradio == null)
            {
                Console.WriteLine("No radio hardware or unsupported software stack");
                return;
            }
            GetPeerDeviceName = bluetoothradio.Name;
            GetPeerDeviceConnected = Convert.ToString(bluetoothradio.Mode);
            GetPeerDeviceAddress = string.Format("{0:C}", bluetoothradio.LocalAddress);

        }
    }
}
