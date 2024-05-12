using AudioSwitcher.Audio;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace ModernAudioSwitcher.Services
{
    public partial class SerializableConfig : ObservableObject
    {
        public string PresetName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public SymbolRegular Icon { get; set; } = SymbolRegular.Speaker232;
        public SerializableDevice PlaybackMultimediaDevice { get; set; } = new SerializableDevice();
        public SerializableDevice PlaybackCommunicationDevice { get; set; } = new SerializableDevice();
        public SerializableDevice RecordMultimediaDevice { get; set; } = new SerializableDevice();
        public SerializableDevice RecordCommunicationDevice { get; set; } = new SerializableDevice();

        [RelayCommand]
        private void Delete()
        {
            PresetService.Instance.Delete(this);
        }

        [RelayCommand]
        private void SetAsDefault()
        {
            PresetService.Instance.SetAsDefault(this);
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Description))
                return Description;

            SerializableDevice[] devices =
            [
                PlaybackMultimediaDevice,
                PlaybackCommunicationDevice, 
                RecordMultimediaDevice,
                RecordCommunicationDevice,
            ];
            var names = devices.Where(d => !string.IsNullOrWhiteSpace(d.DeviceId)).DistinctBy(d => d.DeviceId).Select(d => $"{d.Description}({d.FriendlyName})");
            return string.Join(", ", names);
        }
    }

    public class SerializableDevice
    {
        public string DeviceId { get; set; } = string.Empty;
        public string FriendlyName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public static SerializableDevice Convert(AudioDevice audioDevice)
        {
            if (audioDevice == null)
                return new SerializableDevice();

            if (!audioDevice.TryDeviceFriendlyName(out string friendlyName))
                friendlyName = string.Empty;

            if (!audioDevice.TryGetDeviceDescription(out string description))
                description = string.Empty;

            return new SerializableDevice
            {
                DeviceId = audioDevice.Id,
                FriendlyName = friendlyName,
                Description = description
            };
        }
    }
}
