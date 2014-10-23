using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ShadyWallpaperService.DataTypes
{
    [DataContract]
    [BsonIgnoreExtraElements]
    public class WallEntity
    {
        public string WallUrl;
        public string ThumbUrl;

        public string Board;
        public long ThreadId;
        public int B16X9;
        public int B4X3;
        public long Time;
    }
}