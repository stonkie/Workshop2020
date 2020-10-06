using System;

namespace Step4.RouteObjects
{
    public class Vector2D
    {
        public Point2D Start { get; }
        public Point2D End { get; }
        public double Length { get; }

        public Vector2D(Point2D start, Point2D end)
        {
            Start = start;
            End = end;

            double xDiff = End.X - Start.X;
            double yDiff = End.Y - Start.Y;

            Length = Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
        }

        public override string ToString()
        {
            return $"{Start} -> {End} : {Length}";
        }
    }
}