namespace Step4
{
    internal interface IRouteCalculator
    {
        FinalRoute? PlotCourse(int cityIndex1, int cityIndex2);
        int CitiesCount { get; }
    }
}