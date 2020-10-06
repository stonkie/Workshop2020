using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;

namespace Step4.RouteObjects
{
    internal class RouteCalculator : IRouteCalculator
    {
        public int CitiesCount => _cityLocations.Count;

        private readonly List<Point2D> _cityLocations;
        private readonly double _maximumDistance;

        public RouteCalculator(List<Point2D> cityLocations, double maximumDistance)
        {
            _cityLocations = cityLocations;
            _maximumDistance = maximumDistance;
        }

        public FinalRoute? PlotCourse(int cityIndex1, int cityIndex2)
        {
            return PlotCourse(_cityLocations[cityIndex1], _cityLocations[cityIndex2]);
        }

        private FinalRoute? PlotCourse(Point2D startCity, Point2D endCity)
        {
            List<PartialRoute> partialRoutesToTry = new List<PartialRoute>();
            Dictionary<Point2D, double> bestRouteLengthToCity = new Dictionary<Point2D, double>();

            PartialRoute initialPartialRoute = new PartialRoute(startCity, endCity, new Vector2D[] {});
            partialRoutesToTry.Add(initialPartialRoute);

            while (partialRoutesToTry.Any())
            {
                PartialRoute bestPartialRoute = partialRoutesToTry.First();

                foreach (Point2D intermediateCity in _cityLocations)
                {
                    Vector2D newStep = bestPartialRoute.Remaining.Start.DistanceTo(intermediateCity);

                    if (newStep.Length > 0 && newStep.Length < _maximumDistance)
                    {
                        int neededSize = bestPartialRoute.Steps.Length + 1;

                        Vector2D[] newStepsList = new Vector2D[neededSize];
                        Array.Copy(bestPartialRoute.Steps, newStepsList, bestPartialRoute.Steps.Length);

                        newStepsList[^1] = newStep;

                        PartialRoute newPartialRoute = new PartialRoute(startCity, endCity, newStepsList);

                        if (intermediateCity == endCity)
                        {
                            var finalRoute = newPartialRoute.ToFinalRoute();
                            return finalRoute;
                        }

                        // Only add to routes to try if there is not already a shorter route to this destination
                        if (!bestRouteLengthToCity.ContainsKey(newPartialRoute.Remaining.Start) || bestRouteLengthToCity[newPartialRoute.Remaining.Start] > newPartialRoute.Length)
                        {
                            bestRouteLengthToCity[newPartialRoute.Remaining.Start] = newPartialRoute.Length;
                            partialRoutesToTry.Add(newPartialRoute);
                        }
                    }
                }

                partialRoutesToTry.RemoveAt(0);

                // Sort based on the heuristic
                partialRoutesToTry.Sort((first, second) => first.AStarWeight.CompareTo(second.AStarWeight));
            }

            return null;
        }
    }
}