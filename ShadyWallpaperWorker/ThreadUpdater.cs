using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using ShadyWallpaperService;
using ShadyWallpaperService.DataTypes;
using ShadyWallpaperWorker.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShadyWallpaperWorker
{
    class ThreadUpdater
    {
        private struct Job
        {
            public readonly string Board;
            public readonly long Id;
            public Job(string board, long id)
            {
                Board = board;
                Id = id;
            }
        }

        private DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<ChanThreadPage>));
        private string[] boards;
        private MongoDatabase database;
        private static Queue<Job> jobs = new Queue<Job>();
        private MongoCollection threadCollection;
        private MongoCollection postCollection;

        public ThreadUpdater(string[] boards)
        {
            this.boards = boards; 
            var connectionString = String.Format("mongodb://{0}:{1}@ds063859.mongolab.com:63859/base", Keys.User, Keys.Pass);
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            database = server.GetDatabase("base");
            threadCollection = database.GetCollection("threads");
            postCollection = database.GetCollection("posts");
        }

        public void StartUpdating()
        {
            PopulateQueue();
            ProcessQueue();
        }

        private void PopulateQueue()
        {
            foreach (var board in boards)
            {
                PopulateQueueForBoard(board);
            }
        }

        private void PopulateQueueForBoard(string board)
        {
            Console.WriteLine("Populating queue for /{0}/", board);
            var request = HttpWebRequest.CreateHttp(String.Format("http://a.4cdn.org/{0}/threads.json", board));
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (response == null || response.StatusCode != HttpStatusCode.OK)
                    return;

                var chanThreads = (serializer.ReadObject(response.GetResponseStream()) as List<ChanThreadPage>)
                    .SelectMany(t => t.Threads);//flatten pages

                var dbThreads = threadCollection.AsQueryable<ThreadEntity>()
                    .Where(t => t.Board == board)
                    .ToDictionary(t => t.Id);

                foreach (var thread in chanThreads)
                {
                    if (dbThreads.ContainsKey(thread.Id))
                    {
                        var dbThread = dbThreads[thread.Id];
                        if (dbThread.Time < thread.Time)
                        {
                            Console.WriteLine("Thread {0} is out of date", thread.Id);
                            jobs.Enqueue(new Job(board, thread.Id));
                        }
                        else
                        {
                            Console.WriteLine("Thread {0} is up to date", thread.Id);
                        }
                        dbThreads.Remove(thread.Id);
                    }
                    else
                    {
                        Console.WriteLine("Thread {0} is not in DB", thread.Id);
                        jobs.Enqueue(new Job(board, thread.Id));
                    }
                }
                
                var deadThreads = dbThreads.Select(t => t.Value.Id);
                Console.WriteLine("Threads to be removed: \n{0}", String.Join("\n", deadThreads));
                postCollection.Remove(Query<WallEntity>.In(w => w.ThreadId, deadThreads));
                threadCollection.Remove(Query<ThreadEntity>.In(t => t.Id, deadThreads));
            }
        }

        private void ProcessQueue()
        {
            Console.WriteLine("Processing queue");
            foreach(var job in jobs)
            {
                ProcessJob(job);
                Thread.Sleep(800);
            }
        }

        private void ProcessJob(Job job)
        {
            Console.WriteLine("Downloading posts for thread {0} in /{1}/", job.Id, job.Board);
            var request = WebRequest.Create(String.Format("http://a.4cdn.org/{0}/thread/{1}.json", job.Board, job.Id)) as HttpWebRequest;
            using(var response = request.GetResponse() as HttpWebResponse)
            {
                var data = ChanThreadEntity.CreateThread(response.GetResponseStream(), job.Id, job.Board);
                var newest = postCollection.AsQueryable<WallEntity>()
                    .Where(w => w.ThreadId == job.Id)
                    .OrderBy(w => w.Time)
                    .Take(1)
                    .ToList();
                var walls = data.Walls;
                if(newest.Count() > 0)
                {
                    walls = walls.Where(w => w.Time > newest.Single().Time); 
                }
                if(walls.Count() > 0)
                {
                    postCollection.InsertBatch<WallEntity>(walls);
                }
                if(data.Walls.Count() > 0)
                {
                    threadCollection.Save<ThreadEntity>(data);
                }
            }
        }
    }
}
