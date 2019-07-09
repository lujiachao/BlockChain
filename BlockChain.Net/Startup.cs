using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace BlockChain.Net
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Map("/BlockChain", _ =>
            {
                _.Run(async context =>
                {
                    if (context.Request.Method == "POST")
                    {
                        // 增加区块链
                        if (BlockGenerator._blockChain.Count == 0)
                        {
                            Block firstBlock = new Block()
                            {
                                Index = 0,
                                TimeStamp = BlockGenerator.CalculateCurrentTimeUTC(),
                                BPM = 0,
                                Hash = string.Empty,
                                PrevHash = string.Empty
                            };
                            BlockGenerator._blockChain.Add(firstBlock);
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(firstBlock));
                        }
                        else
                        {
                            int.TryParse(context.Request.Form["BPM"][0], out int bpm);
                            Block oldBlock = BlockGenerator._blockChain.Last();
                            Block newBlock = BlockGenerator.GenerateBlock(oldBlock, bpm);
                            if (BlockGenerator.IsBlockValid(newBlock, oldBlock))
                            {
                                List<Block> newBlockChain = new List<Block>();
                                foreach (var block in BlockGenerator._blockChain)
                                {
                                    newBlockChain.Add(block);
                                }
                                newBlockChain.Add(newBlock);
                                BlockGenerator.SwitchChain(newBlockChain);
                            }
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(newBlock));
                        }
                    }
                });
            });
            app.Map("/BlockChains", _ =>
            {
                _.Run(async context =>
                {
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(BlockGenerator._blockChain));
                });
            });
            app.UseMvc();
        }
    }
}
