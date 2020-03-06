using System;
using System.Threading;

namespace TestPolly
{
    internal class TestCaseB
    {
        const int MaxFailCount = 10;

        public void Run()
        {
            var count = 0;

            do
            {
                try
                {
                    DoSomething();

                    Console.WriteLine("success");
                    break;
                }
                catch (Exception ex)
                {
                    count++;

                    if (count <= MaxFailCount)
                    {
                        Console.WriteLine($"exception retry -- count : {count}");
                        Thread.Sleep(500);
                    }
                    else
                    {
                        Console.WriteLine($"exception - {ex}");
                    }
                }
            }
            while (count <= MaxFailCount);
        }

        private void DoSomething()
        {
            // throw new NotImplementedException("test");
        }
    }
}