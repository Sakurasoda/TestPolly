using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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
        // private const string Url = "https://www.cashwu.com";

        // success
        private const string Url = "https://blog.cashwu.com";

        public async Task Run()
        {
            var count = 0;

            do
            {
                try
                {
                    var responseMessage = await HttpClient.GetAsync(Url);

                    responseMessage.EnsureSuccessStatusCode();

                    var result = await responseMessage.Content.ReadAsStringAsync();

                    if (result.Length < 100)
                    {
                        Console.WriteLine($"success - {result}");

                        break;
                    }

                    count++;

                    if (count <= MaxFailCount)
                    {
                        Console.WriteLine($"error result retry -- count : {count}");
                        await Task.Delay(500);
                    }
                    else
                    {
                        Console.WriteLine("error result");
                    }
                }
                catch (HttpRequestException ex)
                {
                    count++;

                    if (count <= MaxFailCount)
                    {
                        Console.WriteLine($"exception retry -- count : {count}");
                        await Task.Delay(500);
                    }
                    else
                    {
                        Console.WriteLine($"exception - {ex}");
                    }
                }
            }
            while (count <= MaxFailCount);
        }
    }
}