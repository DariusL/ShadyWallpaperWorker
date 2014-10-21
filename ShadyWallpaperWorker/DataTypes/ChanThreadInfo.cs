using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ShadyWallpaperWorker.DataTypes
{
    [DataContract]
    class ChanThreadInfo
    {
        [DataMember(Name = "no")]
        public long Id { get; set; }
        [DataMember(Name = "last_modified")]
        public long Time { get; set; }
    }
}
