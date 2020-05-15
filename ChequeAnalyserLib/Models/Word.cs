using ChequeAnalyserLib.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChequeAnalyserLib.Models.Base
{
    public class Word : AABBBoundingBoxItemWithText
    {
        public string Confidence { get; set; }
    }
}
