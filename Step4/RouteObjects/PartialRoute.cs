using System;
using System.Collections.Generic;
using System.Linq;

namespace Step4.RouteObjects
{
    public class PartialRoute
    {
        public Vector2D[] Steps { get; }
        public Vector2D Remaining { get; }

        public double AStarWeight { get; }
        public double Length { get; }

        public PartialRoute(Point2D startCity, Point2D endCity, Vector2D[] steps)
        {
            Steps = steps;
            Length = Steps.Sum(s => s.Length);
            Remaining = new Vector2D(Steps.Any() ? Steps.Last().End : startCity, endCity);
            AStarWeight = Length + Remaining.Length;
        }

        public FinalRoute ToFinalRoute()
        {
            return new FinalRoute(Steps.ToArray());
        }

        public override string ToString()
        {
            return $"Steps: {Steps.Length} Length: {Length.ToString("N2")} Remaining {Remaining.Length.ToString("N2")}";
        }
    }
}