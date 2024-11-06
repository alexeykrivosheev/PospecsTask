using Microsoft.Xaml.Behaviors;
using SciChartTestTask.Common;
using System.Windows;

namespace SciChartTestTask.Behaviors
{
    public class MainWindowBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Closing += Window_Closing;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Closing -= Window_Closing;
            base.OnDetaching();
        }

        private void Window_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (AssociatedObject.DataContext is WindowViewModelBase windowViewModel)
            {
                if (!windowViewModel.OnClosingAction())
                {
                    e.Cancel = true;
                } 
            }
        }
    }
}
