using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TECH.Areas.Admin.Models.Search
{
    public class FeeViewModelSearch : PageViewModel
    {
        public int city_id { get; set; }
        public int district_id { get; set; }
        public int ward_id { get; set; }
        public string? name { get; set; }

        
    }
}
