using System;
using System.Collections.Generic;
using System.Text;

namespace ChequeAnalyserLib.Models
{
    public class RecognitionResult
    {
        public int Page { get; set; }
        public float ClockwiseOrientation { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Unit { get; set; }
        public List<Line> Lines { get; set; }
    }
}
