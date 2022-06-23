using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemory.Data
{
    public class InMemoryData : DataDB
    {
        public InMemoryData(DataDBOptions<InMemoryData> options)
            : base(options)
        {
        }


        public DbSet<InMemory> InMemory { get; set; }

    }
}