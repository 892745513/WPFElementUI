using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace dps
{
    public static class VisualTreeHelperEx
    {
        public static T FindVisualParent<T>(DependencyObject reference) where T : class
        {
            var parent = VisualTreeHelper.GetParent(reference);
            if (parent == null)
            {
                return null;
            }
            else
            {
                if (parent is T)
                {
                    return parent as T;
                }
                else
                {
                    return FindVisualParent<T>(parent);
                }
            }
        }
    }


}
