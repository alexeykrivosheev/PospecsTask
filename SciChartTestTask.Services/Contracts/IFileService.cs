using SciChartTestTask.Services.Models;

namespace SciChartTestTask.Services.Contracts
{
    public interface IFileService
    {
        void SaveFunction(FunctionModel functionModel, string path);
        FunctionModel LoadFunction(string path);

    }
}
