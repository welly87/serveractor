
using System;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace DataFlowSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var action = new ActionBlock<int>(x =>
                {

                    Console.WriteLine("Processing : {0} in {1}", x, Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(100);
                });

            for (int i = 0; i < 1000; i++)
            {
                action.SendAsync(i);

                Console.WriteLine("Post {0} in {1}", i, Thread.CurrentThread.ManagedThreadId);
            }

            Console.ReadLine();
        }
    }
}
