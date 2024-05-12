using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ModernAudioSwitcher.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernAudioSwitcher.ViewModels
{
    public partial class DevicesViewModel : ObservableObject
    {
        public ObservableCollection<XAudioDevice> PlaybackDevices => AudioService.Instance.PlaybackDevicesSource;
        public ObservableCollection<XAudioDevice> RecordDevices => AudioService.Instance.RecordDevicesSource;
    }
}
