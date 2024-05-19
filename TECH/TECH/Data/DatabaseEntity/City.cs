using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TECH.SharedKernel;

namespace TECH.Data.DatabaseEntity
{
    [Table("cities")]
    public class City : DomainEntity<int>
    {
        [Column(TypeName = "nvarchar(500)")]
        public string? name { get; set; }

        [Column(TypeName = "varchar(500)")]
        public string? type { get; set; }
    }
}
