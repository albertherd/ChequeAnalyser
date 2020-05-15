using System;
using System.Collections.Generic;
using System.Text;

namespace ChequeAnalyserLib.Models
{
    public class ChequeAnalysis
    {
        public string Amount { get; }
        public string Date { get; }
        public string PayDescriptionLineOne { get; }
        public string PayDescriptionLineTwo { get; }
        public string AccountNumber { get; }

        public ChequeAnalysis(string amount, string date, string payDescriptionLineOne, string payDescriptionLineTwo, string accountNumber)
        {
            Amount = amount;
            Date = date;
            PayDescriptionLineOne = payDescriptionLineOne;
            PayDescriptionLineTwo = payDescriptionLineTwo;
            AccountNumber = accountNumber;
        }
    }
}
