using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web;

namespace ShadyWallpaperService.DataTypes
{
    [BsonIgnoreExtraElements]
    public class ThreadEntity
    {
        public long Id;
        public string Board;
        public long Time;
        public string OpContent;
        [BsonIgnore]
        public IEnumerable<WallEntity> Walls;
    }
}