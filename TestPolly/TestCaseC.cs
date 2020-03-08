using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;

namespace TestPolly
{
    /// <summary>
    /// 1, http failed 
    /// 2, success and length less 100
    /// </summary>
    internal class TestCaseC
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        const int MaxFailCount = 10;

        // failed
         //private const string Url = "https://www.cashwu.com";

        // success
        private const string Url = "https://blog.cashwu.com";

        public async Task Run()
        {
            try
            {
                await Policy<string>.HandleResult(s => s.Length<100)
                    .Or<HttpRequestException>()
                    .WaitAndRetryAsync(MaxFailCount, retryAttempt => TimeSpan.FromMilliseconds(500),
                        (exception, timeSpan, retryCount, context) =>
                        {
                            Console.WriteLine($"exception retry -- count : {retryCount}");
                        }).ExecuteAsync(GetHttpResult);
            }
            catch (Exception e)
            {
                Console.WriteLine($"exception - {e}");
            }

            //var count = 0;
            //do
            //{
            //    try
            //    {
            //        var result = await GetHttpResult();

            //        if (result.Length < 100)
            //        {
            //            Console.WriteLine($"success - {result}");

            //            break;
            //        }

            //        count++;

            //        if (count <= MaxFailCount)
            //        {
            //            Console.WriteLine($"error result retry -- count : {count}");
            //            await Task.Delay(500);
            //        }
            //        else
            //        {
            //            Console.WriteLine("error result");
            //        }
            //    }
            //    catch (HttpRequestException ex)
            //    {
            //        count++;

            //        if (count <= MaxFailCount)
            //        {
            //            Console.WriteLine($"exception retry -- count : {count}");
            //            await Task.Delay(500);
            //        }
            //        else
            //        {
            //            Console.WriteLine($"exception - {ex}");
            //        }
            //    }
            //}
            //while (count <= MaxFailCount);
        }

        private static async Task<string> GetHttpResult()
        {
            var responseMessage = await HttpClient.GetAsync(Url);

            responseMessage.EnsureSuccessStatusCode();

            var result = await responseMessage.Content.ReadAsStringAsync();
            return result;
        }
    }
}