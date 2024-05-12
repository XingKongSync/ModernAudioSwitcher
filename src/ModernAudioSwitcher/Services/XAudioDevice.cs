using AudioSwitcher.Audio;
using AudioSwitcher.Presentation.Drawing;
using AudioSwitcher.Presentation.UI;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace ModernAudioSwitcher.Services
{
    public partial class XAudioDevice : ObservableObject
    {
        public string DeviceId { get; set; } = string.Empty;

        [ObservableProperty]
        private string _friendlyName = string.Empty;

        [ObservableProperty]
        private string _description = string.Empty;

        [ObservableProperty]
        private bool _isDefault = false;

        [ObservableProperty]
        private bool _isDefaultMultimediaDevice = false;

        [ObservableProperty]
        private bool _isDefaultCommunicationDevice = false;

        [ObservableProperty]
        private BitmapSource? _icon;

        public XAudioDevice() { }

        public XAudioDevice(AudioDevice dev) : base()
        {
            Update(dev);
        }

        public XAudioDevice(SerializableDevice playbackMultimediaDevice)
        {
            DeviceId = playbackMultimediaDevice.DeviceId;
            FriendlyName = playbackMultimediaDevice.FriendlyName;
            Description = playbackMultimediaDevice.Description;
        }

        public void Update(AudioDevice dev)
        {
            DeviceId = dev.Id;
            if (dev.TryDeviceFriendlyName(out string name))
                FriendlyName = name;
            if (dev.TryGetDeviceDescription(out string description))
                Description = description;
            if (string.IsNullOrWhiteSpace(FriendlyName))
            {
                FriendlyName = description;
            }
            IsDefault = AudioService.Instance.IsDefaultDevice(dev);
            IsDefaultMultimediaDevice = AudioService.Instance.IsDefaultMultimediaDevice(dev);
            IsDefaultCommunicationDevice = AudioService.Instance.IsDefaultCommunicationDevice(dev);
            if (dev.TryGetDeviceClassIconPath(out string iconPath))
            {
                Task.Run(() => UpdateIcon(iconPath));
            }
        }

        private void UpdateIcon(string iconPath)
        {
            var icon = IconLoadService.Instance.GetIcon(iconPath);
            Icon = icon;
        }

        [RelayCommand]
        private void SetAsDefault()
        {
            try
            {
                AudioService.Instance.SetAsDefualt(this, AudioDeviceRole.Multimedia);
                AudioService.Instance.SetAsDefualt(this, AudioDeviceRole.Communications);
            }
            catch
            {
            }
        }

        [RelayCommand]
        private void SetAsDefaultMultiMediaDevice()
        {
            try
            {
                AudioService.Instance.SetAsDefualt(this, AudioDeviceRole.Multimedia);
            }
            catch
            {
            }
        }

        [RelayCommand]
        private void SetAsDefaultCommunicationDevice()
        {
            try
            {
                AudioService.Instance.SetAsDefualt(this, AudioDeviceRole.Communications);
            }
            catch
            {
            }
        }
    }
}
