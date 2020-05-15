using System;
using System.Collections.Generic;
using ChequeAnalyserLib.Models;
using SixLabors.Primitives;
using SixLabors.Shapes;

namespace ChequeAnalyserLib.Base
{
    public class AABBBoundingBox
    {
        public Point Min { get; set; }
        public Point Max { get; set; }

        public AABBBoundingBox(OBBBoundingBox obbBoundingBox)
        {
            /*
             * Azure API returns 4 points per bounding box in an OBB format.
             * For simpler collision dedection, we'll convert them to AABB
             * https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fstatic1.squarespace.com%2Fstatic%2F54851541e4b0fb60932ad015%2Ft%2F571b901f2eeb814e3143cad6%2F1461424171847%2F&f=1&nofb=1
             */

            InitializeFromOBB(obbBoundingBox.Points);
        }

        public AABBBoundingBox(Point min, Point max)
        {
            Min = min;
            Max = max;
        }

        /*
         * Ripped off from https://developer.mozilla.org/en-US/docs/Games/Techniques/3D_collision_detection
         */
        public bool IsIntersecting(AABBBoundingBox other)
        {
            return Min.X <= other.Max.X && Max.X >= other.Min.X &&
             Min.Y <= other.Max.Y && Max.Y >= other.Min.Y;
        }

        public RectangularPolygon AsRectangularPolygon()
        {
            return new RectangularPolygon(Min, Max);
        }

        private void InitializeFromOBB(List<Point> points)
        {
            if (points.Count < 4)
                throw new ArgumentException("Must contain at least 4 points", nameof(points));

            /* Naive, sorry.*/
            int xMin = points[0].X;
            int xMax = points[0].X;

            int yMin = points[0].Y;
            int yMax = points[0].Y;


            for (int i = 1; i < points.Count; i++)
            {
                if (points[i].X < xMin)
                    xMin = points[i].X;

                if (points[i].Y < yMin)
                    yMin = points[i].Y;

                if (points[i].X > xMax)
                    xMax = points[i].X;

                if (points[i].Y > yMax)
                    yMax = points[i].Y;
            }

            Min = new Point(xMin, yMin);
            Max = new Point(xMax, yMax);
        }
    }
}
