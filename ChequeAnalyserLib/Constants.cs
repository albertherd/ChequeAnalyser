using System;
using System.Collections.Generic;
using System.Text;

namespace ChequeAnalyserLib
{
    public class Constants
    {
        public const string SubscriptionHeaderKeyName = "Ocp-Apim-Subscription-Key";

        public static string COMPUTER_VISION_ENDPOINT = "https://ahcv.cognitiveservices.azure.com/";
        public static string COMPUTER_VISION_SUBSCRIPTION_KEY = "916ddce5c4c147f8b8d91b0d8fb2cf8f";
        public static string UriBase = COMPUTER_VISION_ENDPOINT + "vision/v2.1/read/core/asyncBatchAnalyze";
        public static string RequestParameters = UriBase + "?" + "visualFeatures=Categories,Description,Color";
    }
}
