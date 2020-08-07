using System;
using System.Data.Entity.Spatial;
using Shopping.Infrastructure.Helper;

namespace Shopping.Infrastructure.Types
{
    // <summary>
    // موقعیت
    // </summary>
    public class Position : ILatLong, IEquatable<Position>
    {
        public Position() { }
        public Position(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
        /// <summary>
        /// عرض جغرافیایی
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// طول جغرافیایی
        /// </summary>
        public double Longitude { get; set; }
        public string ToQueryString()
        {
            return Latitude.ToString().Replace('/', '.') + "," + Longitude.ToString().Replace('/', '.');
        }
        public override string ToString()
        {
            return Longitude.ToString().Replace('/', '.') + " " + Latitude.ToString().Replace('/', '.');
        }
        public static DbGeography CreateDbGeography(double latitude, double longitude)
        {
            return DbGeography.FromText(
                $"POINT({longitude.ToString().Replace('/', '.')} {latitude.ToString().Replace('/', '.')})");
        }
        public bool Equals(Position other)
        {
            if (other == null)
                return false;
            return Latitude.EqualAccurate(other.Latitude) && Longitude.EqualAccurate(other.Longitude);
        }
        public static Position Default()
        {
            return new Position(0, 0);
        }
    }
}