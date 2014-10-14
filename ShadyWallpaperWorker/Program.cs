﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadyWallpaperWorker
{
    static class Program
    {
        private static Queue<string> jobs = new Queue<string>();

        static void Main(string[] args)
        {
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
