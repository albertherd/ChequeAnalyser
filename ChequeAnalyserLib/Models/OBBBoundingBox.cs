using SixLabors.Primitives;
using System.Collections.Generic;

namespace ChequeAnalyserLib.Models
{
    public class OBBBoundingBox
    {
        public List<Point> Points { get; set; }

        public OBBBoundingBox(List<int> rawObbPoints)
        {
            Points = new List<Point>
            {
                new Point(rawObbPoints[0], rawObbPoints[1]),
                new Point(rawObbPoints[2], rawObbPoints[3]),
                new Point(rawObbPoints[4], rawObbPoints[5]),
                new Point(rawObbPoints[6], rawObbPoints[7])
            };
        }
    }
}
