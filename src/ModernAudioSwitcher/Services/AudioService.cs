using AudioSwitcher.Audio;

namespace ModernAudioSwitcher.Services
{
    public partial class AudioService
    {
        private static Lazy<AudioService> _service = new Lazy<AudioService>(() => new AudioService());

        public static AudioService Instance { get => _service.Value; }

        private AudioDeviceManager? _deviceManager;
        private Thread _deviceThread;
        private ManualResetEventSlim _eventSlim;

        private AudioService() 
        { 
            InitBindingSyncContext();

            _eventSlim = new ManualResetEventSlim(false);
            _deviceThread = new Thread(Init);
            _deviceThread.Start();
            _eventSlim.Wait(5000);
        }

        private void Init()
        {
            _deviceManager = new AudioDeviceManager();
            _eventSlim.Set();

            _deviceManager.DefaultDeviceChanged += DeviceManager_DefaultDeviceChanged;
            _deviceManager.DeviceAdded += DeviceManager_DeviceAdded;
            _deviceManager.DeviceRemoved += DeviceManager_DeviceRemoved;
            _deviceManager.DevicePropertyChanged += DeviceManager_DevicePropertyChanged;
            _deviceManager.DeviceStateChanged += DeviceManager_DeviceStateChanged;

            var devices = _deviceManager.GetAudioDevices(AudioDeviceKind.Playback, AudioDeviceState.Active);
            var devices2 = _deviceManager.GetAudioDevices(AudioDeviceKind.Recording, AudioDeviceState.Active);

            lock (_lock) 
            {
                HandleDevicesChange(devices);
                HandleDevicesChange(devices2);
            }
        }

        private void HandleDevicesChange(IEnumerable<AudioDevice> devices)
        {
            foreach (AudioDevice device in devices)
            {
                HandleDeviceChange(device);
            }
        }

        private void LockHandleDeviceChange(AudioDevice device)
        {
            lock (_lock)
            {
                CheckDefault();
                HandleDeviceChange(device);
            }
        }

        private void CheckDefault()
        { 
            XAudioDevice[] concat;
            lock (_lock)
            {
                var defPlayDevs = PlaybackDevicesSource.Where(d => d.IsDefault || d.IsDefaultMultimediaDevice || d.IsDefaultCommunicationDevice);
                var defRecDevs = RecordDevicesSource.Where(d => d.IsDefault || d.IsDefaultMultimediaDevice || d.IsDefaultCommunicationDevice);
                concat = defPlayDevs.Concat(defRecDevs).ToArray();
            }
            foreach (var vm in concat)
            {
                try
                {
                    var dev = _deviceManager!.GetDevice(vm.DeviceId);
                    vm.Update(dev);
                }
                catch (Exception)
                {
                }
            }
        }

        private void HandleDeviceChange(AudioDevice device)
        {
            if (device == null)
                return;

            var source = device.Kind == AudioDeviceKind.Playback ? PlaybackDevicesSource : RecordDevicesSource;

            XAudioDevice? dev;
            lock (_lock)
            {
                dev = source.FirstOrDefault(d => d.DeviceId == device.Id);
            }
            if (dev == null)
            {
                lock (_lock)
                {
                    source.Add(new XAudioDevice(device));
                }
            }
            else
            {
                if (device.IsActive)
                {
                    dev.Update(device);
                }
                else
                {
                    lock (_lock)
                    {
                        source.Remove(dev);
                    }
                }
            }
        }

        public bool IsDefaultDevice(AudioDevice device)
        {
            return _deviceManager!.IsDefaultAudioDevice(device, AudioDeviceRole.Multimedia)
                && _deviceManager.IsDefaultAudioDevice(device, AudioDeviceRole.Communications);
        }

        public bool IsDefaultMultimediaDevice(AudioDevice device)
        {
            return _deviceManager!.IsDefaultAudioDevice(device, AudioDeviceRole.Multimedia);
        }

        public bool IsDefaultCommunicationDevice(AudioDevice device)
        {
            return _deviceManager!.IsDefaultAudioDevice(device, AudioDeviceRole.Communications);
        }

        private void DeviceManager_DeviceStateChanged(object? sender, AudioDeviceStateEventArgs e)
        {
            LockHandleDeviceChange(e.Device);
        }

        private void DeviceManager_DevicePropertyChanged(object? sender, AudioDeviceEventArgs e)
        {
            LockHandleDeviceChange(e.Device);
        }

        private void DeviceManager_DeviceRemoved(object? sender, AudioDeviceRemovedEventArgs e)
        {

            var dev = PlaybackDevicesSource.FirstOrDefault(d => d.DeviceId == e.DeviceId)
                ?? RecordDevicesSource.FirstOrDefault(d => d.DeviceId == e.DeviceId);
            if (dev != null)
            {
                lock (_lock)
                {
                    PlaybackDevicesSource.Remove(dev);
                    RecordDevicesSource.Remove(dev);
                }
            }
        }

        private void DeviceManager_DeviceAdded(object? sender, AudioDeviceEventArgs e)
        {
            LockHandleDeviceChange(e.Device);
        }

        private void DeviceManager_DefaultDeviceChanged(object? sender, DefaultAudioDeviceEventArgs e)
        {
            LockHandleDeviceChange(e.Device);
        }
    }
}
