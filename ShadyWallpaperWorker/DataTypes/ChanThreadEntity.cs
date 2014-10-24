using ShadyWallpaperService.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ShadyWallpaperWorker.DataTypes
{
    [DataContract]
    class ChanThreadEntity
    {
        [DataMember(Name = "posts")]
        public IEnumerable<ChanWallEntity> Posts{ get; set; }

        public ThreadEntity CreateThread(long id, string board)
        {
            var ret = new ThreadEntity();
            ret.OpContent = Posts.First().Comment;
            ret.Time = Posts.Last().Time;
            ret.Id = id;
            ret.Board = board;
            ret.OpPost = Posts.First().CreateEntity(board, id);
            return ret;
                    
        }
        public static ChanThreadEntity Parse(System.IO.Stream response, long id, string board)
        {
            var threadDeserializer = new DataContractJsonSerializer(typeof(ChanThreadEntity));
            return threadDeserializer.ReadObject(response) as ChanThreadEntity;
        }
    }
}
