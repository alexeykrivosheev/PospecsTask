using System.Windows.Media;
using System.ComponentModel;
using SciChartTestTask.Common;

namespace SciChartTestTask.FunctionsViewModels
{
    public class FunctionViewModel : ViewModelBase
    {
        private string _name;
        private ItemsChangeCollection<FunctionPointViewModel> _points;
        public Func<double, double> CalculateFunction { get; set; } = null!;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public Color Color { get; set; }

        public ItemsChangeCollection<FunctionPointViewModel> Points
        {
            get => _points;
            set
            {
                if (_points != null)
                {
                    _points.ItemChanged -= PointPropertyChanged;
                }

                _points = value;
                _points.ItemChanged += PointPropertyChanged;

                OnPropertyChanged(nameof(Points));
            }
        }
        public bool HasChanges { get; set; }


        private void PointPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            HasChanges = true;
            OnPropertyChanged(e.PropertyName);
        }

    }
}
