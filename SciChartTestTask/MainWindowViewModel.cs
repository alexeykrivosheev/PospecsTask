using Microsoft.Win32;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals.PointMarkers;
using SciChartTestTask.Common;
using SciChartTestTask.FunctionsViewModels;
using SciChartTestTask.Services.Contracts;
using SciChartTestTask.Services.Services;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace SciChartTestTask
{
    public class MainWindowViewModel : WindowViewModelBase
    {
        private ItemsChangeCollection<FunctionViewModel> _functions = null!;
        private FunctionViewModel _selectedFunction = null!;
        private ObservableCollection<IRenderableSeriesViewModel> _renderableSeries = null!;
        private FunctionsSupplier _functionsSupplier = new FunctionsSupplier();
        private IFileService _fileService;
        public MainWindowViewModel()
        {
            _fileService = new FileService();

            InitializeInitialPointsWithDefaults();
            InitializeChart();

            AddPointCommand = new DelegateCommand(AddPointAction);
            DeletePointCommand = new DelegateCommand(DeletePointAction);
            SaveCommand = new DelegateCommand(SaveAction);
            LoadCommand = new DelegateCommand(LoadAction);
        }

        public DelegateCommand AddPointCommand { get; set; }
        public DelegateCommand DeletePointCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand LoadCommand { get; set; }

        public ObservableCollection<IRenderableSeriesViewModel> RenderableSeries
        {
            get { return _renderableSeries; }
            set
            {
                _renderableSeries = value;
                OnPropertyChanged(nameof(RenderableSeries));
            }
        }

        public ItemsChangeCollection<FunctionViewModel> Functions
        {
            get { return _functions; }
            set
            {
                _functions = value;
                OnPropertyChanged(nameof(Functions));
            }
        }

        public FunctionViewModel SelectedFunction
        {
            get { return _selectedFunction; }
            set
            {
                _selectedFunction = value;
                OnPropertyChanged(nameof(SelectedFunction));
            }
        }

        private void AddPointAction(object param)
        {
            SelectedFunction.Points.Add(new FunctionPointViewModel(null, SelectedFunction.CalculateFunction));
        }

        private void DeletePointAction(object param)
        {
            var pointToDelete = ((IList)param).Cast<FunctionPointViewModel>().ToList().FirstOrDefault();

            if (pointToDelete != null)
            {
                SelectedFunction.Points.Remove(pointToDelete);
            }
            UpdateSeries(SelectedFunction);
        }

        private void SaveAction(object param)
        {
            var saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog().GetValueOrDefault())
            {
                var functionModel = MappingHelper.ConvertToModel(SelectedFunction);
                _fileService.SaveFunction(functionModel, saveFileDialog.FileName);
            }
            foreach (var funcion in Functions)
            {
                funcion.HasChanges = false;
            }
        }

        private void LoadAction(object param)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                var functionModel = _fileService.LoadFunction(openFileDialog.FileName);
                MappingHelper.ConvertToViewModel(functionModel, SelectedFunction);
                UpdateSeries(SelectedFunction);
            }
        }

        public override bool OnClosingAction()
        {
            if (Functions.Any(x => x.HasChanges))
            {
                var result = MessageBox.Show("You have some unsaved changes do you want to contunue without saving?", "Warining", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes) return true;
                if (result == MessageBoxResult.No) return false;
                if (result == MessageBoxResult.Cancel) return false;
            }

            return true;
        }

        private void InitializeInitialPointsWithDefaults()
        {
            _functions = new ItemsChangeCollection<FunctionViewModel>();
            _functions.ItemChanged += FunctionChanged;
            var fistFunction = _functionsSupplier.GetFunctionByName(Constants.FirstFunctionName);

            Functions.Add(new FunctionViewModel()
            {
                Name = Constants.FirstFunctionName,
                Color = Colors.Yellow,
                CalculateFunction = fistFunction,
                Points = new ItemsChangeCollection<FunctionPointViewModel>
                {
                    new FunctionPointViewModel(0, fistFunction),
                    new FunctionPointViewModel(1, fistFunction),
                    new FunctionPointViewModel(2, fistFunction),
                    new FunctionPointViewModel(8, fistFunction),
                }
            });

            var secondFunction = _functionsSupplier.GetFunctionByName(Constants.SecondFunctionName);

            Functions.Add(new FunctionViewModel()
            {
                Name = "SecondFunc",
                Color = Colors.Red,
                CalculateFunction = secondFunction,
                Points = new ItemsChangeCollection<FunctionPointViewModel>
                {
                    new FunctionPointViewModel(0, secondFunction),
                    new FunctionPointViewModel(1, secondFunction),
                    new FunctionPointViewModel(2, secondFunction),
                }
            });
        }
        private void InitializeChart()
        {
            _renderableSeries = new ObservableCollection<IRenderableSeriesViewModel>();

            foreach (var function in Functions)
            {
                AddSeries(function);
            }
        }

        private void FunctionChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is not null && sender is FunctionViewModel)
            {
                UpdateSeries((FunctionViewModel)sender);
            }
        }

        private void AddSeries(FunctionViewModel function)
        {
            if (function.Points.All(x => x.YValue.HasValue && x.XValue.HasValue))
            {
                var lineData = new XyDataSeries<double, double>() { SeriesName = function.Name };
                foreach (var point in function.Points)
                {
                    if (point.XValue.HasValue && point.YValue.HasValue)
                    {
                        lineData.Append(point.XValue.Value, point.YValue.Value);
                    }
                }

                var series = new LineRenderableSeriesViewModel()
                {
                    StrokeThickness = 2,
                    Stroke = function.Color,
                    DataSeries = lineData,
                    PointMarker = new EllipsePointMarker() { Width = 6, Height = 6 }
                };
                series.DataSeries.AcceptsUnsortedData = true;

                RenderableSeries.Add(series);
            }
        }

        private void UpdateSeries(FunctionViewModel function)
        {
            if (function.Points.All(x => x.YValue.HasValue && x.XValue.HasValue))
            {
                var seriesToUpdate = RenderableSeries.FirstOrDefault(x => x.DataSeries.SeriesName.Equals(function.Name));
                if (seriesToUpdate != null)
                {
                    RenderableSeries.Remove(seriesToUpdate);
                    AddSeries(function);
                }
            }
        }

    }
}
