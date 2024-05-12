using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace ModernAudioSwitcher.Views
{
    internal static class VisualTreeExtension
    {
        public static childItem? FindVisualChild<childItem>(DependencyObject obj, Func<DependencyObject, bool>? judge = null) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem cItem && (judge == null || judge(cItem)))
                {
                    return cItem;
                }
                else
                {
                    childItem? childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
