using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TECH.Areas.Admin.Models
{
    public class FeeModelView
    {
        public int id { get; set; }

        public int? city_id { get; set; }
        public CityModelView? CityModelView { get; set; }
        public int? district_id { get; set; }
        public DistrictsModelView? DistrictsModelView { get; set; }
        public int? ward_id { get; set; }
        public WardsModelView? WardsModelView { get; set; }
        public int? fee { get; set; }
        public string? feestr { get; set; }
    }
   
}
