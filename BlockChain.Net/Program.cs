using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace BlockChain.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            BlockAction();    
        }

        static void BlockAction()
        {
            var webhostBuilder = WebHost.CreateDefaultBuilder()
                .UseUrls("http://*:7500/")
                .UseStartup<Startup>()
                .UseKestrel()
                .Build();
            webhostBuilder.Run();
        }
    }
}
