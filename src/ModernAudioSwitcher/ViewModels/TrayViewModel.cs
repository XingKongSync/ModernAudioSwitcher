using CommunityToolkit.Mvvm.ComponentModel;
using ModernAudioSwitcher.Services;
using System.Collections.ObjectModel;

namespace ModernAudioSwitcher.ViewModels
{
    public partial class TrayViewModel : ObservableObject
    {
        public ObservableCollection<SerializableConfig> Presets { get => PresetService.Instance.Presets; }
    }
}
