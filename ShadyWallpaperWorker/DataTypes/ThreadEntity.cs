using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
        public IEnumerable<WallEntity> Walls;

        public ThreadEntity() { }
        public ThreadEntity(long id, IEnumerable<WallEntity> walls)
        {
            Id = id;
            Walls = walls;
        }
    }
}