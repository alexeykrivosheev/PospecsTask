namespace SciChartTestTask.Common
{
    public class WindowViewModelBase : ViewModelBase
    {
        public virtual bool OnClosingAction() => true;
    }
}
