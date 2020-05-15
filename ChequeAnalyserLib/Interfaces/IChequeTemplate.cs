using ChequeAnalyserLib.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChequeAnalyserLib.Interfaces
{
    public interface IChequeTemplate
    {
        string Name { get; }
        AABBBoundingBoxItemWithText Amount {get; }
        AABBBoundingBoxItemWithText Date { get; }
        AABBBoundingBoxItemWithText PayDescriptionLineOne { get; }
        AABBBoundingBoxItemWithText PayDescriptionLineTwo { get; }
        AABBBoundingBoxItemWithText AccountNumber { get; }
    }
}
