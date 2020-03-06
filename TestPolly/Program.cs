using System.Threading.Tasks;

namespace TestPolly
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // normal return 
            // new TestCaseA().Run();

            // exception
            // new TestCaseB().Run();
            
            // http
            await new TestCaseC().Run();
        }
    }
}