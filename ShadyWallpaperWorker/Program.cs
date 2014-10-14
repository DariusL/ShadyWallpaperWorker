using ShadyWallpaperWorker.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShadyWallpaperWorker
{
    static class Program
    {
        private static Queue<string> jobs = new Queue<string>();

        static void Main(string[] args)
        {
            var request = WebRequest.Create("http://a.4cdn.org/wg/thread/5957541.json") as HttpWebRequest;
            using(var response = request.GetResponse() as HttpWebResponse)
            {
                var data = ChanThreadEntity.CreateThread(response.GetResponseStream(), 5957541, "wg");
            }
        }

        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach(var item in collection)
            {
                action(item);
            }
        }
    }
}
