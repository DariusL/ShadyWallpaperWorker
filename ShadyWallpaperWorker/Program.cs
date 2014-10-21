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
        private static readonly string[] boards = new string[] { "w", "wg" };

        static void Main(string[] args)
        {
            var updater = new ThreadUpdater(boards);
            updater.StartUpdating();
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach(var item in collection)
            {
                action(item);
                yield return item;
            }
        }
    }
}
