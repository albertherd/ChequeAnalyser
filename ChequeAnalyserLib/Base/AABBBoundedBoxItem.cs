using ChequeAnalyserLib.Models;
using Newtonsoft.Json;
using SixLabors.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChequeAnalyserLib.Base
{
    [JsonObject(MemberSerialization.OptOut)]
    public abstract class AABBBoundedBoxItem
    {
        private AABBBoundingBox _boundingBox;

        [JsonProperty("BoundingBox")]
        private List<int> OBBBoundingBoxRaw { get; set; }

        [JsonIgnore]
        public AABBBoundingBox BoundingBox
        {
            get
            {
                if (_boundingBox != null)
                    return _boundingBox;

                _boundingBox = new AABBBoundingBox(new OBBBoundingBox(OBBBoundingBoxRaw));
                return _boundingBox;
            }
        }

        public AABBBoundedBoxItem() { }
        public AABBBoundedBoxItem(Point min, Point max)
        {
            _boundingBox = new AABBBoundingBox(min, max);
        }
    }
}
