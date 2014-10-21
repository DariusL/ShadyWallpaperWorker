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

        private ThreadEntity CreateThread(long id, string board)
        {
            var ret = new ThreadEntity();
            ret.OpContent = Posts.First().Comment;
            ret.Time = Posts.Last().Time;
            ret.Id = id;
            ret.Board = board;
            ret.Walls = Posts
                .Select(w => w.CreateEntity(board, id))
                .Where(w => w.B16X9 != (int)R16By9.NA || w.B4X3 != (int)R4By3.NA);
            return ret;
                    
        }
        public static ThreadEntity CreateThread(System.IO.Stream response, long id, string board)
        {
            var threadDeserializer = new DataContractJsonSerializer(typeof(ChanThreadEntity));
            var thread = threadDeserializer.ReadObject(response) as ChanThreadEntity;
            if (thread == null)
                return null;

            return thread.CreateThread(id, board);
        }
    }
}
