using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TECH.SharedKernel;

namespace TECH.Data.DatabaseEntity
{
    [Table("siders")]
    public class Siders : DomainEntity<int>
    {
        [Column(TypeName = "varchar(500)")]
        public string? image { get; set; }     
        public int? status { get; set; }
        public DateTime? create_at { get; set; }
    }
}
