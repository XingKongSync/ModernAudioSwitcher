using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace ModernAudioSwitcher.Controls
{
    /// <summary>
    /// InputBoxDialog.xaml 的交互逻辑
    /// </summary>
    public partial class InputBoxDialog : ContentDialog
    {
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Placeholder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(InputBoxDialog), new PropertyMetadata("Please input here"));




        public string SecondaryPlaceHolder
        {
            get { return (string)GetValue(SecondaryPlaceHolderProperty); }
            set { SetValue(SecondaryPlaceHolderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SecondaryPlaceHolder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SecondaryPlaceHolderProperty =
            DependencyProperty.Register("SecondaryPlaceHolder", typeof(string), typeof(InputBoxDialog), new PropertyMetadata("Please input here"));




        public string InputValue
        {
            get { return (string)GetValue(InputValueProperty); }
            set { SetValue(InputValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InputValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InputValueProperty =
            DependencyProperty.Register("InputValue", typeof(string), typeof(InputBoxDialog), new PropertyMetadata(""));




        public string SecondaryInputValue
        {
            get { return (string)GetValue(SecondaryInputValueProperty); }
            set { SetValue(SecondaryInputValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SecondaryInputValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SecondaryInputValueProperty =
            DependencyProperty.Register("SecondaryInputValue", typeof(string), typeof(InputBoxDialog), new PropertyMetadata(""));




        public SymbolRegular Icon
        {
            get { return (SymbolRegular)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(SymbolRegular), typeof(InputBoxDialog), new PropertyMetadata(SymbolRegular.Speaker232));



        public InputBoxDialog(ContentPresenter? contentPresenter) : base(contentPresenter)
        {
            InitializeComponent();
        }
    }
}
