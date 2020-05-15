using ChequeAnalyserLib.Base;
using ChequeAnalyserLib.Models.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChequeAnalyserLib.Models
{
    public class Line : AABBBoundingBoxItemWithText
    {
        [JsonProperty]
        public List<Word> Words { get; set; }
    }
}
