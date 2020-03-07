using System;
using Polly;
using Polly.Retry;

namespace TestPolly
{
    internal class TestCaseA
    {
        const int MaxFailCount = 10;

        public void Run()
        {
            Policy.HandleResult(false)
                .Retry(MaxFailCount,
                    (result, count) =>
                    {
                        Console.WriteLine($"retry -- count : {count}");
                    })
                .Execute(GetResult);
            Console.ReadLine();

            //var count = 1;
            //do
            //{
            //    var result = GetResult();

            //    if (!result)
            //    {
            //        Console.WriteLine($"retry -- count : {count}");

            //        count++;
            //    }
            //    else
            //    {
            //        Console.WriteLine("success");
            //        break;
            //    }
            //}
            //while (count <= MaxFailCount);
        }

        private bool GetResult()
        {
            return false;
        }
    }
}