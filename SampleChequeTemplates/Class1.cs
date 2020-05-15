using ChequeAnalyserLib.Interfaces;
using ChequeAnalyserLib.Models;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChequeAnalyser.cs.ChequeTemplates
{
    public class RandomAmericanCheque : IChequeTemplate
    {
        public string Name { get; }
        public AABBBoundingBox Amount { get; } = new AABBBoundingBox(new Point(870, 170), new Point(1070, 230));
        public AABBBoundingBox Date { get; } = new AABBBoundingBox(new Point(715, 90), new Point(930, 155));
        public AABBBoundingBox PayDescriptionLineOne { get; } = new AABBBoundingBox(new Point(150, 150), new Point(840, 225));
        public AABBBoundingBox PayDescriptionLineTwo { get; } = new AABBBoundingBox(new Point(50, 225), new Point(855, 290));
        public AABBBoundingBox AccountNumber { get; } = new AABBBoundingBox(new Point(50, 430), new Point(775, 470));

    }
}
