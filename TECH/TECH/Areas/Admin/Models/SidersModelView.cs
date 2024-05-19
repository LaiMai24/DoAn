using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TECH.Areas.Admin.Models
{
    public class SidersModelView
    {
        public int id { get; set; }
        public string? image { get; set; }
        public int? status { get; set; }
        public DateTime? create_at { get; set; }
    }
}
