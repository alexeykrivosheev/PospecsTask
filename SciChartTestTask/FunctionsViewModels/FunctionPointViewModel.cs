using SciChartTestTask.Common;

namespace SciChartTestTask.FunctionsViewModels
{
    public class FunctionPointViewModel : ViewModelBase
    {
        private double? _xValue;
        private double? _yValue;
        private Func<double, double> _calculateFunction { get; set; }

        public FunctionPointViewModel(double? xValue, Func<double, double> calculateFunction)
        {
            _calculateFunction = calculateFunction;
            _xValue = xValue;
        }

        public double? XValue
        {
            get => _xValue;
            set
            {
                _xValue = value;
                OnPropertyChanged(nameof(XValue));
                OnPropertyChanged(nameof(YValue));
            }
        }

        public double? YValue
        {
            get => XValue.HasValue ? _calculateFunction(XValue.Value) : null;
        }
    }
}
