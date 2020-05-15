using Newtonsoft.Json;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChequeAnalyserLib.Base
{
    public class AABBBoundingBoxItemWithText : AABBBoundedBoxItem
    {
        [JsonProperty]
        public string Text { get; set; }

        public AABBBoundingBoxItemWithText() { }

        public AABBBoundingBoxItemWithText(Point min, Point max, string text)
            :base(min,max)
        {
            Text = text;
        }
    }
}
