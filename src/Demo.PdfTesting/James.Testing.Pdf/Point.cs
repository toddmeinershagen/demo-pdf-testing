using System;

namespace James.Testing.Pdf
{
    public struct Point : IEquatable<Point>
    {
        public double X { get; set; }
        public double Y { get; set; }

        public bool Equals(Point other)
        {
            return this.X == other.X && this.Y == other.Y;
        }

        public override bool Equals(object value)
        {
            if (value == null)
                return false;

            return Equals((Point)value);
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Point point1, Point point2)
        {
            if (object.ReferenceEquals(point1, point2)) return true;
            if (object.ReferenceEquals(point1, null)) return false;
            if (object.ReferenceEquals(point2, null)) return false;

            return point1.Equals(point2);
        }

        public static bool operator !=(Point point1, Point point2)
        {
            if (object.ReferenceEquals(point1, point2)) return false;
            if (object.ReferenceEquals(point1, null)) return true;
            if (object.ReferenceEquals(point2, null)) return true;

            return !point1.Equals(point2);
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}
