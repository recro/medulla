using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace grpcQueryAPI
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

            services.AddGrpc();
            services.DataDB<InMemoryData>(options => options.UseInMemoryDatabase("InMemory"));
        }
    }
    
}   
