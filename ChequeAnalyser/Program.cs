using ChequeAnalyser.cs.ChequeTemplates;
using ChequeAnalyserLib.Interfaces;
using ChequeAnalyserLib.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ChequeAnalyser.cs
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IChequeTemplate> templates = new List<IChequeTemplate>()
            {
                new RandomAmericanCheque()
            };
            var analyser = new ChequeAnalyserLib.Analyser(templates, new ActionBlock<ChequeAnalysis>((Action<ChequeAnalysis>)onChequeAnalysisComplete));


            foreach(FileInfo file in GetFiles())
            {
                analyser.EnqueueImageAnalysis(file.FullName);
            }         
            
            new ManualResetEvent(false).WaitOne();
        }

        private static List<FileInfo> GetFiles()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine("Analysing files ending with _analysis in directory: " + currentDirectory);
            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());

            List<FileInfo> files = di.GetFiles()
                .Where(file => Path.GetFileNameWithoutExtension(file.Name).EndsWith("_analysis")).ToList();

            Console.WriteLine("Found: " + string.Join("\n", files.Select(file => file.FullName + Environment.NewLine).ToList()));

            return files;
        }

        private static void onChequeAnalysisComplete(ChequeAnalysis obj)
        {
            Console.WriteLine($"Date: {obj.Date}");
            Console.WriteLine($"Payment Details Line one: {obj.PayDescriptionLineOne}");
            Console.WriteLine($"Payment Details Line two: {obj.PayDescriptionLineTwo}");
            Console.WriteLine($"Target Account Number: {obj.AccountNumber}");
            Console.WriteLine($"Amount: {obj.Amount}");
            Console.WriteLine();
        }
    }
}
