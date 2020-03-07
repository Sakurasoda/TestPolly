using System;
using System.Threading;
using Polly;

namespace TestPolly
{
    internal class TestCaseB
    {
        const int MaxFailCount = 10;

        public void Run()
        {
            try
            {
                Policy
                    .Handle<NotImplementedException>(exception => exception.Message == "test")
                    .WaitAndRetry(
                        MaxFailCount,
                        retryAttempt => TimeSpan.FromMilliseconds(500),
                        (exception, timeSpan, retryCount, context) => 
                        {
                            Console.WriteLine($"exception retry -- count : {retryCount}");
                        }
                    ).Execute(DoSomething);
                Console.WriteLine("success");

            }
            catch (Exception e)
            {
                Console.WriteLine($"exception - {e}");
            }
            Console.ReadLine();

            //var count = 0;
            //do
            //{
            //    try
            //    {
            //        DoSomething();

            //        Console.WriteLine("success");
            //        break;
            //    }
            //    catch (Exception ex)
            //    {
            //        count++;

            //        if (count <= MaxFailCount)
            //        {
            //            Console.WriteLine($"exception retry -- count : {count}");
            //            Thread.Sleep(500);
            //        }
            //        else
            //        {
            //            Console.WriteLine($"exception - {ex}");
            //        }
            //    }
            //}
            //while (count <= MaxFailCount);
        }

        private void DoSomething()
        {
            throw new NotImplementedException("test");
        }
    }
}