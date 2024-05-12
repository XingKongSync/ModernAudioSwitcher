using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace ModernAudioSwitcher.Controls
{
    public class IconProvider
    {
        private static SymbolRegular[] _icons =
        {
            SymbolRegular.Speaker232,
            SymbolRegular.Star24,
            SymbolRegular.StarEmphasis24,
            SymbolRegular.NetworkAdapter16,
            SymbolRegular.Headset16,
            SymbolRegular.HeadsetVr24,
            SymbolRegular.DesktopSpeaker24,
            SymbolRegular.SpeakerBluetooth24,
            SymbolRegular.SpeakerUsb24,
            SymbolRegular.TabletSpeaker24,
            SymbolRegular.PhoneSpeaker24
        };

        public static SymbolRegular[] GetIcons() => _icons;
    }
}
