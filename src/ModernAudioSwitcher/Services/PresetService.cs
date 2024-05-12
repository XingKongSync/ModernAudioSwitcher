using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModernAudioSwitcher.Services
{
    public class PresetService
    {
        private const string CONFIG_FILE_NAME = @"Config.json";
        private static Lazy<PresetService> _service = new Lazy<PresetService>(() => new PresetService());

        public static PresetService Instance { get {  return _service.Value; } }

        public ObservableCollection<SerializableConfig> Presets { get; } = new ObservableCollection<SerializableConfig>();

        private string _configFileFullPath;

        private PresetService() 
        {
            string baseDir = Path.GetDirectoryName(Process.GetCurrentProcess()?.MainModule?.FileName) ?? AppDomain.CurrentDomain.BaseDirectory;
            _configFileFullPath = Path.Combine(baseDir, CONFIG_FILE_NAME);
            Load();
        }

        private void Load()
        {
            Presets.Clear();
            try
            {
                if (File.Exists(_configFileFullPath))
                {
                    string configJson = File.ReadAllText(_configFileFullPath);
                    var config = JsonSerializer.Deserialize<List<SerializableConfig>>(configJson);
                    if (config == null)
                        return;
                    foreach (var item in config)
                    {
                        Presets.Add(item);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void Save()
        {
            try
            {
                string json = JsonSerializer.Serialize(Presets.ToArray());
                File.WriteAllText(_configFileFullPath, json);
            }
            catch (Exception)
            {
            }
        }

        public void Create(string presetName, string description, Wpf.Ui.Controls.SymbolRegular icon)
        {
            AudioService audioService = AudioService.Instance;
            var pmDevice = audioService.GetDefaultAudioDevice(AudioSwitcher.Audio.AudioDeviceKind.Playback, AudioSwitcher.Audio.AudioDeviceRole.Multimedia);
            var pcDevice = audioService.GetDefaultAudioDevice(AudioSwitcher.Audio.AudioDeviceKind.Playback, AudioSwitcher.Audio.AudioDeviceRole.Communications);

            var rmDevice = audioService.GetDefaultAudioDevice(AudioSwitcher.Audio.AudioDeviceKind.Recording, AudioSwitcher.Audio.AudioDeviceRole.Multimedia);
            var rcDevice = audioService.GetDefaultAudioDevice(AudioSwitcher.Audio.AudioDeviceKind.Recording, AudioSwitcher.Audio.AudioDeviceRole.Communications);

            var preset = new SerializableConfig()
            {
                PresetName = presetName,
                Description = description,
                Icon = icon,
                PlaybackMultimediaDevice = SerializableDevice.Convert(pmDevice),
                PlaybackCommunicationDevice = SerializableDevice.Convert(pcDevice),
                RecordMultimediaDevice= SerializableDevice.Convert(rmDevice),
                RecordCommunicationDevice = SerializableDevice.Convert(rcDevice)
            };

            Presets.Add(preset);
            Save();
        }

        public void Delete(SerializableConfig serializableConfig)
        {
            Presets.Remove(serializableConfig);
            Save();
        }

        public void SetAsDefault(SerializableConfig serializableConfig)
        {
            AudioService audioService = AudioService.Instance;
            audioService.SetAsDefualt(new XAudioDevice(serializableConfig.PlaybackMultimediaDevice), AudioSwitcher.Audio.AudioDeviceRole.Multimedia);
            audioService.SetAsDefualt(new XAudioDevice(serializableConfig.PlaybackCommunicationDevice), AudioSwitcher.Audio.AudioDeviceRole.Communications);
            audioService.SetAsDefualt(new XAudioDevice(serializableConfig.RecordMultimediaDevice), AudioSwitcher.Audio.AudioDeviceRole.Multimedia);
            audioService.SetAsDefualt(new XAudioDevice(serializableConfig.RecordCommunicationDevice), AudioSwitcher.Audio.AudioDeviceRole.Communications);
        }
    }
}
