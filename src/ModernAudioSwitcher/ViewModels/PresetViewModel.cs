using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModernAudioSwitcher.Controls;
using ModernAudioSwitcher.Services;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace ModernAudioSwitcher.ViewModels
{
    public partial class PresetViewModel : ObservableObject
    {
        public ObservableCollection<SerializableConfig> Presets { get => PresetService.Instance.Presets; }

        [RelayCommand]
        private async Task CreatePreset()
        {
            var dialog = new InputBoxDialog(UIService.Instance.ContentDialogService.GetDialogHost());
            dialog.Title = Resources.Resource.CreatePreset;//"Create Preset"
            dialog.Placeholder = Resources.Resource.PresetName;//"Preset Name"
            dialog.SecondaryPlaceHolder = Resources.Resource.PresetDescription;//"Description";
            var result = await dialog.ShowAsync();
            var presetName = dialog.InputValue;
            var description = dialog.SecondaryInputValue;
            var icon = dialog.Icon;
            if (result is ContentDialogResult.Primary
                && !string.IsNullOrWhiteSpace(presetName))
            {
                PresetService.Instance.Create(presetName, description, icon);
            }
        }
    }
}
