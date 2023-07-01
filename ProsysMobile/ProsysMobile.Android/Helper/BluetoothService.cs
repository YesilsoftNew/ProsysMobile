using Android.Bluetooth;
using Java.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using WiseMobile.Droid.Helper;
using WiseMobile.Helper;
using WiseMobile.Models.CommonModels.CustomModel;
using WiseMobile.Models.CommonModels.Enums;
using WiseMobile.Services.Bluetooth;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(BluetoothService))]
namespace WiseMobile.Droid.Helper
{
    public class BluetoothService : IBluetoothService
    {
        BufferedReader reader;
        Stream mStream;
        InputStreamReader mReader;

        public List<BluetoothDeviceList> getDevices()
        {
            try
            {
                Android.Bluetooth.BluetoothAdapter btAdapter = Android.Bluetooth.BluetoothAdapter.DefaultAdapter;
                btAdapter.Enable();

                Thread.Sleep(1000);

                if (btAdapter.IsEnabled)
                {
                    ICollection<BluetoothDevice> devices = btAdapter.BondedDevices;

                    if (devices == null)
                        return new List<BluetoothDeviceList>();

                    var json = JsonConvert.SerializeObject(devices.Select(t => new { t.Name, t.Address }).ToList());
                    return JsonConvert.DeserializeObject<List<BluetoothDeviceList>>(json);
                }

                return new List<BluetoothDeviceList>();
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
                return new List<BluetoothDeviceList>();
            }
        }

        public void DisconnectDevice()
        {
            try
            {
                if (GENERAL.mSocket != null && GENERAL.mSocket.IsConnected)
                {
                    GENERAL.mSocket.Close();
                    GlobalSetting.Instance.isBluetoothConnected = false;

                    MessagingCenter.Send<object, enCustomBluetoothToolbar>("BluetoothService", "BluetoothConnectionStausChanged", enCustomBluetoothToolbar.NotConnectedToDevice);
                }
            }
            catch (Exception ex)
            {
                // buralayı bilerek log'lamadım gerek yok
                // WiseLogger.Instance.CrashLog(ex);
                GlobalSetting.Instance.isBluetoothConnected = true;

                MessagingCenter.Send<object, enCustomBluetoothToolbar>("BluetoothService", "BluetoothConnectionStausChanged", enCustomBluetoothToolbar.ConnectedToDevice);

                return;
            }
        }

        public bool ConnectDevice()
        {
            try // bağlantıyı yapıyorum
            {
                if (GlobalSetting.Instance.bluetoothDevice == null)
                    return false;

                MessagingCenter.Send<object, enCustomBluetoothToolbar>("BluetoothService", "BluetoothConnectionStausChanged", enCustomBluetoothToolbar.Connecting);

                try
                {
                    if (GENERAL.mSocket != null && GENERAL.mSocket.IsConnected)
                    {
                        GlobalSetting.Instance.isBluetoothConnected = true;

                        MessagingCenter.Send<object, enCustomBluetoothToolbar>("BluetoothService", "BluetoothConnectionStausChanged", enCustomBluetoothToolbar.ConnectedToDevice);

                        return true;
                    }
                }
                catch (Exception ex)
                {
                    GlobalSetting.Instance.isBluetoothConnected = false;

                    MessagingCenter.Send<object, enCustomBluetoothToolbar>("BluetoothService", "BluetoothConnectionStausChanged", enCustomBluetoothToolbar.ConnectionFailed);

                    WiseLogger.Instance.CrashLog(ex);
                }

                if (GENERAL.device != null)
                    GENERAL.device.Dispose();

                if (GENERAL.mSocket != null)
                    GENERAL.mSocket.Dispose();

                Android.Bluetooth.BluetoothAdapter btAdapter = Android.Bluetooth.BluetoothAdapter.DefaultAdapter;
                btAdapter.Enable();

                Thread.Sleep(1000);

                if (btAdapter.IsEnabled)
                {
                    GENERAL.device = btAdapter.GetRemoteDevice(GlobalSetting.Instance.bluetoothDevice.Address);

                    if (GENERAL.device == null) // cihaz yok
                    {
                        GlobalSetting.Instance.isBluetoothConnected = false;

                        MessagingCenter.Send<object, enCustomBluetoothToolbar>("BluetoothService", "BluetoothConnectionStausChanged", enCustomBluetoothToolbar.ConnectionFailed);

                        return false;
                    }
                }

                GENERAL.mSocket = GENERAL.device.CreateInsecureRfcommSocketToServiceRecord(Java.Util.UUID.FromString("00001101-0000-1000-8000-00805F9B34FB"));

                if (!GENERAL.mSocket.IsConnected)
                    GENERAL.mSocket.Connect();

                GlobalSetting.Instance.BluetoothConnectionRetryCount = 0;
                GlobalSetting.Instance.isBluetoothConnected = true;

                MessagingCenter.Send<object, enCustomBluetoothToolbar>("BluetoothService", "BluetoothConnectionStausChanged", enCustomBluetoothToolbar.ConnectedToDevice);

                return true;
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
                GlobalSetting.Instance.isBluetoothConnected = false;

                MessagingCenter.Send<object, enCustomBluetoothToolbar>("BluetoothService", "BluetoothConnectionStausChanged", enCustomBluetoothToolbar.ConnectionFailed);

                return false;
            }
        }

        public void GetDeviceData()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    mStream = GENERAL.mSocket.InputStream;

                    mReader = new Java.IO.InputStreamReader(mStream);
                    reader = new Java.IO.BufferedReader(mReader);

                    var bluetoothResult = await reader.ReadLineAsync();

                    if (!string.IsNullOrWhiteSpace(bluetoothResult) && !bluetoothResult.StartsWith("T") && !bluetoothResult.StartsWith("P"))
                    {
                        GetDeviceData();
                        return;
                    }

                    GlobalSetting.Instance.BluetoothGetDataRunning = false;

                    MessagingCenter.Send<object, string>("BluetoothService", "BluetoothDataCommand", bluetoothResult);
                }
                catch (Exception ex)
                {
                    // buralayı bilerek log'lamadım gerek yok her disconnect olunca hata veriyor o sırada veri cekmeye calısıyorsan o yüzden yapmadım 
                    // WiseLogger.Instance.CrashLog(ex);

                    GlobalSetting.Instance.BluetoothGetDataRunning = false;
                    GlobalSetting.Instance.isBluetoothConnected = false;

                    MessagingCenter.Send<object, string>("BluetoothService", "BluetoothDataCommand", "");

                    if (GENERAL.device != null)
                        GENERAL.device.Dispose();

                    if (GENERAL.mSocket != null)
                        GENERAL.mSocket.Dispose();

                    MessagingCenter.Send<object, enCustomBluetoothToolbar>("BluetoothService", "BluetoothConnectionStausChanged", enCustomBluetoothToolbar.ConnectionFailed);
                    return;
                }
            });
        }
    }
}