using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TECH.SharedKernel;

namespace TECH.Data.DatabaseEntity
{
    [Table("wards")]
    public class Wards : DomainEntity<int>
    {
        public int? district_id { get; set; }
        [ForeignKey("district_id")]
        public Districts? Districts { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? name { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string? type { get; set; }
    }
}
