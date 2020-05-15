using System;
using System.Collections.Generic;
using System.Text;

namespace ChequeAnalyserLib
{
    public class Constants
    {
        public const string SubscriptionHeaderKeyName = "Ocp-Apim-Subscription-Key";

        public static string COMPUTER_VISION_ENDPOINT = "";
        public static string COMPUTER_VISION_SUBSCRIPTION_KEY = "";
        public static string UriBase = COMPUTER_VISION_ENDPOINT + "vision/v2.1/read/core/asyncBatchAnalyze";
        public static string RequestParameters = UriBase + "?" + "visualFeatures=Categories,Description,Color";
    }
}
