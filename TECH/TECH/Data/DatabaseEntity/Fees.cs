using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TECH.SharedKernel;

namespace TECH.Data.DatabaseEntity
{
    [Table("fees")]
    public class Fees : DomainEntity<int>
    {
        public int? city_id { get; set; }
        [ForeignKey("city_id")]
        public City? City { get; set; }

        public int? district_id { get; set; }
        [ForeignKey("district_id")]
        public Districts? Districts { get; set; }

        public int? ward_id { get; set; }
        [ForeignKey("ward_id")]
        public Wards? Wards { get; set; }

        public int? fee { get; set; }

    }
}
