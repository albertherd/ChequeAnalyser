using ChequeAnalyserLib.Base;
using ChequeAnalyserLib.Interfaces;
using SixLabors.Primitives;

namespace ChequeAnalyser.cs.ChequeTemplates
{
    public class RandomAmericanCheque : IChequeTemplate
    {
        public string Name { get; } = "RandomAmericanCheque";
        public AABBBoundingBoxItemWithText Amount { get; } = new AABBBoundingBoxItemWithText(new Point(880, 190), new Point(1070, 220), "Amount");
        public AABBBoundingBoxItemWithText Date { get; } = new AABBBoundingBoxItemWithText(new Point(750, 90), new Point(930, 155), "Date");
        public AABBBoundingBoxItemWithText PayDescriptionLineOne { get; } = new AABBBoundingBoxItemWithText(new Point(170, 160), new Point(840, 225), "PayDescriptionLineOne");
        public AABBBoundingBoxItemWithText PayDescriptionLineTwo { get; } = new AABBBoundingBoxItemWithText(new Point(50, 225), new Point(855, 290), "PayDescriptionLineTwo");
        public AABBBoundingBoxItemWithText AccountNumber { get; } = new AABBBoundingBoxItemWithText(new Point(50, 430), new Point(775, 470), "AccountNumber");

    }
}
