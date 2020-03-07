using System.Threading.Tasks;

namespace TestPolly
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // normal return 
            //new TestCaseA().Run();

            // exception
            new TestCaseB().Run();

            // http
            //await new TestCaseC().Run();
        }
    }
}