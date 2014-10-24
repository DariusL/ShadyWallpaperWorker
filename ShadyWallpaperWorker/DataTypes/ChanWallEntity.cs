using ShadyWallpaperService.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ShadyWallpaperWorker.DataTypes
{
    [DataContract]
    class ChanWallEntity
    {
        [DataMember(Name = "tim")]
        public string Filename { get; set; }
        [DataMember(Name = "ext")]
        public string Ext { get; set; }
        [DataMember(Name = "w")]
        public int Width { get; set; }
        [DataMember(Name = "h")]
        public int Height { get; set; }
        [DataMember(Name = "time")]
        public long Time { get; set; }
        [DataMember(Name = "filedeleted")]
        public int Deleted { get; set; }
        [DataMember(Name = "com")]
        public string Comment { get; set; }
        [DataMember(Name = "sticky")]
        public int Sticky { get; set; }

        public WallEntity CreateEntity(string board, long thread)
        {
            var ret = new WallEntity();
            ret.Board = board;
            ret.ThreadId = thread;
            ret.B16X9 = (int)TypeUtils.FromSizeR16By9(Width, Height);
            ret.B4X3 = (int)TypeUtils.FromSizeR4By3(Width, Height);
            ret.Time = Time;
            ret.WallUrl = String.Format("http://i.4cdn.org/{0}/{1}{2}", board, Filename, Ext);
            ret.ThumbUrl = String.Format("http://t.4cdn.org/{0}/{1}s.jpg", board, Filename);
            return ret;
        }

        //http(s)://i.4cdn.org/board/tim.ext post
        //http(s)://t.4cdn.org/board/tims.jpg thumb
    }
}
