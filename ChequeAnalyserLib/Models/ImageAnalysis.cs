using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChequeAnalyserLib.Models
{
    public class ImageAnalysis
    {
        public const string Succeeded = "Succeeded";
        public const string Running = "Running";

        [JsonProperty]
        private string Status { get; set; }
        public List<RecognitionResult> RecognitionResults { get; set; }
        public bool IsSuccessStatus => Status.Equals(Succeeded);
    }
}
