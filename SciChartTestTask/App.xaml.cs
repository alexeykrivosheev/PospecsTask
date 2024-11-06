using Newtonsoft.Json;
using SciChart.Charting.Visuals;
using System.Configuration;
using System.Data;
using System.Windows;

namespace SciChartTestTask
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            SciChartSurface.SetRuntimeLicenseKey("l1Ir2b9K5daccoj7mOQuQid6eGz6//GHGdrxDvvxL6s2GK3kUDfXtFujev4e92A1svHbkDUmcnRPW0dlNbq7KrEBKdCc3s0yGdsouJ0y8r+zOCb/dH0ZLVQii0eSREGgd01v0HTzitfObyMuBShMtLfIbMXvlFRptBy0MCFmrXnzrl5wEHI3s9P+0sBQi/p/HazCBQBwxP3VGD16p9CQnbRep840q67+o3jZQ/sfFDW3CJQXCIRdX4JzpClck81onOlijkoe00add1MtH4Fribx3ydhiASMAARyr80f+waXCcmNuBP+dYo7xLeqtrACZ5PYVvpD8zJqKog7lZBQD2otJNqWUz8FFrRCZ2eMcyW+oKNZVklE6+tAQlOtPQ1wk2E8ohtFCNUZT+zIGn6urqGOMFjoZTzeTzrWYTrfF3vofhsAEhPQZXd3J6RwZY8M19rzkjXMom5fHCoDEktll7Yv6Wp9I7xWb3P/d6Wb51J7CdWp3ADJjSTiXoRkq8MMxaqjfL4ur3RQLLNsYhy4Tq4eUtfek8QWXXR346cRyIYkj1oKSctRlXM3A2WDqiv9egv23s8oh1Dj68W2KSw==");
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        }
    }

}
