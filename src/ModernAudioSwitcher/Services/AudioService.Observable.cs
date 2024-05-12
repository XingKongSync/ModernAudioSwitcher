using AudioSwitcher.Audio;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace ModernAudioSwitcher.Services
{
    public partial class AudioService
    {
        public ObservableCollection<XAudioDevice> PlaybackDevicesSource { get; } = new ObservableCollection<XAudioDevice>();

        public ObservableCollection<XAudioDevice> RecordDevicesSource { get; } = new ObservableCollection<XAudioDevice>();

        private object _lock = new object();

        private void InitBindingSyncContext()
        {
            BindingOperations.EnableCollectionSynchronization(PlaybackDevicesSource, _lock);
            BindingOperations.EnableCollectionSynchronization(RecordDevicesSource, _lock);
        }

        private bool TryFindDevice(ObservableCollection<XAudioDevice> source, XAudioDevice target, out AudioDevice? device)
        {
            if (!TryGetAudioDevice(target.DeviceId, out device))
            {
                var dev1 = source.FirstOrDefault(d => d.DeviceId == target.DeviceId);
                if (dev1 == null || TryGetAudioDevice(dev1.DeviceId, out device))
                {
                    dev1 = source.FirstOrDefault(d => d.FriendlyName == target.FriendlyName && d.Description == target.Description);
                    if (dev1 != null)
                    {
                        TryGetAudioDevice(dev1.DeviceId, out device);
                    }
                }
            }
            return device != null;
        }

        public void SetAsDefualt(XAudioDevice dev, AudioDeviceRole role)
        {
            if (TryFindDevice(PlaybackDevicesSource, dev, out var device)
                || TryFindDevice(RecordDevicesSource, dev, out device))
            {
                if (device != null && device.IsActive)
                {
                    try
                    {
                        _deviceManager!.SetDefaultAudioDevice(device, role);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        private bool TryGetAudioDevice(string id, out AudioDevice? audioDevice)
        {
            audioDevice = null;
            try
            {
                audioDevice = _deviceManager!.GetDevice(id);
                return audioDevice != null;
            }
            catch (Exception)
            {
            }
            return false;
        }

        public AudioDevice GetDefaultAudioDevice(AudioDeviceKind kind, AudioDeviceRole role) => _deviceManager!.GetDefaultAudioDevice(kind, role);
    }
}
