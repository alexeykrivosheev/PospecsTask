namespace SciChartTestTask.Common
{
    public class FunctionsSupplier
    {
        public Func<double, double> GetFunctionByName(string name)
        {
            if (name == Constants.FirstFunctionName) return FirstFunction;
            if (name == Constants.SecondFunctionName) return SecondFuction;
            return null;
        }

        private double FirstFunction(double x)
        {
            if (x <= -3) return -1 * x - 3;
            if (x < 0 && x > -3) return x + 3;
            if (x >= 0 && x < 3) return -2 * x + 3;
            if (x >= 3) return 0.5 * x - 4.5;

            return 0;
        }

        private double SecondFuction(double x)
        {
            if (x <= -3) return -1 * x - 3;
            if (x < 0 && x > -3) return x + 4;
            if (x >= 0 && x < 4) return -2 * x + 5;
            if (x >= 4) return 0.5 * x - 5.5;

            return 0;
        }
    }
}
