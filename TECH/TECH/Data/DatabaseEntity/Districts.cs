using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TECH.SharedKernel;

namespace TECH.Data.DatabaseEntity
{
    [Table("districts")]
    public class Districts : DomainEntity<int>
    {
        public int? city_id { get; set; }
        [ForeignKey("city_id")]
        public City? City { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string? name { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string? type { get; set; }
    }
}
