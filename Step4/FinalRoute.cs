using System.Collections.Generic;
using System.Linq;
using Step4.RouteObjects;

namespace Step4
{
    public class FinalRoute
    {
        public IReadOnlyCollection<Vector2D> Steps { get; }

        public FinalRoute(IReadOnlyCollection<Vector2D> steps)
        {
            Steps = steps;
        }

        public override string ToString()
        {
            return $"Steps: {Steps.Count} From: {Steps.First().Start} To {Steps.Last().End} Total: {Steps.Sum(s => s.Length)}";
        }
    }
}