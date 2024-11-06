using SciChartTestTask.FunctionsViewModels;
using SciChartTestTask.Services.Models;
using System.Windows.Media;

namespace SciChartTestTask.Common
{
    public static class MappingHelper
    {
        public static FunctionModel ConvertToModel(FunctionViewModel functionViewModel)
        {
            var functionModel = new FunctionModel();
            functionModel.Points = new List<PointModel>();
            functionModel.Name = functionViewModel.Name;
            functionModel.Color = System.Drawing.Color.FromArgb(
                functionViewModel.Color.A,
                functionViewModel.Color.R,
                functionViewModel.Color.G,
                functionViewModel.Color.B);

            foreach (var point in functionViewModel.Points)
            {
                functionModel.Points.Add(new PointModel() { XValue = point.XValue, YValue = point.YValue });
            }
            return functionModel;
        }

        public static void ConvertToViewModel(FunctionModel functionModel, FunctionViewModel functionViewModel)
        {
            functionViewModel.Name = functionModel.Name;
            functionViewModel.Color = Color.FromArgb(
                functionModel.Color.A,
                functionModel.Color.R,
                functionModel.Color.G,
                functionModel.Color.B);
            functionViewModel.Points.Clear();
            foreach (var point in functionModel.Points)
            {
                functionViewModel.Points.Add(new FunctionPointViewModel(point.XValue, functionViewModel.CalculateFunction));
            }
        }
    }
}
