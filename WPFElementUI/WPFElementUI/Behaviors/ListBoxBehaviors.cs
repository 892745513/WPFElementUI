using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Behaviors
{
    public class AutoScrollBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            //this.AssociatedObject.SelectionChanged += new SelectionChangedEventHandler(AssociatedObject_SelectionChanged);

            this.AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
            AssociatedObject_SelectionChanged(this.AssociatedObject, null);


            this.AssociatedObject.SizeChanged += AssociatedObject_Initialized;
        }

        private void AssociatedObject_Initialized(object sender, EventArgs e)
        {
            ListBox listbox = (sender as ListBox);
            if (listbox != null)
            {
                if (listbox.SelectedItem != null)
                {
                    listbox.Dispatcher.Invoke(delegate
                    {
                        listbox.ScrollIntoView(listbox.SelectedItem); 
                    });
                }
            }
        }

        void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listbox = (sender as ListBox);
            if (listbox != null)
            {
                if (listbox.SelectedItem != null)
                {
                    listbox.Dispatcher.Invoke(delegate
                    {
                        listbox.UpdateLayout();

                        listbox.ScrollIntoView(listbox.SelectedItem);

                    });
                }
            }
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.SelectionChanged -=
                AssociatedObject_SelectionChanged;
        }
    }
}
