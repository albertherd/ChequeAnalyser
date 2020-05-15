using ChequeAnalyserLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChequeAnalyserLib.Models
{
    public class AnalysedImage
    {
        public ImageAnalysis Result { get; set; }
        public QueuedImage QueuedImage { get; set; }
        public IChequeTemplate ChequeTemplate { get; set; }
    }
}
