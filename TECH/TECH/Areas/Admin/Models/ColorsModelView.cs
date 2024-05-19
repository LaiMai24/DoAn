using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TECH.Areas.Admin.Models
{
    public class ColorsModelView
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? code { get; set; }
        public int? status { get; set; }
    }
}
