namespace Step4.RouteObjects
{
    public class Point2D
    {
        public double X { get; }
        public double Y { get; }

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector2D DistanceTo(Point2D destination)
        {
            return new Vector2D(this, destination);
        }

        public override string ToString()
        {
            return $"({X.ToString("N2")}, {Y.ToString("N2")})";
        }
    }
}