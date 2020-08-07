using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Types;
using Shopping.Infrastructure.Types;

namespace Shopping.Infrastructure.Helper
{
    public static class DbGeographyExtenstions
    {
        public static Position ToPosition(this DbGeography dbGeography)
        {
            return new Position(dbGeography.Latitude.Value, dbGeography.Longitude.Value);
        }

        public static ICollection<Position> ToPositions(this DbGeography dbGeography)
        {
            IList<Position> positions = new List<Position>();

            var data = dbGeography.ProviderValue.ToString();
            if (data.Contains("POLYGON"))
            {
                data = data.Substring(10);
                data = data.Replace("))", "");//.Replace(" ","");
                var resultPoints = data.Split(new[] { ", " }, StringSplitOptions.None);


                foreach (var resultPoint in resultPoints)
                {
                    var da = resultPoint.Split(' ');
                    var latitude = double.Parse(da[1]);
                    var longitude = double.Parse(da[0]);
                    positions.Add(new Position(latitude, longitude));
                }

                return positions;
            }



            if (dbGeography.PointCount > 0)
            {
                for (int i = 1; i <= dbGeography.PointCount; i++)
                {
                    var point = dbGeography.ElementAt(i);
                    positions.Add(new Position(point.Latitude.Value, point.Longitude.Value));
                }
            }
            return positions;
        }

        public static DbGeography ToDbGeography(this Position position)
        {
            return DbGeography.FromText("POINT(" + position + ")");
        }

        public static DbGeography ToDbGeographiesMultiPoint(this IEnumerable<Position> positions)
        {
            var points = positions.Aggregate("MULTIPOINT(", (current, position) => current + (position + ","));
            points = points.Remove(points.Length - 1);
            points += ")";
            return DbGeography.MultiPointFromText(points, 4326);
        }

        public static DbGeography ToDbGeographyPolygon(this IEnumerable<Position> rawPositions)
        {

            var positions = rawPositions.ToList();
            if (positions.First().ToString() != positions.Last().ToString())
                positions.Add(positions.First());

            var count = 0;
            var sb = new StringBuilder();
            sb.Append(@"POLYGON((");
            foreach (var coordinate in positions)
            {
                if (count == 0)
                {
                    sb.Append(coordinate.Longitude + " " + coordinate.Latitude);
                }
                else
                {
                    sb.Append("," + coordinate.Longitude + " " + coordinate.Latitude);
                }

                count++;
            }

            sb.Append(@"))");

            return DbGeography.PolygonFromText(sb.ToString(), 4326);
        }

        public static DbGeography GetCentralGeographyPosition(this IEnumerable<Position> rawPositions)
        {
            var positions = rawPositions.ToList();
            var latitude = positions.Sum(item => item.Latitude) / positions.Count;
            var longitude = positions.Sum(item => item.Longitude) / positions.Count;
            return Position.CreateDbGeography(latitude, longitude);
        }

        public static bool IsValidPolygon(this DbGeography polygon)
        {
            SqlGeography sqlPolygon = SqlGeography.STGeomFromWKB(new System.Data.SqlTypes.SqlBytes(polygon.AsBinary()), DbGeography.DefaultCoordinateSystemId).MakeValid();
            return true;
        }

        public static bool IsInside(this DbGeography point, DbGeography polygon)
        {
            //DbGeography point = DbGeography.FromText(string.Format("POINT({1} {0})", latitude.ToString().Replace(',', '.'), longitude.ToString().Replace(',', '.')), DbGeography.DefaultCoordinateSystemId);

            // If the polygon area is larger than an earth hemisphere (510 Trillion m2 / 2), we know it needs to be fixed
            /*if (polygon.Area.HasValue && polygon.Area.Value > 255000000000000L)
            {
                
            }*/

            SqlGeography sqlPolygon = SqlGeography.STGeomFromWKB(new System.Data.SqlTypes.SqlBytes(polygon.AsBinary()), DbGeography.DefaultCoordinateSystemId).MakeValid();
            sqlPolygon = sqlPolygon.ReorientObject();
            polygon = DbGeography.FromBinary(sqlPolygon.STAsBinary().Value);




            return point.Intersects(polygon);
        }

        public static bool IsOverlap(this DbGeography polygon1, DbGeography polygon2)
        {

            SqlGeography sqlPolygon1 = SqlGeography.STGeomFromWKB(new System.Data.SqlTypes.SqlBytes(polygon1.AsBinary()), DbGeography.DefaultCoordinateSystemId).MakeValid();
            sqlPolygon1 = sqlPolygon1.ReorientObject();
            polygon1 = DbGeography.FromBinary(sqlPolygon1.STAsBinary().Value);


            SqlGeography sqlPolygon2 = SqlGeography.STGeomFromWKB(new System.Data.SqlTypes.SqlBytes(polygon2.AsBinary()), DbGeography.DefaultCoordinateSystemId).MakeValid();
            sqlPolygon2 = sqlPolygon2.ReorientObject();
            polygon2 = DbGeography.FromBinary(sqlPolygon2.STAsBinary().Value);

            return polygon1.Intersects(polygon2);
        }
    }
}