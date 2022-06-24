using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemory.Models
{
    public class InMemory
    {
        public int itemID { get; set; }
        public string? GetRecord { get; set; }
        public string? GetRecords { get; set; }
        public string? AddRecord { get; set; }
        public string? UpdateRecord { get; set; }
        public string? DeleteRecord { get; set; }
        public DateTime CreatedTime { get; set; }

    }
}