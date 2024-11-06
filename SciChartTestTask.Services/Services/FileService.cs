using Newtonsoft.Json;
using SciChartTestTask.Services.Contracts;
using SciChartTestTask.Services.Models;

namespace SciChartTestTask.Services.Services
{
    public class FileService : IFileService
    {
        public FunctionModel LoadFunction(string path)
        {
            string content;

            using (StreamReader reader = new StreamReader(path))
            {
                content = reader.ReadToEnd();
            }

            FunctionModel functionModel = JsonConvert.DeserializeObject<FunctionModel>(content);

            return functionModel;
        }

        public void SaveFunction(FunctionModel functionModel, string path)
        {
            var content = JsonConvert.SerializeObject(functionModel);

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.Write(content);
            }
        }
    }
}
